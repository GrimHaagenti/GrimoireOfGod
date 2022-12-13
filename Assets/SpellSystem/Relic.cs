using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum RelicType { FUSION, INDIVIDUAL, BATTERY, NONE}
[CreateAssetMenu(fileName ="Relic", menuName ="New Relic")]
public class Relic : ScriptableObject
{
    [SerializeField] protected Sprite runeIcon;
    [SerializeField] protected string relicName;
    [SerializeField] [Range(1, 9)] 
    protected int Potency;
    [SerializeField] private RelicType relicType = RelicType.NONE;
    [SerializeField] protected Elements DefaultAttribute;
    [SerializeField] private bool IsCharacterSpecific;
    [SerializeField] private RelicAction[] effects;

    [SerializeField] public List<ElementalBlock> relicsElement = new List<ElementalBlock>();

    [SerializeField] public ElementalBlock DefaultElement;

    public TurnResolution Use(List<Entity> targets, Entity user)
    {

        TurnResolution resolution = new TurnResolution();
        if (relicsElement.Count <= 0 || relicsElement == null) { relicsElement = new List<ElementalBlock>() { DefaultElement }; }
        for (int i  = 0; i < effects.Length; i++)
        {
            switch (relicType)
            {
                case RelicType.BATTERY:
                case RelicType.FUSION:
                    ElementalBlock elem = FuseElements(relicsElement);
                    resolution = effects[i].Use(targets, elem, user, Potency, resolution);

                    break;
                case RelicType.INDIVIDUAL:
                    for (int x = 0; x < relicsElement.Count; x++)
                    {
                        resolution = effects[i].Use(targets, relicsElement[x], user, Potency, resolution);
                        

                    }
                    break;

                default:
                case RelicType.NONE:
                    break;
            }

        }
        relicsElement.ForEach((it) =>
        {
            if (user._ElementInventory.Contains(it))
            {
                user._ElementInventory.Remove(it);
            }
        });
        
        

        relicsElement.Clear();
        return resolution;
    }
    private ElementalBlock FuseElements(List<ElementalBlock> elem)
    {
        return  GameManager._GAME_MANAGER._ELEMENT_FACTORY.ElementFusion(elem);
    }
        public bool AddToRelicElements(ElementalBlock element)
    {
        if (relicsElement.Count > 3) { return false; }


        relicsElement.Add(element);

        return true;

    }
    
    public Sprite RuneIcon { get { return runeIcon; } }
}
