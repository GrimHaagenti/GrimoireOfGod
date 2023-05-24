using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Scenes { MAINMENU, WORLD, BATTLE};

[System.Serializable]

public class UnityEvent_SCENES: UnityEvent<Scenes> { }
public class GameSceneManager 
{

    public Scene currentScene { get; private set; }
    private Scene backgroundScene;


    private bool MainSceneActivate = false;

    public Scenes currentSceneIndex { get; private set; }
    [SerializeField] bool IsLauncherScene;

    /*
    public delegate void SceneChanged();
    public static SceneChanged OnSceneChanged;
    public static SceneChanged OnSceneChangedBattle;

    public delegate void SceneLoaded();
    //public SceneLoaded OnSceneLoaded;
    
    public delegate void BattleUnloaded();
    public BattleUnloaded OnBattleUnloaded;
    */
    public UnityEvent_SCENES OnSceneLoaded;
    public UnityEvent OnBattleSceneUnloaded;



    AsyncOperation asyncLoad;
    AsyncOperation asyncUnload;


    public void Init()
    {
        OnSceneLoaded = new UnityEvent_SCENES();
        OnBattleSceneUnloaded = new UnityEvent();
    }
   

    

    public void Update()
    {
        if (asyncLoad != null) {

            if (asyncLoad.isDone)
            {
                currentScene = SceneManager.GetSceneByBuildIndex((int)currentSceneIndex);
                OnSceneLoaded?.Invoke(currentSceneIndex);

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
                OnBattleSceneUnloaded?.Invoke();


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

   
    public void LoadGame()
    {
        LoadScene(Scenes.WORLD, LoadSceneMode.Single, true);
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
        currentSceneIndex = Scenes.WORLD;


    }

    private void AsyncUnload()
    {
        asyncUnload = SceneManager.UnloadSceneAsync(currentScene);
    }

}
