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
    List<Relic> playerRunes;
    Relic activeRune;


    public void SetSubmenu(List<Relic> runes)
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

        AdvanceMenuButton.onClick.AddListener(() => {
            battleManager.SetRune(activeRune); NextPanel.SetActive(true);
            gameObject.SetActive(false);
        });

    }

    public void SetActiveRune(Relic rune)
    {
        AdvanceMenuButton.GetComponentInChildren<Image>().sprite = rune.RuneIcon;
        activeRune = rune;
    }

    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
