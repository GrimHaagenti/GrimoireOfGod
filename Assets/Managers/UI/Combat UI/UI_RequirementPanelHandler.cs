using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RequirementPanelHandler : UI_ParentStandard
{

    [SerializeField] private List<Image> requirementIcons;

    public void SetRequirementIcons(List<Elements_Enum> requirements)
    {
        if(requirements.Count > 3)
        {
            Debug.Log("Too many requirements");
            return;
        }

        for (int i = 0; i < requirementIcons.Count; i++)
        {
            Sprite spr = null;
            Color color = Color.clear;
            if (i < requirements.Count ) 
            {
                spr = BattleUIManager._BATTLE_UI_MANAGER.ElementIcons[(int)requirements[i]];
                color = Color.white;
            }

            requirementIcons[i].sprite = spr;
            requirementIcons[i].color = color;

        }

    }
}
