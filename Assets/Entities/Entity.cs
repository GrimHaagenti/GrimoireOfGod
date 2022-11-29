using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityStat stats;

    protected List<ElementalBlock> ElementInventory;
    [SerializeField] protected List<Relic> RelicInventory;
    [SerializeField] public Attributes[] Weaknesses;

    
    // Start is called before the first frame update
        private void Awake()
        {
        ElementInventory = new List<ElementalBlock>();
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetHit(int damage)
    {
        stats.MaxHP -= damage;

        return stats.MaxHP;
    }

    public EntityStat GetEntityStats { get { return stats; } }
    public List<ElementalBlock> EntityElements { get { return ElementInventory; } }
    public List<Relic> EntityRelics { get { return RelicInventory; } }
}
