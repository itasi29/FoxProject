// ポーズ画面を開く閉じる操作.
// 何もなかったのでとりあえずここで、BGMを流す処理しています。
// HACK:ポーズ画面を雑に作ったのでコーディング規約だけ見てほしい.

using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private SoundManager _sound;
    [SerializeField] private string bgmName;
    public bool _getResult = false;
    [SerializeField] private FadeAnimDirector _animDirector = null;

    private Text _text;

    void Start()
    {
        _sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_getResult || IsFadeCheck())
        {
            _sound.StopBgm();
        }
        else
        {
            _sound.PlayBGM(bgmName);
        }
    }
    private bool IsFadeCheck()
    {
        if(_animDirector != null)
        {
            return _animDirector._isFade;
        }
        return false;
    }
}
