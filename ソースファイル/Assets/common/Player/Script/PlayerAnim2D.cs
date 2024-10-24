// 2Dアニメーション

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim2D : MonoBehaviour
{
    private float _horizontal = 0;
    private Player2DMove _player;
    private Fade2DSceneTransition _flag;

    private void Start()
    {
        _player = GetComponent<Player2DMove>();
        _flag = GameObject.Find("FadeObject2D").GetComponent<Fade2DSceneTransition>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        //Debug.Log(_flag._isGoal);
    }

    //-------------------------------------------
    // アニメーション再生.
    // true: 再生.
    // false:再生しない.
    //-------------------------------------------

    // アイドル状態.
    public bool Idle()
    {
        if(_horizontal == 0 && !_player.GetIsJumpNow() &&
            _player.GetHp() != 0)
        {
            return true;
        }

        return false;
    }

    // 移動.
    public bool Run()
    {
        if(_horizontal != 0)
        {
            return true;
        }

        return false;
    }

    // ジャンプアニメーション.
    public bool Jump()
    {
        if (_player.GetIsJumpNow())
        {
            return true;
        }

        return false;
    }

    // ゲームオーバーアニメーション.
    public bool GameOver()
    {
        if(_player.GetHp() <= 0)
        {
            return true;
        }

        return false;
    }

    // ゴールアニメーション.
    public bool Goal()
    {
        if (_flag._isGoal)
        {
            return true;
        }

        return false;
    }
}
