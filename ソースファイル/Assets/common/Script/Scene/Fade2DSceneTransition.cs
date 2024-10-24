// 2D表ステージからのシーン遷移処理.
// TODO:マジックナンバーあり.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade2DSceneTransition : MonoBehaviour
{
    // プレイヤー.
    private Player2DMove _player;
    // プレイヤーがどのゲート前にいるか.
    private GateFlag _transitionScene;
    // シーン遷移マネージャー.
    private SceneTransitionManager _sceneTransitionManager;
    // フェード.
    private Fade _fade;
    // フェードアニメーション管理.
    private FadeAnimDirector _fadeDirector;
    // ポーズ画面.
    private UpdatePause _pause;
    // ギミックマネージャー.
    private SolveGimmickManager _solveGimmickManager;
    // サウンドマネージャー
    private SoundManager _soundManager;

    // ゴールしたタイミング.
    public bool _isGoal;

    // 次のシーンへ行く時のカウント.
    private int _nextSceneCount = 0;
    // カウント開始しているかどうか.
    private bool _isCount = false;

    // シーン遷移で共通しているフラグ取得.
    public bool _transitionFlagCommon = false;

    void Start()
    {
        // 初期化.
        Init();
    }

    void Update()
    {
        // 共通フラグ.
        //_transitionFlagCommon = _color.a >= 0.9f && !_player.GetIsMoveActive();

        
        // シーン遷移関数.
        SceneTransition();

        // ゴールしたかどうか.
        if ((_transitionScene._isGoal1_1 || _transitionScene._isGoal1_2) && Input.GetKeyDown("joystick button 3"))
        {
            _isGoal = true;
        }
    }

    private void FixedUpdate()
    {
        // 共通フラグ.
        _transitionFlagCommon = _fade.cutoutRange == 1 && !_player.GetIsMoveActive();
        // ゲームオーバー.
        GameOverSceneTransition();
    }

    // 初期化.
    private void Init()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player2DMove>();
        _fade = GetComponentInChildren<Fade>();
        _fadeDirector = GetComponentInChildren<FadeAnimDirector>();
        _isGoal = false;
        _transitionScene = GameObject.FindWithTag("Player").GetComponent<GateFlag>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _pause = GameObject.Find("PauseSystem").GetComponent<UpdatePause>();
        _solveGimmickManager = GameObject.Find("GimmickManager").GetComponent<SolveGimmickManager>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // シーン遷移
    private void SceneTransition()
    {
        // ポーズ画面を開いていたら止める.
        if (_pause._isPause) return;
        if (_solveGimmickManager._solve[0] || _solveGimmickManager._solve[1] ||
            _solveGimmickManager._solve[2] || _solveGimmickManager._solve[3])
            return;

        // ボタン押したら(ボタン配置は仮).
        if (Input.GetKeyDown("joystick button 3"))
        {
            // ゲートの前にいないときはスキップ.
            if (!_transitionScene.SetGateFlag()) return;

            // デバッグ用スキップ.
            if (_transitionScene._isGoal1_2)
            {
                _nextSceneCount = 120;
            }
            _isCount = true;
        }

        if(_isCount)
        {
            _nextSceneCount--;
        }

        if (_nextSceneCount <= 0 && _isCount)
        {
            _fadeDirector._isFade = true;
        }

        if(_transitionFlagCommon)
        {
            SceneManager.sceneLoaded += GameSceneLoaded;
        }

        

        // シーン遷移.
        if (_transitionScene._isGateGimmick1_1 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_1_1();
        }
        else if (_transitionScene._isGateGimmick1_2 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_1_2();
        }
        else if (_transitionScene._isGateGimmick2_1 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2_1();
        }
        else if (_transitionScene._isGateGimmick2_2 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2_2();
        }
        else if (_transitionScene._isGateGimmick2_3 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2_3();
        }
        else if (_transitionScene._isGateGimmick2_4 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2_4();
        }
        else if(_transitionScene._isGateRoad3_1 && _transitionFlagCommon)
        {
            _sceneTransitionManager.GimmickRoad3_1();
        }
        else if(_transitionScene._isGateGimmick3_1 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3_1();
        }
        else if (_transitionScene._isGateRoad3_2 && _transitionFlagCommon)
        {
            _sceneTransitionManager.GimmickRoad3_2();
        }
        else if (_transitionScene._isGateGimmick3_2 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3_2();
        }
        else if (_transitionScene._isGateRoad3_3 && _transitionFlagCommon)
        {
            _sceneTransitionManager.GimmickRoad3_3();
        }
        else if (_transitionScene._isGateGimmick3_3 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3_3();
        }
        else if (_transitionScene._isGateRoad3_4 && _transitionFlagCommon)
        {
            _sceneTransitionManager.GimmickRoad3_4();
        }
        else if (_transitionScene._isGateGimmick3_4 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3_4();
        }
        else if(_transitionScene._isGoal1_1 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2();
        }
        else if (_transitionScene._isGoal1_2 && _transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3();
        }
        else if (_transitionScene._isGoal1_3 && _transitionFlagCommon)
        {
            _sceneTransitionManager.EndScene();
        }

        

    }

    // 体力が0になった時の処理.
    private void GameOverSceneTransition()
    {
        // 生きていたら処理を通さない.
        if (_player._hp > 0) return;

        _nextSceneCount++;
        if(_nextSceneCount >= 300)
        {
            _fadeDirector._isFade = true;
        }


        //Debug.Log(_transitionFlagCommon);
        if(SceneManager.GetActiveScene().name == "MainScene1-1" && _fade.cutoutRange == 1)
        {
            _sceneTransitionManager.MainScene1_1();
        }
        else if(SceneManager.GetActiveScene().name == "MainScene1-2" && _fade.cutoutRange == 1)
        {
            _sceneTransitionManager.MainScene1_2();
        }
    }

    // シーン切り替え時に呼ぶ.
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        if(_transitionScene._isGoal1_1)
        {
            // 切り替え先のスクリプト取得
            Player2DMove player2D = GameObject.FindWithTag("Player").GetComponent<Player2DMove>();

            // 残機を一つ増やす.
            _player._hp = _player._hp + 1;

            // hpの引継ぎ.
            player2D._hp = _player.GetHp();
        }
        else if(_transitionScene._isGoal1_2)
        {
            // 切り替え先のスクリプト取得
            Player3DMove player3D = GameObject.FindWithTag("Player").GetComponent<Player3DMove>();

            // 残機を一つ増やす.
            _player._hp = _player._hp + 1;

            // hpの引継ぎ.
            player3D._hp = _player.GetHp();
        }
        else if(!_transitionScene._isGoal1_1 && !_transitionScene._isGoal1_2)
        {
            // 切り替え先のスクリプト取得
            Player3DMove player3D = GameObject.FindWithTag("Player").GetComponent<Player3DMove>();

            // hpの引継ぎ.
            player3D._hp = _player.GetHp();
        }

        _soundManager.StopSe();
        _soundManager.StopBgm();
        // 削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
