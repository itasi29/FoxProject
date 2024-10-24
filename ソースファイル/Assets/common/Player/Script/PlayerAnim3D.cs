// 3Dプレイヤーアニメーション.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim3D : MonoBehaviour
{
    private Player3DMove _player;

    public bool _isPushing = false;

    //-------------------------------------------
    // アニメーション再生.
    // true: 再生.
    // false:再生しない.
    //-------------------------------------------

    private void Start()
    {
        _player = GetComponent<Player3DMove>();
    }

    // 移動.
    public bool Run()
    {
        // 垂直方向.
        float vertical = Input.GetAxis("Vertical");
        // 水平方向.
        float horizontal = Input.GetAxis("Horizontal");
        // スティックを傾けた.
        bool isStickTilt = vertical != 0 ||
            horizontal != 0;

        
        if(isStickTilt)
        {
            return true;
        }
        return false;
    }

    // ジャンプアニメーション.
    public bool Jump()
    {
        if (!_player._isGround)
        {
            return true;
        }
        return false;
    }

    // 押しているアニメーション.
    public bool Push()
    {
        if (_isPushing)
        {
            return true;
        }

        return false;
    }

    // ゲームオーバーアニメーション.
    public bool GameOver()
    {
        if (_player._hp <= 0)
        {
            return true;
        }
        return false;
    }

}
