using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class New_BattleManager : MonoBehaviour
{
    
    const int MaxEnemyInEncounter = 3;

    [SerializeField] private Transform playerPosition;
    [SerializeField] private List<Transform> enemiesPosition3F;
    [SerializeField] private List<Transform> enemiesPosition2F;

    [SerializeField] private List<GameObject> enemiesPrefab;

    //Entity Stats
    private New_Entity_Script player;
    public List<New_Entity_Script> enemies { get; private set; }

    public delegate void WeaponEvent(Weapon_Scr weapon);
    public event WeaponEvent UseWeaponEvent;

    public static New_BattleManager _BATTLE_MANAGER = null;


    public Weapon_Scr selectedWeapon = null;

    private int currentEnemyAtkIndex = 0;

    private void Awake()
    {
        if (_BATTLE_MANAGER != null && _BATTLE_MANAGER != this)
        {
            Destroy(this);
        }
        else
        {
            _BATTLE_MANAGER = this;
        }

        

    }
 
    private int DamageCalculation(int Potency)
    {
        return Potency;
    }
    
    public List<int> RequirementToElementList(List<Elements_Enum> requirement)
    {
        List<int> result = new List<int>();

        for (int i = 0; i < (int)Elements_Enum.NEUTRAL_E; i++)
        {
            result.Add(0);
        }
        

        foreach (Elements_Enum element in requirement)
        {
            if (element != Elements_Enum.NEUTRAL_E)
            {
                result[(int)element]++;
            }

        }
        return result;
    }

    public void playerUseWeapon()
    {
        List<int> requirementList = RequirementToElementList(selectedWeapon.Requirement);

        player.DepleteCharge(requirementList);
        BattleUIManager._BATTLE_UI_MANAGER.UI_elementalContainerHandler.UpdateChargeText(player);


        switch (selectedWeapon.Reach)
        {
            case WeaponReach_Enum.ONE:
                UseDamagingWeapon(selectedWeapon, enemies[BattleUIManager._BATTLE_UI_MANAGER.UI_reticleHandler.GetCurrentEnemyIndex], player);
                break;
            case WeaponReach_Enum.ALL:
                foreach (New_Entity_Script enemy in enemies)
                {
                    UseDamagingWeapon(selectedWeapon, enemy, player);
                }
                break;
            case WeaponReach_Enum.NONE_REACH:
                break;
        }
        BattleUIManager._BATTLE_UI_MANAGER.UI_healthHandler.UpdateEnemiesHealth(enemies);

        // GO TO ENEMY TURN
        //EnemyTurn();
    }

    public void UseDamagingWeapon(Weapon_Scr selWeapon, New_Entity_Script atkReceiver, New_Entity_Script attacker)
    {
        attacker.PlayAtkAnimation();

        if (atkReceiver.BarrierSlot != null)
        {

            atkReceiver.BarrierSlot.HitBarrier(selWeapon.Potency, selWeapon.WeaponElement, out int damage, out bool gainOrNot);
            atkReceiver.TakeDamage(damage);

        }
        else
            {
        atkReceiver.TakeDamage(DamageCalculation(selWeapon.Potency));
        }
    }

    public Weapon_Scr? GetATK(Directions_Enum currentDir)
    {
        switch (currentDir)
        {
            case Directions_Enum.UP:
                return player.GetWeaponFromInventoryIndex(player.Equipment.SupportWeapon);
                break;
            case Directions_Enum.DOWN:
                return player.GetWeaponFromInventoryIndex(player.Equipment.MeleeWeapon);
                break;
            case Directions_Enum.LEFT:
                return player.GetWeaponFromInventoryIndex(player.Equipment.Skill1Weapon);
                break;
            case Directions_Enum.RIGHT:
                return player.GetWeaponFromInventoryIndex(player.Equipment.Skill2Weapon);
                break;
            case Directions_Enum.NO_DIR:
                break;
        }
        return null;
    }

    void ShieldDrain()
    {
        if (player.BarrierSlot != null)
        {
            List<int> requirementList = new List<int>();

            for (int i = 0; i < (int)Elements_Enum.NEUTRAL_E; i++)
            {
                requirementList.Add(0);
            }

            requirementList[(int)player.BarrierSlot.barrierElement] += 1;

            if (player.ElementalChargesCurrentCapacity[(int)player.BarrierSlot.barrierElement] <= 0)
            {
                player.TurnOffBarrier();
                player.BarrierPrefab.DeactivateBarrier();
            }
            else
            {

                player.DepleteCharge(requirementList);
            }
        }
    }

    public void SetupShield()
    {

        List<int> requirementList = RequirementToElementList(selectedWeapon.Requirement);

        player.DepleteCharge(requirementList);

        //Let's assume selected weapon is a shield
        if (player.BarrierSlot == null)
        {
            player.SetBarrier( Barrier.CastBarrier(selectedWeapon.WeaponElement));
            player.BarrierPrefab.ActivateBarrier(selectedWeapon.WeaponElement);
        }
        else
        {
            player.TurnOffBarrier();
            player.BarrierPrefab.DeactivateBarrier();
        }
        // GO TO ENEMY TURN
        //EnemyTurn();

    }


    public void SetSelectedWeapon(Weapon_Scr selWeapon)
    {
        selectedWeapon = selWeapon;
    }
    public void UseATK(Directions_Enum currentDir)
    {
        switch (currentDir)
        {
            case Directions_Enum.UP:
                selectedWeapon = player.GetWeaponFromInventoryIndex(player.Equipment.SupportWeapon);
                Debug.Log("Supp");
                break;
            case Directions_Enum.DOWN:
                selectedWeapon = player.GetWeaponFromInventoryIndex(player.Equipment.MeleeWeapon);
                Debug.Log("Melee");
                break;
            case Directions_Enum.LEFT:
                selectedWeapon = player.GetWeaponFromInventoryIndex(player.Equipment.Skill1Weapon);
                Debug.Log("Skill1");
                break;
            case Directions_Enum.RIGHT:
                selectedWeapon = player.GetWeaponFromInventoryIndex(player.Equipment.Skill2Weapon);
                Debug.Log("Skill2");
                break;
            case Directions_Enum.NO_DIR:
                break;
        }
    }


    public void StartCombat(New_Entity_Script player_b, List<GameObject> enemies_b)
    {
        player = player_b;
        player.gameObject.SetActive(true);
        player.InitPlayerBattle();

        player.transform.position = playerPosition.position;

        enemies = new List<New_Entity_Script>();

        switch (enemies_b.Count)
        {
            case 1:
                enemies.Add(Instantiate(enemies_b[0], enemiesPosition3F[1]).GetComponent<New_Entity_Script>());
                break;
            case 2:
                for (int i = 0; i < enemies_b.Count; i++)
                {
                    enemies.Add(Instantiate(enemies_b[i], enemiesPosition2F[i]).GetComponent<New_Entity_Script>());
                }
                break;
            case 3:
                for (int i = 0; i < enemies_b.Count; i++)
                {
                    enemies.Add(Instantiate(enemies_b[i], enemiesPosition3F[i]).GetComponent<New_Entity_Script>());
                }
                break;
        }

        BattleUIManager._BATTLE_UI_MANAGER.InitializeCombatUI(player, enemies);
        CombatStart();
    }
    void CombatStart()
    {
        //Everything That has to happen at the begginning of the combat. Just once per combat

        if (player == null) 
        {
            Debug.LogError("No player");
            return;
        }
        if(enemies.Count < 0) { 
            Debug.LogError("No Enemies, wtf"); 
            return;
        }
        if(enemies.Count> MaxEnemyInEncounter)
        {
            Debug.LogWarning("Too many enemies, recortando...");
            enemies.RemoveRange(MaxEnemyInEncounter, enemies.Count - MaxEnemyInEncounter);
        }

        player.BarrierPrefab.BarrierAnimationFinished.AddListener(EnemyTurn);
        player.AtkAnimationFinished.AddListener(EnemyTurn);
        

        foreach (New_Entity_Script enemy in enemies) 
        {
            enemy.AtkAnimationFinished.AddListener(AdvanceEnemyAttack);
        }

    }

    void TurnStart()
    {
        //Everything That has to happen at the begginning of the turn

        // Use up a charge if barrier activated
        ShieldDrain();

        BattleUIManager._BATTLE_UI_MANAGER.ResetTurnUI();

    }

    void PlayerTurn()
    {

    }

    void EnemyTurn()
    {
        currentEnemyAtkIndex= 0;
        MakeEnemyAttack();
    }

    void AdvanceEnemyAttack()
    {
        currentEnemyAtkIndex++;
        BattleUIManager._BATTLE_UI_MANAGER.UI_healthHandler.UpdatePlayerHealth(player);
        MakeEnemyAttack();

    }

    void MakeEnemyAttack()
    {
        if (currentEnemyAtkIndex >= enemies.Count)
        {
            TurnEnd();
            return;
        }

        Debug.LogWarning("REDO ENEMY BEHAVIOUR");
        if (enemies[currentEnemyAtkIndex].CurrentHP > 0) { UseDamagingWeapon(ItemManager._ITEM_MANAGER.GetWeaponByIndex(1), player, enemies[currentEnemyAtkIndex]); }
        else { AdvanceEnemyAttack(); }
        
    }


    void TurnEnd()
    {
        BattleUIManager._BATTLE_UI_MANAGER.UI_healthHandler.UpdateEnemiesHealth(enemies);
        BattleUIManager._BATTLE_UI_MANAGER.UI_healthHandler.UpdatePlayerHealth(player);
        BattleUIManager._BATTLE_UI_MANAGER.UpdateChargeText(player);

        int enemyDeadCount = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            
            if(enemies[i].CurrentHP <= 0)
            {
                enemyDeadCount++;
            }

        }
        if (enemyDeadCount == enemies.Count)
        {
            CombatEnd(true); return;
        }

        if (player.CurrentHP <= 0)
        {
            CombatEnd(false); return;
        }
        TurnStart();


    }
    void CombatEnd(bool playerWon) 
    {
        player.TurnOffBarrier();
        if (playerWon)
        {
            enemies.ForEach(e => { Destroy(e.gameObject); });
            enemies.Clear();
            BattleUIManager._BATTLE_UI_MANAGER.receiveInput = false;
            BattleUIManager._BATTLE_UI_MANAGER.UI_winPanel.gameObject.SetActive(true);
        }
        else
        {
            
            BattleUIManager._BATTLE_UI_MANAGER.receiveInput = false;
            BattleUIManager._BATTLE_UI_MANAGER.UI_losePanel.gameObject.SetActive(true);
        }

    }
    
}