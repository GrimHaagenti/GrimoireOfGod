using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElementFactory
{
   
    [SerializeField] public List<Sprite> ElementsSprites;
    [SerializeField] public List<ElementalBlock> GameElement;



    public void Init()
    {
    }


    private Elements ElementCombination(Elements element1, Elements element2)
    {
        if (element2 == Elements.NO_ELEMENT) { return Elements.NO_ELEMENT; }
        if (element1 == Elements.NO_ELEMENT) { return element2; }
        
        if (GameManager._GAME_MANAGER._DB_MANAGER.GetElementCombination(element1, element2, out Elements result))
        {
            return result;
        }


        return element1;
    }

    public ElementalBlock ElementFusion(List<ElementalBlock> elements)
    {
        ElementalBlock baseElem = elements[0];

        if (elements.Count == 1) { return baseElem; }

        ElementalBlock newElemBlock = ScriptableObject.CreateInstance<ElementalBlock>();
        Elements newElem = Elements.NO_ELEMENT;
        int level = 0;
        for (int i = 0; i < elements.Count; i++)
        {
            level += (int)elements[i].Level+1;
            
            if (newElem != elements[i].BlockElement || level >= 3)
            {
                newElem = ElementCombination(newElem, elements[i].BlockElement);
                //level = Mathf.CeilToInt(level / 3);
            }

        }
        newElemBlock.Potency = level;
        ElementLevel newLevel = (ElementLevel)Mathf.CeilToInt(level / 3);

        newElemBlock.BlockElement = newElem;

        return newElemBlock;
    }

    // public ElementalBlock CreateElement(Elements element)
    //return ElementalBlock.CreateElement(element, sprites[(int)element],  1);



    /*public ElementalBlock FuseElement(List<ElementalBlock> elementsToBeFused)
    {


        return new ElementalBlock(Elements.FIRE);
    }
    */

}
