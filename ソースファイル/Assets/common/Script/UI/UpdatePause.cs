// ポーズ画面の処理.

using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdatePause : MonoBehaviour
{
    // ポーズ画面を開いたかどうか.
    public bool _isPause = false;
    // ポーズ画面のオブジェクト
    [SerializeField] private GameObject _pauseCanvas;
    private SoundManager _soundManager;

    // 説明用クラス
    private TipsDrawer _tipsDrawer;

    // 現在のタイムスケールを取得する
    public float _timeScale { get;  set; }
    
    // Tipsをひらけるかどうか.
    private bool _isOpenTips = false;

    void Start()
    {
        _tipsDrawer = GameObject.Find("Tips").GetComponent<TipsDrawer>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ画面を開いているかどうか.
        _pauseCanvas.gameObject.SetActive(_isPause);

        // 時間を動かす.
        if (!_isPause)
        {
            Time.timeScale = 1;
        }

        // Start
        if (Input.GetKeyDown("joystick button 7"))
        {
            _isPause = true;
        }
        // B
        if (Input.GetKeyUp("joystick button 1"))
        {
            // 説明が出ている場合
            if (_tipsDrawer._isSlideEnd)
            {
                _tipsDrawer.IsUpSlider();
            }
            else
            {
                _isPause = false;
            }
        }

        _isOpenTips = SceneManager.GetActiveScene().name == "MainScene1-1" ||
                    SceneManager.GetActiveScene().name == "MainScene1-2" ||
                    SceneManager.GetActiveScene().name == "MainScene1-3" ||
                    SceneManager.GetActiveScene().name == "GimmickRoad3_1" ||
                    SceneManager.GetActiveScene().name == "GimmickRoad3_2" ||
                    SceneManager.GetActiveScene().name == "GimmickRoad3_3" ||
                    SceneManager.GetActiveScene().name == "GimmickRoad3_4";

        // 時間がとまっている場合
        if (_isPause)
        {
            // 画像表示
            if (Input.GetKeyDown("joystick button 3"))
            {
                if (_isOpenTips) return;
                _tipsDrawer.IsDownSlider();
            }

        }

        // タイムスケールの取得
        _timeScale = Time.timeScale;  
    }

    private void FixedUpdate()
    {
        // 時間を止める.
        if (_isPause)
        {
            Time.timeScale = 0;
        }
    }

}
