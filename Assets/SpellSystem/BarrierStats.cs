using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Barrier", menuName ="Barrier/BarrierStats")]
public class BarrierStats : ScriptableObject
{
    [SerializeField] public int baseBarrierHP;
    [SerializeField] public int elementalBarrierHP;
    [SerializeField] public Elements barrierElement = Elements.NO_ELEMENT;
}
