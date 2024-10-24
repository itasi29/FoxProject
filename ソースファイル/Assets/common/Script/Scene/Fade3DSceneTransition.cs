// 2Dのフェードインアウト処理.
// TODO:マジックナンバーあり.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade3DSceneTransition : MonoBehaviour
{
    private Player3DMove _player;

    private GateFlag _transitionScene;

    private SceneTransitionManager _sceneTransitionManager;

    private Fade _fade;
    private FadeAnimDirector _fadeDirector;

    // ゴールしたタイミング
    public bool _isGoal;

    // 次のシーンへ行く時のカウント.
    private int _nextSceneCount = 0;

    // ゲートのボタンを押したかどうか.
    public bool _isPush;

    // DontDestroyOnLoadを消す用の変数
    //public GameObject _eraseObject;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player3DMove>();

        _fade = GetComponentInChildren<Fade>();
        _fadeDirector = GetComponentInChildren<FadeAnimDirector>();

        // 初期化.
        _isGoal = false;
        _isPush = false;
        _transitionScene = GameObject.FindWithTag("Player").GetComponent<GateFlag>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // シーン遷移関数.
        SceneTransition();

        GameOverSceneTransition();
    }

    // シーン遷移
    private void SceneTransition()
    {
        // ボタン押したら(ボタン配置は仮).
        if (Input.GetKeyDown("joystick button 3"))
        {
            // ゲートの前にいないときはスキップ.
            if (!_transitionScene.SetGateFlag()) return;
            _fadeDirector._isFade = true;
        }

        // 共通フラグ
        bool transitionFlagCommon = _fade.cutoutRange == 1.0f && !_player.GetIsMoveActive();

        // シーン遷移.
        if (_transitionScene._isGateGimmick1_1 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_1_1();
        }
        else if (_transitionScene._isGateGimmick1_2 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_1_2();
        }
        else if (_transitionScene._isGateGimmick2_1 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2_1();
        }
        else if (_transitionScene._isGateGimmick2_2 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2_2();
        }
        else if (_transitionScene._isGateGimmick2_3 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2_3();
        }
        else if (_transitionScene._isGateGimmick2_4 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2_4();
        }
        else if (_transitionScene._isGateRoad3_1 && transitionFlagCommon)
        {
            _sceneTransitionManager.GimmickRoad3_1();
        }
        else if (_transitionScene._isGateGimmick3_1 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3_1();
        }
        else if (_transitionScene._isGateRoad3_2 && transitionFlagCommon)
        {
            _sceneTransitionManager.GimmickRoad3_2();
        }
        else if (_transitionScene._isGateGimmick3_2 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3_2();
        }
        else if (_transitionScene._isGateRoad3_3 && transitionFlagCommon)
        {
            _sceneTransitionManager.GimmickRoad3_3();
        }
        else if (_transitionScene._isGateGimmick3_3 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3_3();
        }
        else if (_transitionScene._isGateRoad3_4 && transitionFlagCommon)
        {
            _sceneTransitionManager.GimmickRoad3_4();
        }
        else if (_transitionScene._isGateGimmick3_4 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3_4();
        }
        else if (_transitionScene._isGoal1_1 && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_2();
        }
        else if ((_transitionScene._isGoal1_2 || _transitionScene._isGateMainScene1_3) && transitionFlagCommon)
        {
            _sceneTransitionManager.MainScene1_3();
        }
        else if (_transitionScene._isGoal1_3 && transitionFlagCommon)
        {
            //SceneManager.MoveGameObjectToScene(_eraseObject, SceneManager.GetActiveScene());
            _sceneTransitionManager.ClearScene();
        }

        if ((_transitionScene._isGoal1_1 || _transitionScene._isGoal1_2) && Input.GetKeyDown("joystick button 3"))
        {
            _isGoal = true;
        }

        

    }

    // 体力が0になった時の処理.
    private void GameOverSceneTransition()
    {
        // 生きていたら処理を通さない.
        if (_player.GetHp() > 0) return;

        _nextSceneCount++;
        if(_nextSceneCount >= 300)
        {
            _fadeDirector._isFade = true;
        }

        // いずれかのシーンであるかないか.
        bool isEitherScene = SceneManager.GetActiveScene().name == "GimmickRoad3_1" ||
            SceneManager.GetActiveScene().name == "GimmickRoad3_2" ||
            SceneManager.GetActiveScene().name == "GimmickRoad3_3" ||
            SceneManager.GetActiveScene().name == "GimmickRoad3_4";

        if (isEitherScene && _fade.cutoutRange == 1.0f)
        {
            _sceneTransitionManager.MainScene1_3();
        }

    }
}
