// 3DプレイヤーのHPをUIに反映.

using UnityEngine;
using UnityEngine.UI;

public class Player3DHpUi : MonoBehaviour
{
    // テキスト
    private Text _text;
    // 対象キャラクター
    private Player3DMove _player;

    void Start()
    {
        _text = GetComponent<Text>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player3DMove>();
    }

    private void FixedUpdate()
    {
        _text.text = "X" + _player.GetHp();
    }
}
