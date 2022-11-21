using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFactory
{
   SpellFactory _SPELL_FACTORY;

    Spell currentSpell;

    ElementFactory elementFactory;

    SpellFactory()
    {
        elementFactory = new ElementFactory();
    }

    Spell CreateSpell(List<ElementalBlock> elements, Rune runeEffect)
    {
        ElementFactory elementFusionFactory = new ElementFactory();
        currentSpell = new Spell();



        return currentSpell;
    }
    
}
