using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributesEnum {SLASH, FIRE, WATER, WIND };

[CreateAssetMenu(menuName="Attribute")]
public class Attributes : ScriptableObject
{
    public AttributesEnum attribute;
}
