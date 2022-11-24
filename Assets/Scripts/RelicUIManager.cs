using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicUIManager : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;

    [SerializeField] protected GameObject NextPanel;
    [SerializeField] protected List<Button> optionButtons;

    [SerializeField] protected Button AdvanceMenuButton;


    [SerializeField] GameObject RelicBlockUI;
    [SerializeField] GameObject listParent;


    List<GameObject> RelicBlockUIList = new List<GameObject>();
    List<Rune> playerRunes;
    Rune activeRune;


    public void SetSubmenu(List<Rune> runes)
    {
        playerRunes = runes;
        //CHANGE TO MAX RELIC VARIABLE
        if(playerRunes.Count> 3)
        {
            Debug.Log("Tooo many Relics");
            return;
        }

        playerRunes?.ForEach(
            (it) =>
            {

                GameObject button = Instantiate(RelicBlockUI, listParent.transform);
                button.GetComponentInChildren<Image>().sprite = it.RuneIcon;
                button.GetComponentInChildren<Button>().onClick.AddListener(() => SetActiveRune(it));

                RelicBlockUIList.Add(button);
            }
            );
    }

    public void SetActiveRune(Rune rune)
    {
        AdvanceMenuButton.GetComponentInChildren<Image>().sprite = rune.RuneIcon;
        activeRune = rune;
    }

    void Start()
    {
       
        AdvanceMenuButton.onClick.AddListener(() => {
            battleManager.SetRune(activeRune); NextPanel.SetActive(true);
            gameObject.SetActive(false);
        }) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
