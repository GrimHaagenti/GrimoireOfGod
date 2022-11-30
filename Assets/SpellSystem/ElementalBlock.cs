using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Elements { FIRE, WATER, EARTH, WIND, METAL, PLANT, ELECTRICITY, ICE, ROCK}
public enum ElementLevel { ONE, TWO, THREE}
[CreateAssetMenu(menuName ="Elemental Shape")]
public class ElementalBlock: ScriptableObject
{
    [SerializeField] public Elements BlockElement;
    [SerializeField] public ElementLevel level;
    [SerializeField] public Sprite blockSprite;
    [SerializeField] public int Quantity;

}
