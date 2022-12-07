using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier 
{
    public int baseBarrierHP { get; private set; }
    public int elementalBarrierHP { get; private set; }
    public Elements barrierElement { get; private set; }

    public void CastBarrier(int baseHp, int elementalHP, Elements element)
    {
        baseBarrierHP = baseHp;
        elementalBarrierHP = elementalHP;
        barrierElement = element;
    }    


    public int HitBarrier(int damage, Elements element)
    {
        int surplus = 0;

        if (element == barrierElement && elementalBarrierHP > 0)
        {
            surplus = elementalBarrierHP - damage;
            if (surplus >= 0)
            {
                elementalBarrierHP = surplus;
                return 0;
            }
            else
            {
                int excedent = baseBarrierHP += surplus;
                return -Mathf.Min(excedent, 0);
            }
        }
        else
        {
            surplus = baseBarrierHP - damage;

            if (surplus >= 0)
            {
                baseBarrierHP = surplus;
                return 0;
            }
            else
            {
                return -surplus;
            }


        }
    }


}
