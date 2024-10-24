using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver2D : MonoBehaviour
{
    // シーンの名前.
    public string[] _sceneName = new string[2] {"MainScene1-1", "MainScene1-2" };
    // プレイヤー.
    private Player2DMove _player2D;
    // シーン遷移マネージャー.
    private SceneTransitionManager _sceneTransitionManager;
    // 2D場面のシーン遷移.
    private Fade2DSceneTransition _fade2DSceneTransition;
    

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_player2D._hp > 0) return;

        if(SceneManager.GetActiveScene().name ==  _sceneName[0])
        {
            _sceneTransitionManager.MainScene1_1();
        }
        else if(SceneManager.GetActiveScene().name == _sceneName[1])
        {
            _sceneTransitionManager.MainScene1_2();
        }
    }

    // 変数の初期化
    private void Init()
    {
        _player2D = GameObject.FindWithTag("Player").GetComponent<Player2DMove>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _fade2DSceneTransition = GetComponent<Fade2DSceneTransition>();
    }
}
