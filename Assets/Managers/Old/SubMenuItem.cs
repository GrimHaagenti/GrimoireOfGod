using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubMenuItem : MonoBehaviour
{
    [SerializeField] List<GameObject> SubMenuPanels;

    Stack<GameObject> panelStack;
    GameObject currentPanel;

    int panelIndex = 0;

    public delegate void OnPanelChange(SubMenuItem PanelToGo);
    public static OnPanelChange PanelChangeEvent;

    
    private void Awake()
    {
        panelStack = new Stack<GameObject>();
        currentPanel = SubMenuPanels[panelIndex];
        if (!currentPanel.activeSelf) { currentPanel.SetActive(true); }
    }
    public void OnActivation()
    {
        SubMenuPanels.ForEach((it) => { it.SetActive(false); });
        SubMenuPanels[panelIndex].SetActive(true);
    }

    public void ShowPanel(GameObject Index)
    {
        SubMenuPanels.ForEach((it) =>
        {
            if (it == Index) { it.SetActive(true); }
            else { it.SetActive(false); }
        }
        );
    }
    public void GoToNextSubMenu()
    {
        currentPanel.SetActive(false);
        panelStack.Push(currentPanel);
        panelIndex = Mathf.Min(panelIndex+1, SubMenuPanels.Count - 1);
        currentPanel = SubMenuPanels[panelIndex];
        currentPanel.SetActive(true);
    }
    
    public void GoToPreviousSubMenu()
    {
        currentPanel.SetActive(false);

        currentPanel = panelStack.Pop();
        panelIndex = Mathf.Max(panelIndex--, 0) ;
        currentPanel.SetActive(true);
    }


    public void ChangeSubItem(SubMenuItem PanelToGo)
    {
        PanelChangeEvent(PanelToGo);
    }
}
