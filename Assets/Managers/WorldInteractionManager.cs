using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractionManager : MonoBehaviour
{
    public static WorldInteractionManager _INTERACT_MANAGER;

    [SerializeField]List<WorldInteractObject> _interactObjects;

    private void Awake()
    {
        if (_INTERACT_MANAGER != null && _INTERACT_MANAGER != this)
        {
            Destroy(this);
        }
        else
        {
            _INTERACT_MANAGER = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    public void DeactivateObjectByChannel(int channel) 
    {
        _interactObjects.ForEach(io => { 
            if (io.WorldInteractChannel == channel) 
            { 
                io.gameObject.SetActive(false);
            } 
        });
    }
    public void ActivateObjectByChannel(int channel) 
    {
        _interactObjects.ForEach(io => { 
            if (io.WorldInteractChannel == channel) 
            { 
                io.gameObject.SetActive(true);
            } 
        });
    }
    public void ActivateChannel(int channel)
    {
        _interactObjects.ForEach(obj =>
        {
            if (obj.WorldInteractChannel == channel)
            {
                obj.Activate();
            }
        });
    }
}
