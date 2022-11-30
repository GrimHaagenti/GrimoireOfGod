using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public ElementFactory _ELEMENT_FACTORY = null;
    public RelicManager _RELIC_MANAGER;
    public static GameManager _GAME_MANAGER = null;
    [SerializeField] public GameSceneManager _SCENE_MANAGER;
    [SerializeField] public InputManager _INPUT_MANAGER;
    [SerializeField] public BattleManager _BATTLE_MANAGER;
    [SerializeField] public Camera Camera;

    [SerializeField] public GameObject Player;
    [HideInInspector] public PlayerScript playerScript;

    public Levelnfo currentLevelInfo;
    public GameObject PlayerPrefab { get; private set; }

    [SerializeField] List<Sprite> ElementsSprites;

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
            _ELEMENT_FACTORY.sprites = ElementsSprites;
        }

        if (_SCENE_MANAGER == null)
        {
            _SCENE_MANAGER = new GameSceneManager();
        } 

        if (_INPUT_MANAGER == null)
        {
            _INPUT_MANAGER = new InputManager();
        } 
        
        if (_BATTLE_MANAGER == null)
        {
            _BATTLE_MANAGER = new BattleManager();
        }


        InitializeManagers();

        _SCENE_MANAGER.OnSceneLoaded += OnSceneFinishLoaded;
        PlayerPrefab =  GameObject.Instantiate(Player);
        playerScript = PlayerPrefab.GetComponent<PlayerScript>();
        DontDestroyOnLoad(this);
    }
        

    void InitializeManagers()
    {
        _SCENE_MANAGER.Init();
        _INPUT_MANAGER.Init();
    }

    private void Update()
    {
        _SCENE_MANAGER.Update();
        _INPUT_MANAGER.Update();
        _BATTLE_MANAGER.Update();
    }

    public void LoadScene(Scenes scn, LoadSceneMode mode)
    {
        _SCENE_MANAGER.LoadScene(scn, mode);
    }
    public void LoadBattleScene()
    {
        LoadScene(Scenes.BATTLE, LoadSceneMode.Additive);
    }

    private void OnSceneFinishLoaded()
    {
        foreach (GameObject obj in _SCENE_MANAGER.currentScene.GetRootGameObjects())
        {
            if (obj.TryGetComponent<Levelnfo>(out Levelnfo info ))
            {
                currentLevelInfo = info;
            }
        }

        if (currentLevelInfo != null) { Vector3 pos = currentLevelInfo.startPosition.transform.position;
            playerScript.InitializePosition(pos);
            if (!PlayerPrefab.activeSelf) { PlayerPrefab.SetActive(true); }
        }
        if (_SCENE_MANAGER.currentScene == SceneManager.GetSceneByBuildIndex((int)Scenes.BATTLE))
        {
            _BATTLE_MANAGER.PrepareBattle();
        }
        
    }
}
