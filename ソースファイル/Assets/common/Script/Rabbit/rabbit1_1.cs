// ウサギの1-1の演出

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbit1_1 : MonoBehaviour
{
    // プレイヤーの情報を取得.
    [SerializeField] private GameObject _player;

    private Player2DMove _move;
    private GoalMove1_1 _goal;

    private SoundManager _soundManager;

    // アニメーション.
    private bool _jumping = false;
    private bool _jumpMoving = false;

    private Animator _animator;

    // アニメーション時間.
    private int _animTime = 0;
    // アニメーション最大再生時間.
    private int _maxAnimTime = 160;

    // 現在のインターバル.
    private int _moveInterval = 0;
    // 動けるの時間.
    private int _moveTime = 60;
    // 動けない時間.
    private int _dontMoveTime = 10;
    // 動ける最大時間.
    private int _moveMaxtime = 75;
    // SEを鳴らすタイミング
    private int _playSeTime = 70;

    // SEが鳴ったらtrueならない.
    private bool _isPalySe;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _move = _player.GetComponent<Player2DMove>();
        _goal = GameObject.Find("goal_02").GetComponent<GoalMove1_1>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _isPalySe = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    private void FixedUpdate()
    {
        Anim();

        //Debug.Log("_moveInterval" + _moveInterval);

        // プレイヤーが近づいたらアニメーション開始
        if (_player.transform.position.x >= 145.0f && _animTime <= _maxAnimTime)
        {
            _move._isMoveActive = false;
            _jumping = true;
            _animTime++;
            _playSeTime++;
            if (_playSeTime > 70)
            {
                _soundManager.PlaySE("Jump");
                _playSeTime = 0;
            }
        }
        // その場でジャンプ.
        else if (_animTime >= _maxAnimTime)
        {
            _jumping = false;
            
        }

        // ジャンプアニメーションをしながら移動.
        if (_animTime >= _maxAnimTime && !_jumping)
        {
            if (transform.localEulerAngles.y >= 100)
            {
                transform.Rotate(0f, -10f, 0f);
            }
            else
            {
                _jumping = true;

                _moveInterval++;

                if (_moveInterval < _moveTime && _moveInterval > _dontMoveTime)
                {
                    transform.position += new Vector3(0.4f, 0.0f, 0.0f);
                    if(!_isPalySe)
                    {
                        _soundManager.PlaySE("Jump");
                        _isPalySe = true;
                    }
                    
                }
                else if (_moveInterval > _moveMaxtime)
                {
                    _moveInterval = 0;
                }
                //transform.position += new Vector3(0.1f, 0.0f, 0.0f);
            }
        }

        if (transform.position.x >= 170)
        {
            _move._isMoveActive = true;
            _goal._eventFlag = true;
        }

        if (transform.position.x >= 180)
        {
            Destroy(gameObject);
        }
    }

    // アニメーション.
    private void Anim()
    {
        _animator.SetBool("Jump", _jumping);
        _animator.SetBool("JumpMove", _jumpMoving);
    }

}
