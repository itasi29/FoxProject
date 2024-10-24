// 3D視点からのゲームオーバー.

using UnityEngine;
using UnityEngine.UI;

public class GameOverUi2D : MonoBehaviour
{
    // 2Dプレイヤー.
    private Player2DMove _player2D;
    // UIの色.
    private Color _color;

    void Start()
    {
        _player2D = GameObject.FindWithTag("Player").GetComponent<Player2DMove>();
        _color = gameObject.GetComponent<Image>().color;
        
    }

    private void FixedUpdate()
    {
        if (_player2D.GetHp() <= 0)
        {
            _color.a += 0.01f;
            gameObject.GetComponent<Image>().color = _color;
        }
    }
}
