using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    [SerializeField] protected AnimationClip StateAnimation;
    public abstract void Act(StateController controller);
}
