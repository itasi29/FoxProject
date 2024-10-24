// ゲートの当たり判定.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateFlag : MonoBehaviour
{
    // シーン遷移の真偽.
    // 1-1
    public bool _isGateGimmick1_1;
    public bool _isGateGimmick1_2;
    // 1-2
    public bool _isGateGimmick2_1;
    public bool _isGateGimmick2_2;
    public bool _isGateGimmick2_3;
    public bool _isGateGimmick2_4;
    // 1-3
    public bool _isGateMainScene1_3;
    public bool _isGateRoad3_1;
    public bool _isGateGimmick3_1;
    public bool _isGateRoad3_2;
    public bool _isGateGimmick3_2;
    public bool _isGateRoad3_3;
    public bool _isGateGimmick3_3;
    public bool _isGateRoad3_4;
    public bool _isGateGimmick3_4;


    // ゴールについたかどうか
    public bool _isGoal1_1;
    public bool _isGoal1_2;
    public bool _isGoal1_3;

    void Start()
    {
        // 初期化.
        _isGateGimmick1_1 = false;
        _isGateGimmick1_2 = false;
        _isGoal1_1 = false;
    }

    void Update()
    {
        //Debug.Log($"{name}");
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);

        // 扉の当たり判定に入ったらture.
        if (other.tag == "Gimmick1_1")
        {
            _isGateGimmick1_1 = true;
        }
        else if(other.tag == "Gimmick1_2")
        {
            _isGateGimmick1_2 = true;
        }
        else if(other.tag == "Gimmick2_1")
        {
            _isGateGimmick2_1 = true;
        }
        else if (other.tag == "Gimmick2_2")
        {
            _isGateGimmick2_2 = true;
        }
        else if (other.tag == "Gimmick2_3")
        {
            _isGateGimmick2_3 = true;
        }
        else if (other.tag == "Gimmick2_4")
        {
            _isGateGimmick2_4 = true;
        }
        else if(other.tag == "MainScene1_3")
        {
            _isGateMainScene1_3 = true;
        }
        else if(other.tag == "GimmickRoad3_1")
        {
            _isGateRoad3_1 = true;
        }
        else if (other.tag == "Gimmick3_1")
        {
            _isGateGimmick3_1 = true;
        }
        else if (other.tag == "GimmickRoad3_2")
        {
            _isGateRoad3_2 = true;
        }
        else if (other.tag == "Gimmick3_2")
        {
            _isGateGimmick3_2 = true;
        }
        else if (other.tag == "GimmickRoad3_3")
        {
            _isGateRoad3_3 = true;
        }
        else if (other.tag == "Gimmick3_3")
        {
            _isGateGimmick3_3 = true;
        }
        else if (other.tag == "GimmickRoad3_4")
        {
            _isGateRoad3_4 = true;
        }
        else if (other.tag == "Gimmick3_4")
        {
            _isGateGimmick3_4 = true;
        }
        else if(other.tag == "Goal1_1")
        {
            _isGoal1_1 = true;
        }
        else if(other.tag == "Goal1_2")
        {
            _isGoal1_2 = true;
        }
        else if( other.tag == "Goal1_3")
        {
            _isGoal1_3 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 扉の当たり判定を出たらfalse.
        if (other.tag == "Gimmick1_1")
        {
            _isGateGimmick1_1 = false;
        }
        else if (other.tag == "Gimmick1_2")
        {
            _isGateGimmick1_2 = false;
        }
        else if (other.tag == "Gimmick2_1")
        {
            _isGateGimmick2_1 = false;
        }
        else if (other.tag == "Gimmick2_2")
        {
            _isGateGimmick2_2 = false;
        }
        else if (other.tag == "Gimmick2_3")
        {
            _isGateGimmick2_3 = false;
        }
        else if (other.tag == "Gimmick2_4")
        {
            _isGateGimmick2_4 = false;
        }
        else if (other.tag == "MainScene1_3")
        {
            _isGateMainScene1_3 = false;
        }
        else if (other.tag == "GimmickRoad3_1")
        {
            _isGateRoad3_1 = false;
        }
        else if (other.tag == "Gimmick3_1")
        {
            _isGateGimmick3_1 = false;
        }
        else if (other.tag == "GimmickRoad3_2")
        {
            _isGateRoad3_2 = false;
        }
        else if (other.tag == "Gimmick3_2")
        {
            _isGateGimmick3_2 = false;
        }
        else if (other.tag == "GimmickRoad3_3")
        {
            _isGateRoad3_3 = false;
        }
        else if (other.tag == "Gimmick3_3")
        {
            _isGateGimmick3_3 = false;
        }
        else if (other.tag == "GimmickRoad3_4")
        {
            _isGateRoad3_4 = false;
        }
        else if (other.tag == "Gimmick3_4")
        {
            _isGateGimmick3_4 = false;
        }
        else if (other.tag == "Goal1_1")
        {
            _isGoal1_1 = false;
        }
        else if (other.tag == "Goal1_2")
        {
            _isGoal1_2 = false;
        }
        else if (other.tag == "Goal1_3")
        {
            _isGoal1_3 = false;
        }
    }


    // ゲートの前にいるかの状態.
    public bool SetGateFlag()
    {
        return _isGateGimmick1_1 ||
            _isGateGimmick1_2 ||
            _isGateGimmick2_1 ||
            _isGateGimmick2_2 ||
            _isGateGimmick2_3 ||
            _isGateGimmick2_4 ||
            _isGateMainScene1_3 ||
            _isGateRoad3_1 ||
            _isGateGimmick3_1 ||
            _isGateRoad3_2 ||
            _isGateGimmick3_2 ||
            _isGateRoad3_3 ||
            _isGateGimmick3_3 ||
            _isGateRoad3_4 ||
            _isGateGimmick3_4 ||
            _isGoal1_1 ||
            _isGoal1_2 ||
            _isGoal1_3;
    }

}
