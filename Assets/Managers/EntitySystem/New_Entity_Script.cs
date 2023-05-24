using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class New_Entity_Script : MonoBehaviour
{

    [SerializeField] private New_EntityStats entityStats;


    private string name = "";

    //STATS
    private int maxHP = 0;
    //tempStats
    private int currentHP = 0;
    
    //Database References
    private List<int> inventory;
    //Equipment reference inventory
    private Entity_Equipment equipment;
    private List<int> m_elementalChargesMaxCapacity;
    private List<int> m_elementalChargesCurrentCapacity;

    private Barrier barrierSlot;

    [SerializeField] private Prefab_BarrierScript barrierScript;

    [Header("Animations")]
    [SerializeField] Animator entityAnimator;
    [SerializeField] AnimationClip hitAnimation;
    [SerializeField] AnimationClip deathAnimation;
    [SerializeField] AnimationClip atkAnimation;

    public UnityEvent AtkAnimationFinished;

    private void Awake()
    {
        InitEntity();
    }

    private void InitEntity() 
    {
        name = entityStats.Name;
        maxHP = entityStats.MaxHP;
        currentHP = entityStats.MaxHP;
        
        equipment = new Entity_Equipment(entityStats.Equipment);

        inventory = entityStats.Inventory;
        m_elementalChargesMaxCapacity = new List<int>();
        m_elementalChargesCurrentCapacity = new List<int>();
        for (int i = 0; i < entityStats.ElementalChargesMaxCapacity.Count; i++)
        {
            m_elementalChargesMaxCapacity.Add(entityStats.ElementalChargesMaxCapacity[i]);
            m_elementalChargesCurrentCapacity.Add(entityStats.ElementalChargesMaxCapacity[i]);
        }
        
    }
    public void ReplenishPlayer()
    {
        Heal(maxHP);
        Recharge(m_elementalChargesMaxCapacity);
    }

    public void Heal(int healAmount)
    {
        currentHP = Mathf.Min(currentHP + healAmount, maxHP);
    }

    public void ReceiveWeapon(Weapon_Scr newWeapon)
    {
        inventory.Add(ItemManager._ITEM_MANAGER.GetWeaponIndex(newWeapon));
    }

    public void InitPlayerBattle()
    {
        transform.localScale = new Vector3(4, 4, 4);
        transform.rotation = Quaternion.Euler(0, 125, 0); 
    }
   public void ReturnToWorld()
    {
        transform.localScale = new Vector3(1,1,1);
    }
    int GetWeaponIndexFromInventoryItem(int InventoryIndex)
    {
       return inventory[InventoryIndex];
    }

    public Weapon_Scr? GetWeaponFromInventoryIndex(int weaponInventoryIndex)
    {
        if (weaponInventoryIndex >= 0)
        {
            return ItemManager._ITEM_MANAGER.GetWeaponByIndex(inventory[weaponInventoryIndex]);
        }
        else { return null; }
    }


    public void SetWeaponInSlot(int WeaponInventoryIndex, int weaponSlot)
    {
        switch (weaponSlot)
        {
            case 0: // MELEE
                equipment.SetWeapon(GetWeaponIndexFromInventoryItem(WeaponInventoryIndex), weaponSlot);
                break;
            case 1: // SKILL 1
                if (equipment.Skill2Weapon == WeaponInventoryIndex) { equipment.ClearSlot(2); }
                equipment.SetWeapon(GetWeaponIndexFromInventoryItem(WeaponInventoryIndex), weaponSlot);
                break;
            case 2: // SKILL 2
                if (equipment.Skill1Weapon == WeaponInventoryIndex) { equipment.ClearSlot(1); }
                equipment.SetWeapon(GetWeaponIndexFromInventoryItem(WeaponInventoryIndex), weaponSlot);
                break;
            case 3: // SUPPORT
                equipment.SetWeapon(GetWeaponIndexFromInventoryItem(WeaponInventoryIndex), weaponSlot);
                break;
        }

    }
    public void SetBarrier(Barrier bar)
    {
        barrierSlot = bar;
    }

    public void TurnOffBarrier()
    {
        barrierSlot = null;
    }
    public void TakeDamage(int damageReceived)
    {
        currentHP -= damageReceived;
        if (currentHP <= 0) 
        {
            PlayDeathAnimation();
            currentHP = 0;
        }
        else
        { PlayHitAnimation(); }
    }
    public void Recharge(List<int> chargeRe)
    {
        m_elementalChargesCurrentCapacity[(int)Elements_Enum.FIRE] = Mathf.Min(m_elementalChargesCurrentCapacity[(int)Elements_Enum.FIRE] + chargeRe[(int)Elements_Enum.FIRE], m_elementalChargesMaxCapacity[(int)Elements_Enum.FIRE]);
        m_elementalChargesCurrentCapacity[(int)Elements_Enum.ICE] = Mathf.Min(m_elementalChargesCurrentCapacity[(int)Elements_Enum.ICE] + chargeRe[(int)Elements_Enum.ICE], m_elementalChargesMaxCapacity[(int)Elements_Enum.ICE]);
        m_elementalChargesCurrentCapacity[(int)Elements_Enum.ELEC] = Mathf.Min(m_elementalChargesCurrentCapacity[(int)Elements_Enum.ELEC] + chargeRe[(int)Elements_Enum.ELEC], m_elementalChargesMaxCapacity[(int)Elements_Enum.ELEC]);
    }

    public void DepleteCharge(List<int> chargeDepleted)
    {
        m_elementalChargesCurrentCapacity[(int)Elements_Enum.FIRE] -= chargeDepleted[(int)Elements_Enum.FIRE];
        m_elementalChargesCurrentCapacity[(int)Elements_Enum.ICE] -= chargeDepleted[(int)Elements_Enum.ICE];
        m_elementalChargesCurrentCapacity[(int)Elements_Enum.ELEC] -= chargeDepleted[(int)Elements_Enum.ELEC];
    }
    
    public void PlayHitAnimation()
    {
        entityAnimator.Play("Hit");
    }

    public void PlayAtkAnimation()
    {
        entityAnimator.Play("Attack");
    }
    public void PlayDeathAnimation()
    {
        entityAnimator.Play("Death");
    }

    //Accesors
    #region
    public string Name { get { return name;} }
    public int MaxHP { get { return maxHP;} }
    public int CurrentHP { get { return currentHP; } }
    public Entity_Equipment Equipment { get { return equipment; } }
    public List<int> Inventory { get { return inventory; } }
    public List<int> ElementalChargesMaxCapacity { get { return m_elementalChargesMaxCapacity; } }
    public List<int> ElementalChargesCurrentCapacity { get { return m_elementalChargesCurrentCapacity; } }
    public Barrier BarrierSlot { get { return barrierSlot; } }
    public Prefab_BarrierScript BarrierPrefab { get { return barrierScript; } }

    #endregion

}
