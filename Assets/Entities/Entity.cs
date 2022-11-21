using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityStat stats;

    protected List<ElementalBlock> ElementInventory;

    
    // Start is called before the first frame update
        private void Awake()
        {
            stats.currentHP = stats.MaxHP;
        ElementInventory = new List<ElementalBlock>();
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetHit(int damage)
    {
        stats.currentHP -= damage;

        return stats.currentHP;
    }

    public EntityStat GetEntityStats { get { return stats; } }
}
