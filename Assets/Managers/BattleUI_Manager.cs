using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class BattleUI_Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHP;
    [SerializeField] TextMeshProUGUI enemyHP;

    [SerializeField] Button AtkButton;
    [SerializeField] List<SubMenuItem> MainCombatPanels;

    SubMenuItem currentSubMenu;

    private Stack<SubMenuItem> subMenuStack;
    static public BattleUI_Manager _UI_MANAGER;




    //Events
    //public delegate void ClickAction();
    public UnityEvent OnAtkButtonPressed;

    private PlayerScript player;
    private Entity enemy;

    private void Awake()
    {
        subMenuStack = new Stack<SubMenuItem>();
       if (_UI_MANAGER != null && _UI_MANAGER != this)
        {
            Destroy(_UI_MANAGER);
        }
        else
        {
            _UI_MANAGER = this;
        }
        OnAtkButtonPressed = new UnityEvent();

        SubMenuItem.PanelChangeEvent += ChangeSubItemPanels;
        }
    private void Start()
    {
        MainCombatPanels.ForEach((it) => { it.gameObject.SetActive(false); });
           currentSubMenu = MainCombatPanels[0];
        currentSubMenu.gameObject.SetActive(true);
        subMenuStack.Push(currentSubMenu);

    }

    void ChangeSubItemPanels(SubMenuItem PanelToGo)
    {


        MainCombatPanels.ForEach((it) => {
            if (it == PanelToGo)
            {
                it.gameObject.SetActive(true);
                it.OnActivation();
            }
            else
            {
                it.gameObject.SetActive(false);
            }
        });

        subMenuStack.Push(PanelToGo);

    }


    private void Update()
    {
        playerHP.text = player.GetEntityStats.MaxHP + "/" + player.GetEntityStats.MaxHP;
        enemyHP.text = enemy.GetEntityStats.MaxHP + "/" + enemy.GetEntityStats.MaxHP;



    }
    public void OnButtonPress()
    {
        
        OnAtkButtonPressed.Invoke();
        

    }
    private void OnGUI()
    {

    }
    
    public void SetBattle(PlayerScript player, Entity enemy)
    {
        this.player = player;
        this.enemy = enemy;

    }
    


}
