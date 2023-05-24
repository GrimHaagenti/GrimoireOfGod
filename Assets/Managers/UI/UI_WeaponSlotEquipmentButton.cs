using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponSlotEquipmentButton : MonoBehaviour
{
    [SerializeField] public Button weaponSlotButton;
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] Sprite EquippedSprite;
    [SerializeField] Sprite NotEquippedSprite;
    public void SetName(string newName)
    {
        weaponSlotButton.image.sprite = EquippedSprite;
        weaponName.text = newName;
    }
    public void SetName()
    {
        weaponSlotButton.image.sprite = NotEquippedSprite;
        weaponName.text = "-----";
    }
}
