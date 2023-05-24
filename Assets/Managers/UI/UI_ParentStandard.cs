using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ParentStandard : MonoBehaviour
{
    [SerializeField] protected List<GameObject> hideObjects;

    public virtual void ShowPanel(bool show)
    {
        foreach (GameObject go in hideObjects)
        {
            go.SetActive(show);
        }
    }

}
