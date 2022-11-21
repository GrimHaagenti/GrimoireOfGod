using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubMenuItem : MonoBehaviour
{
    [SerializeField] List<GameObject> SubMenuPanels;

    Stack<GameObject> panelStack;
    GameObject currentPanel;

    public delegate void OnPanelChange(SubMenuItem PanelToGo);
    public static OnPanelChange PanelChangeEvent;

    
    private void Awake()
    {
        panelStack = new Stack<GameObject>();
        currentPanel = SubMenuPanels[0];
        currentPanel.SetActive(true);
        panelStack.Push(currentPanel);
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

    public void ChangeSubItem(SubMenuItem PanelToGo)
    {
        PanelChangeEvent(PanelToGo);
    }
}
