using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes { LAUNCHER, WORLD, BATTLE };

[System.Serializable]
public class GameSceneManager 
{

    [SerializeField] Scenes LaunchScene;

    public Scene currentScene { get; private set; }
    public Scenes currentSceneIndex { get; private set; }
    [SerializeField] bool IsLauncherScene;

    public delegate void SceneChanged();
    public static SceneChanged OnSceneChanged;
    public static SceneChanged OnSceneChangedBattle;


    public delegate void SceneLoaded();
    public SceneLoaded OnSceneLoaded;

    AsyncOperation asyncLoad;


    private void Awake()
    {
        

        PlayerScript.OnBattleStarted += TransitionToBattle;

        SceneManager.sceneLoaded += _SceneChanged;
    }
    public void Init()
    {

        if (IsLauncherScene)
        {
            LoadScene(LaunchScene, LoadSceneMode.Single);
        }
    }


    public void Update()
    {
       if (asyncLoad == null) { return; }

        if (asyncLoad.isDone)
        {
            currentScene = SceneManager.GetSceneByBuildIndex((int)currentSceneIndex);
            OnSceneLoaded.Invoke();
            asyncLoad = null;
        }
    }
    public void LoadScene(Scenes scene, LoadSceneMode mode)
    {

       currentSceneIndex = scene;
       asyncLoad =  SceneManager.LoadSceneAsync((int)scene, mode);
    }

 


    private void _SceneChanged(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene == SceneManager.GetSceneByBuildIndex((int)Scenes.BATTLE)){
            OnSceneChangedBattle.Invoke();
        }
        else
        {
            OnSceneChanged.Invoke();
        }
    }

    private void TransitionToBattle()
    {
        SceneManager.LoadScene((int)Scenes.BATTLE, LoadSceneMode.Additive);
        
        
    }

}
