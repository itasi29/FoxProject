/*3Dプレイヤーの挙動*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class Player3DMove : MonoBehaviour
{
    // カメラ.
    private GameObject _camera;
    // ゲートにあったっているかのフラグ.
    private GateFlag _transitionScene;
    // 3Dプレイヤーのアニメーション.
    private PlayerAnim3D _anim3D;
    // アニメーション.
    private Animator _animator;
    // 物理演算.
    private Rigidbody _rigidbody;
    // transformをキャッシュ.
    private Transform _transform;
    // ポーズ画面
    private UpdatePause _pause;

    // レンダーマテリアル.
    public Renderer[] _renderer;

    private SoundManager _soundManager;

    // 体力.
    public int _hp = 3;

    // ダメージを受けた後の無敵時間.
    private int _invincibleTime;
    // ダメージを受けた後の最大無敵時間.
    public int _invincibleMaxTime = 120;

    // 足音のするタイミング.
    private int _footstepsTime = 0;

    // 着地しているかどうか.
    public bool _isGround;

    // 移動スピード.
    [SerializeField] private float _speed = 5;

    // ジャンプ力.
    [SerializeField] private float _jumpPower;

    // 移動方向.
    private Vector3 _moveDirection = Vector3.zero;
    Vector3 vector = Vector3.zero;

    private RaycastHit hit; // レイが何かに当たった時の情報.

    // 操作可能かどうか.
    public bool _isController = true;
    // ジャンプ可能かどうか.
    public bool _isJumpController = true;
    // ダメージを受けたかどうか.
    private bool _isDamage = false;

    [Header("身体にめり込ませるRayの長さ")]
    [SerializeField] private float _rayOffset;

    [Header("円のRayの長さ")]
    [SerializeField] private float _raySphereLength = 0.1f;

    [Header("円のy座標調整")]
    [SerializeField] private float _SphereCastRegulationY = 0;

    // SphereCastの中心座標
    private Vector3 _SphereCastCenterPosition = Vector3.zero;

    // 地面との距離.
    [SerializeField] private float _distanceGround;
    // 足煙.
    [SerializeField] private ParticleSystem _particleSystem;

    // Rendererの表示、非表示.
    private bool _isRendererDisplay = true;
    
    float currentGravity = -0.1f;

    private bool _isPlaySe = false;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (_pause._isPause) return;
        if (Input.GetKeyDown("joystick button 3"))
        {
            // ゲートの前にいないときはスキップ.
            if (!_transitionScene.SetGateFlag()) return;

            _soundManager.PlaySE("Warp");
            _isController = false;
        }

        if(_anim3D.GameOver())
        {
            _isController = false;
            _animator.SetBool("isDead", _anim3D.GameOver());
            if(!_isPlaySe)
            {
                _soundManager.PlaySE("GameOver");
                _isPlaySe = true;
            }
            
        }

        if (!_isController) return;
        MoveDirection();
        if(_isJumpController)
        {
            Jump();
        }
        Anim();
        //FallDebug();

        // 土煙のエフェクトを着地している間に再生.
        if(IsGroundShpere() && _hp > 0)
        {
            _particleSystem.Play();
        }
        else if(!IsGroundShpere() || _hp <= 0)
        {
            _particleSystem.Stop();
        }

    }

    private void FixedUpdate()
    {
        if(_pause._isPause) return;
        _SphereCastCenterPosition = 
            new Vector3(_transform.position.x,
            _transform.position.y + _SphereCastRegulationY,
            _transform.position.z);

        _isGround = IsGroundShpere();

        if(SceneManager.GetActiveScene().name == "Gimmick1_3_2" ||
           SceneManager.GetActiveScene().name == "Gimick1_3_4" ||
           SceneManager.GetActiveScene().name == "GimmickRoad3_1" ||
           SceneManager.GetActiveScene().name == "GimmickRoad3_2" ||
           SceneManager.GetActiveScene().name == "GimmickRoad3_3" ||
           SceneManager.GetActiveScene().name == "GimmickRoad3_4")
        {
            if (_rigidbody.velocity.y <= -20.0f && IsGroundShpere())
            {
                HpDown();
                _isDamage = true;
            }
        }
        

        if(_isDamage)
        {
            DamageBlinking();
        }


        RendererDisplay();
        //Debug.Log(IsGroundShpere());

        //Debug.DrawRay(ray.origin, ray.direction * distance, Color.red); // レイを赤色で表示させる
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DamageFloor")
        {
            HpDown();
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 50, _rigidbody.velocity.z);
            _isDamage = true;

            //_rigidbody.AddForce(new Vector3(_rigidbody.velocity.x, 20, _rigidbody.velocity.z), ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        // デバッグ表示.
        // 接地.
        // true 緑.
        // false 赤.
        Gizmos.color = IsGroundShpere() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(_SphereCastCenterPosition + -transform.up * (hit.distance), _raySphereLength);
    }

    // 初期化.
    private void Init()
    {
        _transitionScene = GameObject.Find("3DPlayer").GetComponent<GateFlag>();
        _camera = GameObject.Find("Camera");
        _anim3D = GetComponent<PlayerAnim3D>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _pause = GameObject.Find("PauseSystem").GetComponent<UpdatePause>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }


    // 回転を含む移動処理
    private void MoveDirection()
    {
        // 垂直方向.
        float vertical = Input.GetAxis("Vertical");
        // 水平方向.
        float horizontal = Input.GetAxis("Horizontal");

        
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        float Gravity = 1.1f;

        

        currentGravity *= Gravity;

        if(currentGravity <= -20.0f)
        {
            currentGravity = -20.0f;
        }

        // プレイヤーの回転.
        _transform.forward = Vector3.Slerp(_transform.forward, _moveDirection, Time.deltaTime * 10.0f);

        // カメラの角度によって正面方向を変える.
        _moveDirection = _speed * (cameraRight.normalized * horizontal + cameraForward.normalized * vertical);

        // プレイヤーの移動.
        _rigidbody.velocity = new Vector3(_moveDirection.x, _rigidbody.velocity.y, _moveDirection.z);

        if(_isGround && (vertical != 0 || horizontal != 0))
        {
            _footstepsTime++;
            if (_footstepsTime >= 50)
            {
                _soundManager.PlaySE("Fottsteps");
                _footstepsTime = 0;
            }
        }
    }

    // ジャンプ処理.
    private void Jump()
    {
        if(Input.GetKeyDown("joystick button 0"))
        {
            if (IsGroundShpere())
            {
                _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _soundManager.PlaySE("Jump");
            }
        }
    }

    // Rayが接地するかどうか.
    // 円
    public bool IsGroundShpere()
    {
        Ray ray = new(origin: transform.position + Vector3.up * _rayOffset, direction: Vector3.down);

        // 円キャスト.
        Physics.SphereCast(_SphereCastCenterPosition, _raySphereLength, -transform.up, out hit);

        // 接地距離によってtrue.
        if (hit.distance <= _distanceGround)
        {
            return true;
        }

        
        return false;
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

    // 体力を減らす.
    private void HpDown()
    {
        if (_isDamage) return;
        _soundManager.PlaySE("Damage");
        _hp--;
    }

    // アニメーションの処理.
    private void Anim()
    {
        _animator.SetBool("Run", _anim3D.Run());
        _animator.SetBool("Jump", _anim3D.Jump());
        _animator.SetBool("Push", _anim3D.Push());
        _animator.SetBool("isDead", _anim3D.GameOver());
    }

    // 操作できるかどうか.
    public bool GetIsMoveActive() { return _isController; }

    // Hp.
    public int GetHp() { return _hp; }
}
