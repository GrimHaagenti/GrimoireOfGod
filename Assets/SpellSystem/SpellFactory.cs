using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFactory
{

    private Elements ElementCombination(Elements element1, Elements element2)
    {
        switch (element1)
        {

        }
        return element1; 
    }

    public ElementalBlock ElementFusion(List<ElementalBlock> elements)
    {
        ElementalBlock baseElem = elements[0];

        if (elements.Count == 1) { return baseElem; }

        ElementalBlock newElem = new ElementalBlock();

        for (int i = 0; i < elements.Count; i++)
        {
             //elements[i].BlockElement 
        }

        return baseElem;
    }
    
    
}
