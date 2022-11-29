using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityStat stats;

    
    protected void Awake()
    {
    stats.ElementInventory = new List<ElementalBlock>();
    }


    public int GetHit(int damage)
    {
        stats.MaxHP -= damage;

        return stats.MaxHP;
    }

    public EntityStat GetEntityStats { get { return stats; } }
    public List<ElementalBlock> EntityElements { get { return stats.ElementInventory; } }
    public List<Relic> EntityRelics { get { return stats.RelicInventory; } }
}
