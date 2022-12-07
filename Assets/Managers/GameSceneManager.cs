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
    private Scene backgroundScene;


    private bool MainSceneActivate = false;

    public Scenes currentSceneIndex { get; private set; }
    [SerializeField] bool IsLauncherScene;

    public delegate void SceneChanged();
    public static SceneChanged OnSceneChanged;
    public static SceneChanged OnSceneChangedBattle;

    public delegate void ActiveSceneBackToMain();
    public ActiveSceneBackToMain OnActiveSceneBackToMain;

    public delegate void SceneLoaded();
    public SceneLoaded OnSceneLoaded;
    
    public delegate void BattleUnloaded();
    public BattleUnloaded OnBattleUnloaded;

    AsyncOperation asyncLoad;
    AsyncOperation asyncUnload;


    public void Init()
    {

        if (IsLauncherScene)
        {
            LoadScene(LaunchScene, LoadSceneMode.Additive, true);

        }
        SceneManager.sceneLoaded += HasTerminado;
        SceneManager.sceneUnloaded += HasTerminadoparte2;
    }
    public void HasTerminadoparte2(Scene sc)
    {
        Debug.Log(sc.name);

    }
    public void HasTerminado(Scene sc, LoadSceneMode mode)
    {
        Debug.Log(sc.name);

    }

    public void Update()
    {
        if (asyncLoad != null) {

            if (asyncLoad.isDone)
            {
                currentScene = SceneManager.GetSceneByBuildIndex((int)currentSceneIndex);
                OnSceneLoaded.Invoke();

                asyncLoad = null;
            }
        }

        if (asyncUnload != null)
        {
            if (asyncUnload.isDone)
            {
                currentScene = backgroundScene;

                foreach (GameObject obj in currentScene.GetRootGameObjects())
                {
                    obj.SetActive(true);
                }
                OnBattleUnloaded.Invoke();


                asyncUnload = null;
            }
        }

        if(backgroundScene != null)
        {
            if(SceneManager.GetActiveScene() == backgroundScene && MainSceneActivate)
            {


                AsyncUnload();
                MainSceneActivate = false;


            }

        }
        
    }

   

    public void LoadScene(Scenes scene, LoadSceneMode mode, bool bypassDeactivate=false)
    {

       currentSceneIndex = scene;
        if (!bypassDeactivate)
        {
            foreach (GameObject obj in currentScene.GetRootGameObjects())
            {
                obj.SetActive(false);
            }
        }
        if(mode == LoadSceneMode.Additive)
        {
            backgroundScene = currentScene;
        }
       asyncLoad =  SceneManager.LoadSceneAsync((int)scene, mode);
    }
    
 
    public void UnloadBattleScene()
    {
        if(backgroundScene == null) { return; }
        SceneManager.SetActiveScene(backgroundScene);
        MainSceneActivate = true;
        
    }

    private void AsyncUnload()
    {
        asyncUnload = SceneManager.UnloadSceneAsync(currentScene);
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

   
}
