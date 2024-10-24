// 2Dプレイヤーの処理.
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Player2DMove : MonoBehaviour
{
    // アニメーション.
    private PlayerAnim2D _anim;
    // ゲートの判定.
    private GateFlag _transitionScene;

    // プレイヤーのリジットボディ.
    private Rigidbody _rigid;
    // マテリアル.
    private BoxCollider _boxCollider;
    // プレイヤーのアニメーション.
    private Animator _animator;
    // フェードシーン遷移.
    private Fade2DSceneTransition _flag;
    // 足から出てくるパーティクル.
    [SerializeField] private ParticleSystem _particle;
    // ギミックを解いたかどうか.
    private SolveGimmickManager _gimmickManager;
    // サウンドマネージャー.
    private SoundManager _soundManager;

    // ワープの座標.
    private Vector3 _warpPosition = Vector3.zero;
    // 当たったエネミーの距離と方向.
    private Vector3 _enemyDirection = Vector3.zero;

    // レンダーマテリアル.
    public Renderer[] _renderer;
    // マテリアルの色.
    private Color[] _color;

    // プレイヤーの体力.
    public int _hp = 10000;

    // ダメージを受けた後の無敵時間.
    private int _invincibleTime;
    // ダメージを受けた後の最大無敵時間.
    public int _invincibleMaxTime = 120;
    // 足音のするタイミング.
    private int _footstepsTime = 0;

    // ジャンプ力.
    private float _jumpPower = 30.0f;
    // ノックバック力.
    public float _knockBackPower = 1.0f;
    // ジャンプしているかどうか.
    private bool _isJumpNow;
    // どの向きを向いているか.
    // true :右.
    // false:左.
    private bool _isDirection;
    // 動けるように処理を通すかどうか.
    public bool _isMoveActive = true;
    // 敵に当たったかどうか.
    public bool _isHitEnemy = false;
    // ダメージを受けたかどうか
    private bool _isDamage = false;
    // Rendererの表示、非表示.
    private bool _isRendererDisplay = true;

    // ゲームオーバーSEが鳴ったらtrueにする.
    private bool _isSeGameOver;

    // イベントが発生する座標.
    [SerializeField] private float _eventPos;

    // ポーズ画面.
    private UpdatePause _pause;
    

    void Start()
    {
        // 初期化処理.
        Init();
    }
    
    void Update()
    {
        if (_pause._isPause) return;
        // ワープ.
        Warp();
        // 全体の挙動.
        WholeBehavior();
        // やられた時の処理
        GameOver();
        // ワープするときの演出.
        WarpDirect();
    }

    private void FixedUpdate()
    {
        if (_pause._isPause) return;
        // デバッグ用の処理.
        FallDebug();
        // 敵と当たった時の処理.
        HitEnemy();
        // 表示非表示.
        RendererDisplay();

        if(_isDamage)
        {
            DamageBlinking();
        }

        // ゴールした時の向き.
        GoalPlayerDirection();

        if (_flag._isGoal) return;

        // 移動しているプレイヤーの向き.
        MovePlayerDirection();
    }

    private void OnCollisionStay(Collision collision)
    {
        // 地面から離れたら.
        if (collision.gameObject.tag == "Stage")
        {
            if (_isJumpNow)
            {
                _isJumpNow = false;
                _particle.Play();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 敵に当たったら体力を1減らす.
        if (collision.gameObject.tag == "Enemy")
        {
            if (_invincibleTime > 0 && _invincibleTime < 120) return;

            _isHitEnemy = true;
            // 自身と敵の距離と方向の正規化.
            _enemyDirection = (transform.position - collision.transform.position).normalized;
        }

        if(collision.gameObject.tag == "DamageFloor")
        {
            Damage();

            transform.position = new Vector3(25.0f, 0.0f, 0.0f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 地面についたら.
        if (collision.gameObject.tag == "Stage")
        {
            if (!_isJumpNow)
            {
                _isJumpNow = true;
                _particle.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _warpPosition = other.transform.position;

        if(other.gameObject.tag == "BossEat")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _warpPosition = Vector3.zero;
    }

    // 初期化.
    private void Init()
    {
        _anim = this.GetComponent<PlayerAnim2D>();
        _transitionScene = GetComponent<GateFlag>();
        _rigid = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        _flag = GameObject.Find("FadeObject2D").GetComponent<Fade2DSceneTransition>();

        _gimmickManager = GameObject.FindWithTag("GimmickManager").GetComponent<SolveGimmickManager>();

        _pause = GameObject.Find("PauseSystem").GetComponent<UpdatePause>();

        _isDirection = false;
        _isSeGameOver = false;
    }

    // プレイヤーの全体の挙動.
    private void WholeBehavior()
    {
        // ジャンプ力の限界値.
        if (_rigid.velocity.y >= 30.0f)
        {
            Debug.Log("通る");
            _rigid.velocity = new Vector3(_rigid.velocity.x, 30.0f, _rigid.velocity.z);
        }

        if (_hp > 0 && _isMoveActive && !_gimmickManager.GetSolve())
        {
            // プレイヤーの移動処理.
            Move();

            // アニメーション.
            Anim();

        }
        else
        {
            // 重力以外の力をなくす.
            _rigid.velocity = new Vector3(0.0f, _rigid.velocity.y, 0.0f);

            // ゴールしていないときとしている時とでアニメーションを変更.
            if (!_flag._isGoal)
            {
                ForciblyIdle();
            }
            else if (_flag._isGoal)
            {
                GoalAnim();
            }

        }
    }

    // アニメーション制御.
    private void Anim()
    {
        _animator.SetBool("Idle", _anim.Idle());
        _animator.SetBool("Run", _anim.Run());
        _animator.SetBool("Jump", _anim.Jump());
        _animator.SetBool("GameOver", _anim.GameOver());
    }

    // アニメーションを強制的にアイドル状態にする
    private void ForciblyIdle()
    {
        _animator.SetBool("Idle", true);
        _animator.SetBool("Run", false);
        _animator.SetBool("Jump", false);
    }

    // ゴールした時のアニメーション
    private void GoalAnim()
    {
        _animator.SetBool("Goal", _anim.Goal());
    }

    // ジャンプ処理.
    void Jump()
    {
        

        if (_isJumpNow) return;

        _rigid.AddForce(transform.up* _jumpPower, ForceMode.Impulse);

        _soundManager.PlaySE("Jump");

        _isJumpNow = true;
    }
    // 移動処理.
    void Move()
    {
        float hori = Input.GetAxis("Horizontal");
        float speed = hori * 25.0f;// 速さ.
        Vector3 vec = new (speed, 0, 0);

        if (Input.GetKeyDown("joystick button 0"))
        {
            Jump();
        }

        // 移動しているかどうかでプレイヤーの摩擦力を変更.
        if (hori != 0)
        {
            _boxCollider.material.dynamicFriction = 0.0f;

            // 速度が10以下ならば力を加える.
            if (_rigid.velocity.x < 10.0f && _rigid.velocity.x > -10.0f)
            {
                _rigid.AddForce(vec);
            }
        }
        else if (hori == 0 && !_isJumpNow)
        {
            _boxCollider.material.dynamicFriction = 1.0f;
        }

        // 方向変更.
        if (hori < 0)
        {
            if (hori == 0) return;
            if (_isJumpNow) return;
            _isDirection = true;
        }
        else
        {
            if (hori == 0) return;
            if (_isJumpNow) return;
            _isDirection = false;
        }

        // 移動するSE
        if(hori != 0 && !_isJumpNow)
        {
            _footstepsTime++;
            if(_footstepsTime >= 50)
            {
                _soundManager.PlaySE("Fottsteps");
                _footstepsTime = 0;
            }
            
        }
    }

    // 敵に当たった時の処理.
    private void HitEnemy()
    {
        if(!_isHitEnemy) return;
        _isHitEnemy = false;
        _isDamage = true;

        Damage();
        KnockBack();
    }

    // ダメージを受けた時の処理.
    private void Damage()
    {
        _hp -= 1;

        _soundManager.PlaySE("Damage");

        if (_hp <= 0)
        {
            _hp = 0;
        }
    }

    // 敵に当たった時のノックバック.
    private void KnockBack()
    {
        _rigid.AddForce(_enemyDirection * _knockBackPower, ForceMode.Impulse);
    }

    // ダメージを受けた時の点滅処理.
    private void DamageBlinking()
    {
        _invincibleTime++;
        if (_invincibleTime % 5 == 0)
        {
            _isRendererDisplay = false;
        }
        else
        {
            _isRendererDisplay = true;
        }

        if (_invincibleTime >= _invincibleMaxTime)
        {
            _invincibleTime = 0;
            _isDamage = false;
            _isRendererDisplay = true;
        }

    }

    // Rendererの表示非表示.
    private void RendererDisplay()
    {
        for (int rendererNum = 0; rendererNum < _renderer.Length; rendererNum++)
        {
            _renderer[rendererNum].enabled = _isRendererDisplay;
        }
    }

    // ワープするときの処理.
    private void Warp()
    {
        //if (!_isMoveActive) return; 
        if (_gimmickManager._solve[0] || _gimmickManager._solve[1] ||
            _gimmickManager._solve[2] || _gimmickManager._solve[3])
            return;

        // ボタン押したら(ボタン配置は仮).
        if (Input.GetKeyDown("joystick button 3"))
        {
            // ゲートの前にいないときはスキップ.
            if (!_transitionScene.SetGateFlag()) return;
            _isMoveActive = false;
            if(!_flag._isGoal)
            {
                _soundManager.PlaySE("Warp");
            }
            else
            {
                _soundManager.PlaySE("Goal");
            }
        }
    }

    // 裏世界へワープする時の演出
    private void WarpDirect()
    {
        if (_gimmickManager._solve[0] || _gimmickManager._solve[1] ||
            _gimmickManager._solve[2] || _gimmickManager._solve[3])
            return;

        if (!_isMoveActive && !_flag._isGoal && transform.position.x <= _eventPos)
        {
            transform.position = new Vector3(_warpPosition.x + 0.0f, _warpPosition.y, transform.position.z);

            Vector3 warpPos = new Vector3(_warpPosition.x, _warpPosition.y, 0.0f);
            Vector3 velocity = Vector3.zero;

            _rigid.useGravity = false;

            transform.position = Vector3.SmoothDamp(transform.position, _warpPosition, ref velocity, 0.1f);

            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);

            if (transform.localScale.x < 0.05f && transform.localScale.y < 0.05f && transform.localScale.z < 0.05f)
            {
                transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }

            transform.RotateAround(_warpPosition, -Vector3.forward, 500 * Time.deltaTime);
        }
    }

    // ゴールした時のプレイヤーの向き.
    private void GoalPlayerDirection()
    {
        // ゴールした時に正面を向くようにする.
        if (_flag._isGoal)
        {
            Quaternion rotation = Quaternion.LookRotation(new Vector3(0.0f, 0.0f, -180.0f), Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);

            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
    }

    // 移動しているプレイヤーの向き.
    private void MovePlayerDirection()
    {
        // 右を向く.
        if (!_isDirection)
        {
            if (transform.localEulerAngles.y >= 120.0f)
            {
                transform.Rotate(0f, -10f, 0f);
            }
        }
        // 左を向く.
        else if (_isDirection)
        {
            if (transform.localEulerAngles.y <= 240.0f)
            {
                transform.Rotate(0f, 10f, 0f);
            }
        }
    }

    // 落下デバッグ用.
    private void FallDebug()
    {
        // 落ちたら初期位置に戻す.
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(-6.0f, 1.0f, 0.0f);
        }
    }

    // 体力が0になった時.
    private void GameOver()
    {
        if (_hp <= 0)
        {
            _animator.SetBool("GameOver", _anim.GameOver());
            if (_isSeGameOver) return;
            _soundManager.PlaySE("GameOver");
            _isSeGameOver = true;
        }
    }

    public int GetHp()              { return _hp; }
    public bool GetIsJumpNow()      { return _isJumpNow; }
    public bool GetIsDirection()    { return _isDirection; }
    public bool GetIsMoveActive()      { return _isMoveActive; }
}
