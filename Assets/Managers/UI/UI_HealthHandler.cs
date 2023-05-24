using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_HealthHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerName;
    [SerializeField] private TextMeshProUGUI PlayerHealth;
    
    [SerializeField] private List<TextMeshProUGUI> enemiesNameList;
    [SerializeField] private List<TextMeshProUGUI> enemiesHealthList;


    public void InitHP_UI(New_Entity_Script player, List<New_Entity_Script> enemies)
    {
        PlayerName.text = player.Name;
        PlayerHealth.text = player.CurrentHP + "/" + player.MaxHP;

        int hideIndex = -1;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemiesNameList[i].text = enemies[i].Name;
            enemiesHealthList[i].text = enemies[i].CurrentHP + "/" + enemies[i].MaxHP;
            hideIndex = i;
        }
        if (hideIndex < enemiesNameList.Count-1)
        {
            for (int i = hideIndex+1; i < enemiesNameList.Count; i++)
            {
                enemiesNameList[i].gameObject.SetActive(false);
                enemiesHealthList[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdatePlayerHealth(New_Entity_Script player)
    {
        PlayerHealth.text = player.CurrentHP + "/" + player.MaxHP;
    }

    public void UpdateEnemiesHealth(List<New_Entity_Script> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemiesHealthList[i].text = enemies[i].CurrentHP + "/" + enemies[i].MaxHP;
        }
    }
}
