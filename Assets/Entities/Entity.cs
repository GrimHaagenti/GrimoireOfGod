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

    public List<ElementalBlock> _ElementInventory { get; private set; }
    public List<Relic> _RelicInventory { get; private set; }

    //Temporal 
    [SerializeField] protected Animator anim;

    public bool AnimFinished = false;

   
    public void InitEntity()
    {
        CurrentHP = stats.MaxHP;
        CurrentATK = stats.Atk;
        CurrentDEF = stats.Def;
        CurrentSPD = stats.Spd;
        _ElementInventory = new List<ElementalBlock>();
        _RelicInventory = new List<Relic>();
        foreach (ElementalBlock e in stats.ElementInventory)
        {
            _ElementInventory.Add(e);
        }
        foreach (Relic r in stats.RelicInventory)
        {
            _RelicInventory.Add(r);
        }
    }
    public void AddToElementInventory(ElementalBlock elem)
    {
        int count = 0;
        foreach (ElementalBlock e in _ElementInventory)
        {
            if(e.BlockElement == elem.BlockElement){count++;}
        }
        if (count >= stats.ElementInvetoryLimit) { /* Display Message TOO MANY TO HOLD*/}
        else { _ElementInventory.Add(elem); }

    }
    public void PlayAnimation(AnimationClip _anim)
    {
        if (anim?.GetCurrentAnimatorClipInfo(0)[0].clip.name != _anim.name)
        {
            anim.Play(anim.name);
        }
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
    
}
