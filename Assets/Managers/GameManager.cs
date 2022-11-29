using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameSceneManager))]
[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour
{
    public ElementFactory _ELEMENT_FACTORY = null;
    public RelicManager _RELIC_MANAGER;
    public static GameManager _GAME_MANAGER = null;
    
    [SerializeField] List<Sprite> ElementsSprites;

    [SerializeField] public PlayerScript player;

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


        DontDestroyOnLoad(this);
    }
    
    private void Start()
    {
        FindPlayerInScene();
    }
    public void FindPlayerInScene()
    {
        player = GameObject.Find("Player")?.GetComponent<PlayerScript>();

    }

}
