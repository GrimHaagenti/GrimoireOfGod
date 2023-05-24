using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ElementalContainersHandler : UI_ParentStandard
{
    [SerializeField] private List<TextMeshProUGUI> elementChargeText;

    public void UpdateChargeText(New_Entity_Script playerStats)
    {
        
        elementChargeText[(int)Elements_Enum.FIRE].text = playerStats.ElementalChargesCurrentCapacity[(int)Elements_Enum.FIRE] + "/" + playerStats.ElementalChargesMaxCapacity[(int)Elements_Enum.FIRE];
        elementChargeText[(int)Elements_Enum.ICE].text = playerStats.ElementalChargesCurrentCapacity[(int)Elements_Enum.ICE] + "/" + playerStats.ElementalChargesMaxCapacity[(int)Elements_Enum.ICE];
        elementChargeText[(int)Elements_Enum.ELEC].text = playerStats.ElementalChargesCurrentCapacity[(int)Elements_Enum.ELEC] + "/" + playerStats.ElementalChargesMaxCapacity[(int)Elements_Enum.ELEC];
    }
}
