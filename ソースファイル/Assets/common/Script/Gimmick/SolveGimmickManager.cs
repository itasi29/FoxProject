/*解いたギミックの真偽*/
using UnityEngine;

public class SolveGimmickManager : MonoBehaviour
{
    // ギミックを解いたかどうか
    public bool[] _solve = new bool[4];

    // ギミック全体の処理
    public bool GetSolve()
    {
        return _solve[0] || _solve[1] || _solve[2] || _solve[3];
    }

    // 全ギミッククリア
    public bool GetAllClear()
    {
        return _solve[0] && _solve[1] && _solve[2] && _solve[3];
    }
}
