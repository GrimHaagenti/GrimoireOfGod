using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFactory
{
    ElementFactory _ELEMENT_FACTORY;

    List<ElementalBlock> elements;

    public List<Sprite> sprites;

    public ElementalBlock CreateElement(Elements element)
    {
        return ElementalBlock.CreateElement(element, sprites[(int)element]);
        

    }

    /*public ElementalBlock FuseElement(List<ElementalBlock> elementsToBeFused)
    {


        return new ElementalBlock(Elements.FIRE);
    }
    */
   
}
