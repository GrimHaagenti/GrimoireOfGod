using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EntityStats", menuName = "Stats/EntityWorldStats")]
public class EntityWorldStats : ScriptableObject
{
    public float MaxWalkSpeed = 25;
    public float Acceleration = 5;
    public float Decceleration = 5;
    public bool ActionInterrumptMovement = false;
    public float turnSmoothSpeed = 3f;
    public float turnSmoothTime = 0.1f;


    //Spellsword Stats
    public float SlashDistace = 10;
    public float SlashTime = 0.5f;
    public float SlashSpeed;



}
