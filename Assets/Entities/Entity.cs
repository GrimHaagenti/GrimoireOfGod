using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityStat stats;

    public int CurrentHP { get; private set; }
    public int CurrentATK { get; private set; }
    public int CurrentDEF { get; private set; }
    public int CurrentSPD { get; private set; }
    
    public void InitEntity()
    {
        CurrentHP = stats.MaxHP;
        CurrentATK = stats.Atk;
        CurrentDEF = stats.Def;
        CurrentSPD = stats.Spd;
    }

    public int Heal(int healAmount)
    {
        CurrentHP += healAmount;
        CurrentHP = Mathf.Min(CurrentHP, stats.MaxHP);
        return CurrentHP;
    }
    public int GetHit(int damage)
    {
        CurrentHP -= damage;

        return CurrentHP;
    }

    public EntityStat GetEntityStats { get { return stats; } }
    public List<ElementalBlock> EntityElements { get { return stats.ElementInventory; } }
    public List<Relic> EntityRelics { get { return stats.RelicInventory; } }
}
