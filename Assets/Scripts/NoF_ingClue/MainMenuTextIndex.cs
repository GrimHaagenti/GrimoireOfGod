using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuTextIndex : MonoBehaviour
{
    //STATS
    [SerializeField] public TextMeshProUGUI PlayerName;
    [SerializeField] public TextMeshProUGUI PlayerHP;
    [SerializeField] public TextMeshProUGUI PlayerATK;
    [SerializeField] public TextMeshProUGUI PlayerDEF;
    [SerializeField] public TextMeshProUGUI PlayerSPD;

    [SerializeField] public Image MainRelic;
    [SerializeField] public Image[] SubRelics;

    [SerializeField] public TextMeshProUGUI[] ElementQuantity;
    [SerializeField] public TextMeshProUGUI Message;
}
