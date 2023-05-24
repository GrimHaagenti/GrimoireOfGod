using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleUIManager : MonoBehaviour
{

    public static BattleUIManager _BATTLE_UI_MANAGER = null;

    [SerializeField] public UI_PlayerActionButtonHandler UI_playerActionsHandler;
    [SerializeField] public UI_ReticleHandler UI_reticleHandler;
    [SerializeField] public UI_HealthHandler UI_healthHandler;
    [SerializeField] public UI_RequirementPanelHandler UI_requirementPanelHandler;
    [SerializeField] public UI_ElementalContainersHandler UI_elementalContainerHandler;
    [SerializeField] public UI_Combat_WinLosePanel UI_winPanel;
    [SerializeField] public UI_Combat_WinLosePanel UI_losePanel;


    private UI_BattleState_Enum currentUI_BattleState = UI_BattleState_Enum.PLAYER_ACTION;

    [Space]
    
    [SerializeField] public Sprite[] ElementIcons;

    public UnityEvent Camera2ChooseEnemy;
    public UnityEvent Camera2Standard;


    public bool receiveInput = true;

    private void Awake()
    {
        if (_BATTLE_UI_MANAGER != null && _BATTLE_UI_MANAGER != this)
        {
            Destroy(this);
        }
        else
        {
            _BATTLE_UI_MANAGER = this;
        }
        
    }

    public void InitializeCombatUI(New_Entity_Script player, List<New_Entity_Script> enemies)
    {
        UI_playerActionsHandler.InitButtons(player);
        UI_reticleHandler.InitReticles(enemies);
        UI_healthHandler.InitHP_UI(player, enemies);
        UI_elementalContainerHandler.UpdateChargeText(player);
        InputManager._INPUT_MANAGER.SetInputToMenu();   
    }

    private Directions_Enum VectorToDirEnum(Vector2 dir)
    {
        Directions_Enum dir_enum = Directions_Enum.NO_DIR;

        if (dir.x < 0)
        {
            dir_enum = Directions_Enum.LEFT;
        }
        if (dir.x > 0)
        {
            dir_enum = Directions_Enum.RIGHT;
        }
        if (dir.y < 0)
        {
            dir_enum = Directions_Enum.DOWN;
        }
        if (dir.y > 0)
        {
            dir_enum = Directions_Enum.UP;
        }


        return dir_enum;
    }


    public void ResetTurnUI()
    {
        Camera2Standard?.Invoke();

        currentUI_BattleState = UI_BattleState_Enum.PLAYER_ACTION;
        UI_playerActionsHandler.ShowButtons(true);
        UI_reticleHandler.ShowReticles(false);
        receiveInput = true;
        
    }
    private void ToChooseEnemyUI()
    {
        Camera2ChooseEnemy?.Invoke();
        currentUI_BattleState = UI_BattleState_Enum.CHOOSING_TARGET;
        UI_playerActionsHandler.ShowButtons(false);
        UI_reticleHandler.SelectEnemyUI(New_BattleManager._BATTLE_MANAGER.selectedWeapon.Reach);

    }

    private void HideUI()
    {
        Camera2Standard?.Invoke();
        UI_reticleHandler.ShowReticles(false);
        receiveInput = false;
    }
    public void UpdateChargeText(New_Entity_Script player)
    {
        UI_elementalContainerHandler.UpdateChargeText(player);
    }

    void Update()
    {

        if (receiveInput)
        {
            Directions_Enum dir_e = Directions_Enum.NO_DIR;
            switch (currentUI_BattleState)
            {
                case UI_BattleState_Enum.PLAYER_ACTION:

                    dir_e = VectorToDirEnum(InputManager._INPUT_MANAGER.Menu_GetNavigateInput());

                    UI_playerActionsHandler.HighlightButton(dir_e);

                    if (dir_e != Directions_Enum.NO_DIR)
                    {
                        Weapon_Scr s_weapon = New_BattleManager._BATTLE_MANAGER.GetATK(dir_e);
                        if (s_weapon != null)
                        {
                            UI_elementalContainerHandler.ShowPanel(false);
                            UI_requirementPanelHandler.ShowPanel(true);
                            UI_requirementPanelHandler.SetRequirementIcons(s_weapon.Requirement);

                            if (InputManager._INPUT_MANAGER.Menu_GetAcceptButtonPressed())
                            {
                                New_BattleManager._BATTLE_MANAGER.SetSelectedWeapon(s_weapon);
                                if (s_weapon.Type == ItemType_Enum.SUPPORT)
                                {
                                    Weapon_Support sup = (Weapon_Support)s_weapon;

                                    if (sup.supportWeaponType == SupportWeaponType_Enum.SHIELD)
                                    {
                                        //Shield
                                        New_BattleManager._BATTLE_MANAGER.SetupShield();
                                        UI_requirementPanelHandler.ShowPanel(false);
                                        receiveInput = false;
                                        UI_playerActionsHandler.ShowButtons(false);
                                    }
                                    else
                                    {
                                        ToChooseEnemyUI();
                                    }
                                }
                                else
                                {
                                    ToChooseEnemyUI();
                                }
                            }
                        }
                    }
                    else
                    {
                        UI_requirementPanelHandler.ShowPanel(false);
                        UI_elementalContainerHandler.ShowPanel(true);
                    }



                    break;
                case UI_BattleState_Enum.CHOOSING_TARGET:
                    dir_e = VectorToDirEnum(InputManager._INPUT_MANAGER.Menu_GetNavigatePressed());

                    UI_reticleHandler.HighlightReticle(dir_e);

                    if (InputManager._INPUT_MANAGER.Menu_GetAcceptButtonPressed())
                    {
                        New_BattleManager._BATTLE_MANAGER.playerUseWeapon();
                        HideUI();
                    }

                    break;
            }

        }
        
    }

}
