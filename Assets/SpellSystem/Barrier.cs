using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier 
{
    //public int barrierPotency { get; private set; }
    public Elements_Enum barrierElement { get; private set; }

    //public int elementalBarrierHP { get; private set; }

    static public Barrier CastBarrier(Elements_Enum element)
    {
        Barrier bar = new Barrier();
        bar.barrierElement = element;
        
        return bar;

        //elementalBarrierHP = elementalHP;
    }    

    /// <summary>
    /// Barrier Effectiveness agains incoming attack. 
    /// </summary>
    /// <param name="barrier_E"></param>
    /// <param name="incomingAtk_E"></param>
    /// <returns></returns>
    private Effectiveness_Enum GetElementEffectiveness(Elements_Enum barrier_E, Elements_Enum incomingAtk_E)
    {
        Effectiveness_Enum effectiveness = Effectiveness_Enum.NEUTRAL;

        if (incomingAtk_E == Elements_Enum.NEUTRAL_E) { return effectiveness; }

        switch (barrier_E)
        {
            case Elements_Enum.FIRE:
                switch (incomingAtk_E)
                {
                    case Elements_Enum.FIRE:
                        effectiveness = Effectiveness_Enum.SAME;
                        break;
                    case Elements_Enum.ICE:
                        effectiveness = Effectiveness_Enum.STRONG;
                        break;
                    case Elements_Enum.ELEC:
                        effectiveness = Effectiveness_Enum.WEAK;
                        break;
                }
                break;
            case Elements_Enum.ICE:
                switch (incomingAtk_E)
                {
                    case Elements_Enum.FIRE:
                        effectiveness = Effectiveness_Enum.WEAK;
                        break;
                    case Elements_Enum.ICE:
                        effectiveness = Effectiveness_Enum.SAME;
                        break;
                    case Elements_Enum.ELEC:
                        effectiveness = Effectiveness_Enum.STRONG;
                        break;
                }
                break;
            case Elements_Enum.ELEC:
                switch (incomingAtk_E)
                {
                    case Elements_Enum.FIRE:
                        effectiveness = Effectiveness_Enum.STRONG;
                        break;
                    case Elements_Enum.ICE:
                        effectiveness = Effectiveness_Enum.WEAK;
                        break;
                    case Elements_Enum.ELEC:
                        effectiveness = Effectiveness_Enum.SAME;
                        break;
                }
                break;
            

        }

        return effectiveness;

    }


    public void HitBarrier(int incomingDamage, Elements_Enum incomingDamageElement, out int unBlockedDamage, out bool gainOrNot)
    {
        int unabsorbedDamage = 0;
        bool gainCharge = false;

        Effectiveness_Enum effectiveness = GetElementEffectiveness(barrierElement, incomingDamageElement);

        switch (effectiveness)
        {
            case Effectiveness_Enum.SAME:
                unabsorbedDamage = Mathf.FloorToInt(incomingDamage * 0.25f);

                unabsorbedDamage = Mathf.Max(unabsorbedDamage, 0);

                break;
            case Effectiveness_Enum.WEAK:
                unabsorbedDamage = Mathf.FloorToInt(incomingDamage * 0.75f);

                unabsorbedDamage = Mathf.Max(unabsorbedDamage, 0);
                break;
            case Effectiveness_Enum.STRONG:
                unabsorbedDamage = 0;
                gainCharge = true;
                break;
            case Effectiveness_Enum.NEUTRAL:
                unabsorbedDamage = Mathf.FloorToInt(incomingDamage * 0.50f);

                unabsorbedDamage = Mathf.Max(unabsorbedDamage, 0);
                break;
        }

        unBlockedDamage = unabsorbedDamage;
        gainOrNot = gainCharge;
    }


}
