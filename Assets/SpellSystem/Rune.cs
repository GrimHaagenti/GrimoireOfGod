using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : ScriptableObject
{
    [SerializeField] protected Sprite runeIcon;



    virtual public void Effect()
    {

    }


    public Sprite RuneIcon { get { return runeIcon; } }
}
