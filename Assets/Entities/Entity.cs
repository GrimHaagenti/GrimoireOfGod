using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityStat stats;

    protected List<ElementalBlock> ElementInventory;
    [SerializeField] protected List<Rune> RelicInventory;

    
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
    public List<ElementalBlock> EntityElements { get { return ElementInventory; } }
    public List<Rune> EntityRelics { get { return RelicInventory; } }
}
