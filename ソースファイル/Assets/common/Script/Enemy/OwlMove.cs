using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlMove : MonoBehaviour
{
    private Rigidbody _rigidbody;

    // 与える力.
    private Vector3 _force;
    // 最大値.
    public float _maxForce;
    // 最小値.
    public float _minForce;

    // 力を逆転させるタイミング.
    public int _powerChangeTime;
    // 現在の時間.
    private int _currentTime = 0;

    // 上か下か.
    private bool _isUp;


    SolveGimmickManager _solveGimmickManager;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _force.y = _maxForce;
        _solveGimmickManager = GameObject.FindWithTag("GimmickManager").GetComponent<SolveGimmickManager>();
        _isUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_solveGimmickManager._solve[3])
        {
            this.transform.position += new Vector3(0,0.1f,0);
            //一定以上上昇したらフラグを元に戻す
            if(this.transform.position.y >32.0f)
            {
                _solveGimmickManager._solve[3] = false;
            }

        }
        else
        {
            _currentTime++;

            //        _force.y = Random.Range(_minForce, _maxForce);

            // 力を逆転させる.
            if (_currentTime > _powerChangeTime)
            {
                if (_isUp)
                {
                    _force.y = _maxForce;
                    _isUp = false;

                }
                else
                {
                    _force.y = _minForce;
                    _isUp = true;
                }

                // リセット.
                _currentTime = 0;
            }

            _rigidbody.AddForce(_force, ForceMode.Force);

            if (_rigidbody.velocity.y > 1)
            {
                _rigidbody.velocity = new Vector3(0.0f, 1.0f, 0.0f);

            }
            else if (_rigidbody.velocity.y < -1)
            {
                _rigidbody.velocity = new Vector3(0.0f, -1.0f, 0.0f);
            }
        }
        
    }
}
