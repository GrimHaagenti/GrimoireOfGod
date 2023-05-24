using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager _ITEM_MANAGER = null;

    private List<Weapon_Scr> weaponDB;

    private void Awake()
    {
        if (_ITEM_MANAGER != null && _ITEM_MANAGER != this)
        {
            Destroy(this);
        }
        else
        {
            _ITEM_MANAGER = this;
        }
        DontDestroyOnLoad(this);
    }

    public void Init(ItemDB items)
    {
       weaponDB= new List<Weapon_Scr>();
        foreach (Weapon_Scr item in items.Weapons)
        {
            weaponDB.Add(item);
        }
    }

    public Weapon_Scr GetWeaponByIndex(int index)
    {
        return weaponDB[index];
    } 

    public int GetWeaponIndex(Weapon_Scr weapon)
    {
        return weaponDB.IndexOf(weapon);
    }

}
