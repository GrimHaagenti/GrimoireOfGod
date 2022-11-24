using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Elements { FIRE, WATER, EARTH, WIND, METAL, PLANT, ELECTRICITY, ICE, ROCK}
public class ElementalBlock
{
    public static ElementalBlock CreateElement(Elements element, Sprite sprite, int level)
    {
        ElementalBlock elem = new ElementalBlock();
        elem.BlockElement = element;
        elem.blockSprite = sprite;
        elem.Level = level;
        return elem;
    }
    private ElementalBlock() { }
    [SerializeField] public Elements BlockElement { get; private set; }

    [SerializeField] public int Level { get; private set; }
    [SerializeField] public Sprite blockSprite { get; private set; }
    
}
