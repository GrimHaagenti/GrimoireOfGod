using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float cameraAngle = 45;
    [SerializeField] float cameraRotation = 0;
    [SerializeField] float distanceToPlayer;
    
    public bool running = false;

    private void Awake()
    {
        GameManager._GAME_MANAGER._SCENE_MANAGER.OnSceneLoaded += SetupCamera;
        GameManager._GAME_MANAGER._SCENE_MANAGER.OnBattleUnloaded += SetupCamera;
        DontDestroyOnLoad(this);
    }

    private void SetupCamera()
    {
        running = false;
        if (GameManager._GAME_MANAGER.currentLevelInfo.CameraObject == null) { player = GameManager._GAME_MANAGER.PlayerPrefab; }
        else { player = GameManager._GAME_MANAGER.currentLevelInfo.CameraObject; }
        running = true;
    }
    private void LateUpdate()
    {
        if (running)
        {
            
            Vector3 position = Vector3.zero;

            gameObject.transform.rotation = Quaternion.Euler(cameraAngle, cameraRotation, 0);


            position = player.transform.position + (-transform.forward * distanceToPlayer);

            gameObject.transform.position = position;
        }
    }
}
