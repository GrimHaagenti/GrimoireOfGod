using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicUIManager : Panel
{
    BattleManager battleManager;

    [SerializeField] protected GameObject NextPanel;
    [SerializeField] protected List<Button> optionButtons;

    [SerializeField] protected Button AdvanceMenuButton;


    [SerializeField] GameObject RelicBlockUI;
    [SerializeField] GameObject listParent;


    List<GameObject> RelicBlockUIList = new List<GameObject>();
    List<Relic> playerRunes;
    Relic activeRune;


    public void SetSubmenu(List<Relic> runes, BattleManager bM)
    {
        playerRunes = runes;
        //CHANGE TO MAX RELIC VARIABLE

        battleManager = bM;
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

    public override void GoForward()
    {
        throw new System.NotImplementedException();
    }

    public override void GoBackwards()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitPanel()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnterPanel()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAcceptButton()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHoldElementButton()
    {
        throw new System.NotImplementedException();
    }

    public override void OnNavigationVertical(int dir)
    {
        throw new System.NotImplementedException();
    }

    public override void OnNavigationHorizontal(int dir)
    {
        throw new System.NotImplementedException();
    }
}
