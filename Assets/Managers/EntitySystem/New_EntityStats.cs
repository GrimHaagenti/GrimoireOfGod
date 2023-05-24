using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityStats", menuName = "New/Entity/EntityStats")]
public class New_EntityStats : ScriptableObject
{

    [SerializeField] private string playerName = "";

    //STATS
    [SerializeField] private int maxHP = 100;
    //tempStats

    [SerializeField] private Equipment_Scr equipment;

    [SerializeField] private List<Weapon_Scr> inventory;

    [SerializeField] private List<int> elementalChargesMaxCapacity;

    
    List<int> GetInventoryIndexes()
    {
        List<int> indexes = new List<int>();

        foreach (Weapon_Scr wpn in inventory)
        {
            indexes.Add(ItemManager._ITEM_MANAGER.GetWeaponIndex(wpn));
        }
        return indexes;
    }


    #region
    public string Name { get { return playerName; } }
    public int MaxHP { get { return maxHP; } }
    public Equipment_Scr Equipment { get { return equipment; } }
    public List<int> Inventory { get { return GetInventoryIndexes(); } }
    public List<int> ElementalChargesMaxCapacity { get { return elementalChargesMaxCapacity; } }
    #endregion

}
