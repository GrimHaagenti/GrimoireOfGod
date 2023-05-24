using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_ListParent : MonoBehaviour
{

    protected int currentButtonIndex = 0;
    [SerializeField] protected int maxButtonNum = 3;

    

    virtual public void HandleVerticalArrowMovement(int axis)
    {
        currentButtonIndex -= axis;
        currentButtonIndex = Mathf.Clamp(currentButtonIndex, 0, maxButtonNum-1);
        Debug.Log(currentButtonIndex);
    }

    abstract protected void ReselectButton();
    abstract public void OnActionButtonPress();
    abstract public void OnEnter();
    abstract public void OnExit();

}
