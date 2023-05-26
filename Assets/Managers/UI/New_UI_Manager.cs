using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class UnityEvent_IntPar: UnityEvent<int>
{

}

public class New_UI_Manager : MonoBehaviour
{
    public static New_UI_Manager _UI_MANAGER = null;

    [SerializeField] public UI_FadePanel fadePanel = null;
    bool receiveInput = true;

    //HANDLERS//
    [SerializeField] public UI_DialogueBoxHandler UI_DialogueHandler;
    [SerializeField] public UI_GameMenuParentHandler UI_MainMenuParentHandler;
    [SerializeField] UI_MainMenu UI_mainMenu;

    //EVENTS
    public UnityEvent OnActionButtonPressed;
    public UnityEvent OnMenuButtonPressed;
    public UnityEvent_IntPar OnHorizontalAxis;
    public UnityEvent_IntPar OnVerticalAxis;

    
    public void PleaseDontCallMenu()
    {
        UI_MainMenuParentHandler.DONTCALLENTERNOW();
        receiveInput = false;
    }
    public void CanCallMenuNow()
    {
        receiveInput = true;
        UI_MainMenuParentHandler.CanCallNow();
    }

    private void Awake()
    {
        if (_UI_MANAGER != null && _UI_MANAGER != this)
        {
            Destroy(this);
        }
        else
        {
            _UI_MANAGER = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
       InitMainMenuUI();
    }
    
    
    private void InitMainMenuUI()
    {
        InputManager._INPUT_MANAGER.SetInputToMenu();
        UI_mainMenu.Init();
        

    }

    public void InitWorldUI()
    {
        UI_DialogueHandler.Init();
        UI_MainMenuParentHandler.Init();
    }

    private void Update()
    {
        if (receiveInput)
        {

            if (InputManager._INPUT_MANAGER.Menu_GetAcceptButtonPressed())
            {
                OnActionButtonPressed?.Invoke();

            }
            if (InputManager._INPUT_MANAGER.Exploration_GetPauseButtonPressed() || InputManager._INPUT_MANAGER.Menu_GetPauseButtonPressed())
            {
                OnMenuButtonPressed?.Invoke();
            }
            Vector2 NavAxis = InputManager._INPUT_MANAGER.Menu_GetNavigatePressed();

            if (NavAxis.x != 0)
            {
                OnHorizontalAxis?.Invoke(Mathf.CeilToInt(NavAxis.x));
            }
            if (NavAxis.y != 0)
            {
                OnVerticalAxis?.Invoke(Mathf.CeilToInt(NavAxis.y));
            }

        }
    }

    public void GoToEquipmentScreen()
    {
        UI_MainMenuParentHandler.ShowEquipmentScreen();
    }

    public void GoToMainScreen()
    {
        UI_MainMenuParentHandler.ShowMainMenuScreen();
    }
    public void ShowDialog(List<Dialogue_Scr> newConversation)
    {
        InputManager._INPUT_MANAGER.SetInputToMenu();
        UI_DialogueHandler.ShowPanel(true);
        UI_DialogueHandler.BeginConversation(newConversation);
    }

}
