using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Elements { FIRE, WATER, EARTH, WIND, METAL, PLANT, ELECTRICITY, ICE, ROCK, PHYSICAL, NO_ELEMENT}
public enum ElementLevel { ONE, TWO, THREE, LAST_NO_LEVEL}
[CreateAssetMenu(menuName ="Elemental Shape")]
public class ElementalBlock: ScriptableObject
{
    [SerializeField] public Elements BlockElement;
    [SerializeField] public ElementLevel Level;
    [SerializeField] public int Potency;

}
