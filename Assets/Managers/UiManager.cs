using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHP;
    [SerializeField] TextMeshProUGUI enemyHP;

    [SerializeField] Button AtkButton;

    static public UiManager _UI_MANAGER;

    //Events
    //public delegate void ClickAction();
    public UnityEvent OnAtkButtonPressed;

    private PlayerScript player;
    private Entity enemy;

    private void Awake()
    {
       if (_UI_MANAGER != null && _UI_MANAGER != this)
        {
            Destroy(_UI_MANAGER);
        }
        else
        {
            _UI_MANAGER = this;
        }
        OnAtkButtonPressed = new UnityEvent();
    }

    private void Update()
    {
        playerHP.text = player.GetEntityStats.currentHP + "/" + player.GetEntityStats.MaxHP;
        enemyHP.text = enemy.GetEntityStats.currentHP + "/" + enemy.GetEntityStats.MaxHP;



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
