using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_EquipmentItemsListHandler : UI_ListParent
{
    [SerializeField] GameObject ListItemPrefab;
    [SerializeField] GameObject ListParent;
    List<UI_WeaponListItem> WeaponList;

    ItemType_Enum listWeaponType = ItemType_Enum.NONE_ITEMTYPE;

    public UnityEvent ReturnToEquipment;

    int weaponSlotIndex = -1;

    public void SetTypeAndEnter(int slot)
    {
        ItemType_Enum listType = ItemType_Enum.NONE_ITEMTYPE;
        weaponSlotIndex = slot;
        switch (slot)
        {
            case 0:
                listType = ItemType_Enum.MELEE;
                break;
            case 1:
            case 2:
                listType = ItemType_Enum.SKILL;
                break;
            case 3:
                listType = ItemType_Enum.SUPPORT;
                break;
        }
        listWeaponType = listType;
        OnEnter();
    }

    public override void OnActionButtonPress()
    {
        //Change Weapon
        GameManager._GAME_MANAGER.player.SetWeaponInSlot(WeaponList[currentButtonIndex].GetWeaponInventoryIndex(),weaponSlotIndex);
        //InvokOnWeaponSelected
        ReturnToEquipment?.Invoke();
        OnExit();
    }

    public override void HandleVerticalArrowMovement(int axis)
    {
        base.HandleVerticalArrowMovement(axis);
        ReselectButton();
    }

    public override void OnEnter()
    {
        
        WeaponList = new List<UI_WeaponListItem>();
        List<int> playerWeapons = GameManager._GAME_MANAGER.player.Inventory;
        currentButtonIndex = 0;
        if (playerWeapons.Count>0 )
        {
            for (int i = 0; i < playerWeapons.Count; i++)
            {
                Weapon_Scr wpn = ItemManager._ITEM_MANAGER.GetWeaponByIndex(playerWeapons[i]);
                if (wpn.Type == listWeaponType)
                {
                    UI_WeaponListItem WeaponItem = Instantiate(ListItemPrefab, ListParent.transform).GetComponent<UI_WeaponListItem>();
                    WeaponList.Add(WeaponItem);
                    WeaponItem.SetWeaponItemName(wpn, i);

                }
            }
           
            if(WeaponList.Count > 0)
            {
                New_UI_Manager._UI_MANAGER.OnVerticalAxis?.AddListener(HandleVerticalArrowMovement);
                New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.AddListener(OnActionButtonPress);
                maxButtonNum = WeaponList.Count;

                WeaponList[currentButtonIndex].UI_ItemButton.Select();
            }
            else
            {
                ReturnToEquipment?.Invoke();
                
            }
            
        }
    }
    
    public override void OnExit()
    {
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.RemoveListener(HandleVerticalArrowMovement);
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.RemoveListener(OnActionButtonPress);
        foreach (UI_WeaponListItem item in WeaponList)
        {
            Destroy(item.gameObject);
        }
        WeaponList.Clear();
        weaponSlotIndex = -1;
    }

    protected override void ReselectButton()
    {
        WeaponList[currentButtonIndex].UI_ItemButton.Select();
    }

    
}
