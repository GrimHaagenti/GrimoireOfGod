using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager _GAME_MANAGER = null;
    [SerializeField] public GameSceneManager _SCENE_MANAGER;
    [SerializeField] public BattleManager _BATTLE_MANAGER;
    [SerializeField] public DB_Manager _DB_MANAGER;
    [SerializeField] public ElementFactory _ELEMENT_FACTORY;
    [SerializeField] public SpellFactory _SPELL_FACTORY;
    [SerializeField] public UIManager _UI_MANAGER;

    [SerializeField] public CameraScript m_camera;

    [SerializeField] public GameObject Player;
    [HideInInspector] public PlayerScript playerScript;

    private TempPlayerValues tempPlayerValues;

    public Levelnfo currentLevelInfo;
    public GameObject PlayerPrefab { get; private set; }


    //[SerializeField] public PlayerScript player;

    private void Awake()
    {
        if(_GAME_MANAGER !=null && _GAME_MANAGER != this)
        {
            Destroy(_GAME_MANAGER);
        }
        else { _GAME_MANAGER = this; }

        if (_ELEMENT_FACTORY == null)
        {
            _ELEMENT_FACTORY = new ElementFactory();
        }

        if (_SCENE_MANAGER == null)
        {
            _SCENE_MANAGER = new GameSceneManager();
        } 
        
        if (_BATTLE_MANAGER == null)
        {
            _BATTLE_MANAGER = new BattleManager();
        }
        if (_DB_MANAGER == null)
        {
            _DB_MANAGER = new DB_Manager();
        }

        if (_UI_MANAGER == null)
        {
            _UI_MANAGER = new UIManager();
        }

        InitializeManagers();

        _SCENE_MANAGER.OnSceneLoaded += OnSceneFinishLoaded;
        _SCENE_MANAGER.OnBattleUnloaded += OnBattleSceneFinishedUnloading;


        PlayerPrefab =  GameObject.Instantiate(Player);
        PlayerPrefab.SetActive(false);
        playerScript = PlayerPrefab.GetComponent<PlayerScript>();
        playerScript.InitEntity();
        tempPlayerValues = ScriptableObject.CreateInstance<TempPlayerValues>();
        DontDestroyOnLoad(this);
    }
        

    void InitializeManagers()
    {

        _SCENE_MANAGER.Init();
        _DB_MANAGER.Init();
        _UI_MANAGER.Init();
        InputManager._INPUT_MANAGER.Init();

    }

    private void Update()
    {
        _SCENE_MANAGER.Update();
        _BATTLE_MANAGER.Update();
        _UI_MANAGER.Update();


       
    }

    public void LoadScene(Scenes scn, LoadSceneMode mode)
    {
        _SCENE_MANAGER.LoadScene(scn, mode);
        InputManager._INPUT_MANAGER.ChangeInputType(scn);
    }

    public void LoadBattleScene(GameObject enemy)
    {

        enemy.SetActive(false);
         tempPlayerValues.LastPlayerPosition =  playerScript.gameObject.transform.position;
        LoadScene(Scenes.BATTLE, LoadSceneMode.Additive);

    }

    public void ShowMessage(string message, float duration)
    {
        _UI_MANAGER.ShowMessage(message, duration);
    }
    public void UnloadBattleScene()
    {

        _SCENE_MANAGER.UnloadBattleScene();
        InputManager._INPUT_MANAGER.ChangeInputType(Scenes.WORLD);

            
    }

    private void OnBattleSceneFinishedUnloading()
    {
        PanelIndexer ind = null;

        foreach (GameObject obj in _SCENE_MANAGER.currentScene.GetRootGameObjects())
        {
            if (!obj.activeSelf) { continue; }
            if (obj.TryGetComponent<Levelnfo>(out Levelnfo info))
            {
                currentLevelInfo = info;
            }
            if (obj.TryGetComponent<PanelIndexer>(out PanelIndexer inde))
            {
                ind = inde;
            }
        }
        if (ind != null)
        {
            _UI_MANAGER.SetIndexer(ind);
        }
        playerScript.InitializePosition(tempPlayerValues.LastPlayerPosition);
        if (!PlayerPrefab.activeSelf) { PlayerPrefab.SetActive(true); }

    }
    private void OnSceneFinishLoaded()
    {
        //TEMP
        PanelIndexer ind = null;
        //TEMP

        foreach (GameObject obj in _SCENE_MANAGER.currentScene.GetRootGameObjects())
        {
            if (!obj.activeSelf) { continue; }
            if (obj.TryGetComponent<Levelnfo>(out Levelnfo info ))
            {
                currentLevelInfo = info;
            }
            if(obj.TryGetComponent<PanelIndexer>(out PanelIndexer inde))
            {
                ind = inde;
            }
        }

        if (currentLevelInfo != null) { Vector3 pos = currentLevelInfo.startPosition.transform.localPosition;
            playerScript.InitializePosition(pos);
            if (!PlayerPrefab.activeSelf) { PlayerPrefab.SetActive(true); }
        }
        if (ind != null) { _UI_MANAGER.SetIndexer(ind);     }
     

        if (_SCENE_MANAGER.currentScene == SceneManager.GetSceneByBuildIndex((int)Scenes.BATTLE))
        {
            

            _BATTLE_MANAGER.PrepareBattle();
        }

    }

    public int CalculateBattleDamage(int RelicPower, int UserATK, int ElementPotency, int TargetDEF)
    {
        float atkPower = RelicPower * UserATK * (ElementPotency + (ElementPotency * 9));
        float defPower = TargetDEF * (TargetDEF / 3);

        int DamageDone = Mathf.CeilToInt(atkPower / defPower);

        return DamageDone;
    }


}
