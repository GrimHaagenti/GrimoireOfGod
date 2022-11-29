using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    public enum Scenes { LAUNCHER, WORLD, BATTLE};

    [SerializeField] Scenes LaunchScene;
    [SerializeField] bool IsLauncherScene;
    public static GameSceneManager _SCENE_MANAGER;
    private void Awake()
    {
        if (_SCENE_MANAGER != null && _SCENE_MANAGER != this)
        {
            Destroy(_SCENE_MANAGER);
        }
        else { _SCENE_MANAGER = this; }

        PlayerScript.OnBattleStarted += TransitionToBattle;


        DontDestroyOnLoad(this);

    }
    private void Start()
    {

        if (IsLauncherScene) {
            SceneManager.LoadScene((int)LaunchScene);
            GameManager._GAME_MANAGER.FindPlayerInScene();
                }
    }


    private void TransitionToBattle()
    {
        SceneManager.LoadScene((int)Scenes.BATTLE);
    }

}
