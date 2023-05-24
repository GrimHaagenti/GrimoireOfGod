using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager _GAME_MANAGER = null;
    public GameSceneManager _SCENE_MANAGER = null;


    [SerializeField] private CameraScript cam;

    [SerializeField] ItemDB items;
    public GameObject playerPrefab;
    public GameObject PlayerObj { get; private set; }
    public GameObject CameraObj { get; private set; }

    public New_Entity_Script player;

    public UnityEvent LevelLoaded;
    public UnityEvent ReturningToWorld;
    public UnityEvent CameraIHateU;

    private List<GameObject> EnemiesPrefab;
    public Levelnfo currentLevelInfo;
    private Vector3 lastPlayerPosition;
    private Quaternion lastPlayerRotation;


    private void Awake()
    {
        if (_GAME_MANAGER != null && _GAME_MANAGER != this)
        {
            Destroy(_GAME_MANAGER);
        }
        else { _GAME_MANAGER = this; }

        if (_SCENE_MANAGER == null)
        {
            _SCENE_MANAGER = new GameSceneManager();
        }

        DontDestroyOnLoad(this);

        InitializeManagers();

        _SCENE_MANAGER.OnSceneLoaded.AddListener(OnSceneFinishLoaded);
        _SCENE_MANAGER.OnBattleSceneUnloaded.AddListener(OnBattleSceneFinishedUnloading);
    }

    void InitializeManagers()
    {

        _SCENE_MANAGER.Init();
        ItemManager._ITEM_MANAGER.Init(items);
        //_DB_MANAGER.Init();
        //_UI_MANAGER.Init();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _SCENE_MANAGER.Update();
    }
    public void LoadScene(Scenes scn, LoadSceneMode mode)
    {
        _SCENE_MANAGER.LoadScene(scn, mode);
    }

    public void LoadGame()
    {
        _SCENE_MANAGER.LoadGame();
    }
    public void BeginLoadBattleScene(List<GameObject> enemies_b)
    {
        InputManager._INPUT_MANAGER.SetInputToMenu();
        
        EnemiesPrefab = new List<GameObject>();
        enemies_b.ForEach(e => { EnemiesPrefab.Add(e); });
       
        New_UI_Manager._UI_MANAGER.fadePanel.BeginFadeIn();
        New_UI_Manager._UI_MANAGER.fadePanel.OnFadeInComplete.AddListener(LoadBattleScene);

    }
    private void LoadBattleScene()
    {
        New_UI_Manager._UI_MANAGER.fadePanel.OnFadeInComplete.RemoveListener(LoadBattleScene);
        LoadScene(Scenes.BATTLE, LoadSceneMode.Additive);
    }

    public void UnloadBattleScene()
    {
        CameraIHateU?.Invoke();
        EnemiesPrefab.Clear();
        New_UI_Manager._UI_MANAGER.fadePanel.BeginFadeIn();
        New_UI_Manager._UI_MANAGER.fadePanel.OnFadeInComplete.AddListener(_SCENE_MANAGER.UnloadBattleScene);
    }

    private void OnBattleSceneFinishedUnloading()
    {
        ReturnToGame();
        

    }
    private void ReturnToGame()
    {
        
        bool finishedSearching = false;
        foreach (GameObject item in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (!finishedSearching)
            {
                if (item.TryGetComponent<Canvas>(out Canvas aa))
                {
                    UI_FadePanel newPanel = aa.gameObject.GetComponentInChildren<UI_FadePanel>();
                    UI_DialogueBoxHandler newDial = aa.GetComponentInChildren<UI_DialogueBoxHandler>();
                    UI_GameMenuParentHandler newGamePar = aa.GetComponentInChildren<UI_GameMenuParentHandler>();

                    New_UI_Manager._UI_MANAGER.fadePanel = newPanel;
                    New_UI_Manager._UI_MANAGER.UI_DialogueHandler = newDial;
                    New_UI_Manager._UI_MANAGER.UI_MainMenuParentHandler = newGamePar;
                    finishedSearching = true;
                }
            }
        }
        CameraObj = PlayerObj;
        player.ReturnToWorld();
       
        New_UI_Manager._UI_MANAGER.InitWorldUI();
        if (player.gameObject.activeSelf)
        {
            player.gameObject.SetActive(false) ;
        }

        PutPlayerInPosition();
        New_UI_Manager._UI_MANAGER.fadePanel.BeginFadeOut();
        InputManager._INPUT_MANAGER.SetInputToWorld();
        //StartCoroutine(WaitAndPutPlayerInPlaceCuzGodKnowsWTFisGoingOn());

        LevelLoaded?.Invoke();
        ReturningToWorld?.Invoke();
    }

    private void PutPlayerInPosition()
    {
            player.gameObject.transform.position = lastPlayerPosition;
            player.gameObject.transform.rotation = lastPlayerRotation;
            player.gameObject.SetActive(true);

    }

    // Dejo esto aqui por que me hizo gracia
    IEnumerator WaitAndPutPlayerInPlaceCuzGodKnowsWTFisGoingOn()
    {
        float timer = 0f;
        float timeToDo = 0.2f;

        while(timer< timeToDo)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        PutPlayerInPosition();

        yield return new WaitForFixedUpdate();
    }

    private void OnLoadFinishGameLoad()
    {
        bool finishedSearching = false;
        foreach (GameObject item in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (!finishedSearching)
            {
                if (item.TryGetComponent<Canvas>(out Canvas aa))
                {
                    UI_FadePanel newPanel = aa.gameObject.GetComponentInChildren<UI_FadePanel>();
                    UI_DialogueBoxHandler newDial = aa.GetComponentInChildren<UI_DialogueBoxHandler>();
                    UI_GameMenuParentHandler newGamePar = aa.GetComponentInChildren<UI_GameMenuParentHandler>();

                    New_UI_Manager._UI_MANAGER.fadePanel = newPanel;
                    New_UI_Manager._UI_MANAGER.UI_DialogueHandler = newDial;
                    New_UI_Manager._UI_MANAGER.UI_MainMenuParentHandler = newGamePar;
                    
                }
                
            }
            if (item.name == "PlayerInitPosition") { PlayerObj = Instantiate(playerPrefab, item.transform.position, Quaternion.identity); }

        }
        

        CameraObj = PlayerObj;
        player = PlayerObj.GetComponent<New_Entity_Script>();
        New_UI_Manager._UI_MANAGER.fadePanel.BeginFadeOut();
        New_UI_Manager._UI_MANAGER.InitWorldUI();

        InputManager._INPUT_MANAGER.SetInputToWorld();
    }

    private void OnLoadFinishBattleLoad()
    {
        bool finishedSearching = false;
        foreach (GameObject item in _SCENE_MANAGER.currentScene.GetRootGameObjects())
        {
            if (!finishedSearching)
            {
                if (item.TryGetComponent<Canvas>(out Canvas aa) && item.activeSelf)
                {
                    UI_FadePanel newPanel = aa.gameObject.GetComponentInChildren<UI_FadePanel>();
                    New_UI_Manager._UI_MANAGER.fadePanel = newPanel;
                    
                }
                if(item.TryGetComponent<Levelnfo>(out Levelnfo a))
                {
                    currentLevelInfo = a;
                    CameraObj= a.CameraObject;
                }
                

            }

        }
        currentLevelInfo.LastPlayerPosition = player.transform.position;
        lastPlayerPosition = player.transform.position;
        lastPlayerRotation = player.transform.rotation;


        New_UI_Manager._UI_MANAGER.fadePanel.BeginFadeOut();
        New_BattleManager._BATTLE_MANAGER.StartCombat(player, EnemiesPrefab);
    }

    private void OnSceneFinishLoaded(Scenes currentSceneLoaded)
    {
        switch (currentSceneLoaded)
        {
            case Scenes.MAINMENU:
                break;
            case Scenes.WORLD:
                OnLoadFinishGameLoad();
                break;
            case Scenes.BATTLE:
                OnLoadFinishBattleLoad();
                break;
        }
       
        LevelLoaded?.Invoke();
       
    }



}
