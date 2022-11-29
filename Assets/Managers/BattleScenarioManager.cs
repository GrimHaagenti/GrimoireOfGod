using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScenarioManager : Levelnfo
{

    static public BattleScenarioManager _SCENERIO;

    private void Awake()
    {
        if(_SCENERIO != null && _SCENERIO != this)
        {
            Destroy(_SCENERIO);
        }else
        {
            _SCENERIO = this;
        }

    }


}
