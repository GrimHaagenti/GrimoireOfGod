using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Effect{PROJECTILE, BLADE, BARRIER, EXPLOSION  }

public enum Attributes {SLASH,  FIRE, WATER, WIND }

public static class RuneEffects
{

    public static void Projectile(List<Entity> targets,List<ElementalBlock>elementalBlocks, Entity user)
    {
        foreach (Entity ent in targets)
        {
            ent.GetEntityStats.currentHP--;
        }

    }
    public static void Slash(List<Entity> targets, List<ElementalBlock> elementalBlocks, Entity user) 
    {
        foreach (Entity ent in targets)
        {
            ent.GetEntityStats.currentHP--;
        }
    }


}



//public static class RuneEffects
//{

//    public static Projectile(List<Entity> targets)
//    {
//        foreach (Entity ent in targets)
//        {
//            ent.GetEntityStats.currentHP--;
//        }

//    }


//}
