using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraScript : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] float cameraAngle = 45;
    [SerializeField] float cameraRotation = 0;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float VerticalPan = 0;
    [SerializeField] float HorizontalPan = 0;

    [SerializeField] CameraSettings settings;
    [SerializeField] CameraSettings OverWorldStandard;
    [SerializeField] CameraSettings ChooseEnemySettings;
    [SerializeField] CameraSettings StandardCombatSettings;

    [SerializeField] float CameraSpeed = 1;

    public UnityEvent CameraEndMove;


    public bool running = false;
    public bool useSettings = false;

    private void Awake()
    {
        GameManager._GAME_MANAGER.LevelLoaded.AddListener( SetupCamera);
        GameManager._GAME_MANAGER.CameraIHateU.AddListener(()=>{ running = false; });
        running= true;

        
        DontDestroyOnLoad(this);
    }
    
    private void ChooseEnemyCamera()
    {
        settings = ChooseEnemySettings;
        //useSettings = true;
    }
    private void DefaultCombatCamera()
    {
        settings = StandardCombatSettings;
        //useSettings = true;
    }

    public void OverworldCamera()
    {
        settings = OverWorldStandard;
    }

    public void SetupCamera()
    {
        running = false;
        player = GameManager._GAME_MANAGER.CameraObj;
        if(GameManager._GAME_MANAGER._SCENE_MANAGER.currentSceneIndex == Scenes.WORLD) {
            OverworldCamera();
        }
        else
        {
            DefaultCombatCamera();
            BattleUIManager._BATTLE_UI_MANAGER?.Camera2ChooseEnemy.AddListener(ChooseEnemyCamera);
            BattleUIManager._BATTLE_UI_MANAGER?.Camera2Standard.AddListener(DefaultCombatCamera);
        }
        running = true;
    }

    private void Update()
    {
        if (useSettings) {

            SetSettings();
            useSettings= false;
        }
    }

    void LerpCameraValues()
    {
        
            cameraAngle = Mathf.Lerp(cameraAngle, settings.cameraAngle, Time.deltaTime);
            cameraRotation = Mathf.Lerp(cameraRotation, settings.cameraRotation, Time.deltaTime);
            distanceToPlayer = Mathf.Lerp(distanceToPlayer, settings.distanceToPlayer, Time.deltaTime);
            VerticalPan = Mathf.Lerp(VerticalPan, settings.VerticalPan, Time.deltaTime);
            HorizontalPan = Mathf.Lerp(HorizontalPan, settings.HorizontalPan, Time.deltaTime);
        
    }
    public void SetSettings()
    {
        cameraAngle = settings.cameraAngle;
        cameraRotation = settings.cameraRotation;
        distanceToPlayer = settings.distanceToPlayer;
        VerticalPan = settings.VerticalPan;
        HorizontalPan = settings.HorizontalPan;
    }
    private void LateUpdate()
    {
        if (running)
        {
            
            Vector3 position = Vector3.zero;

            cameraAngle = Mathf.Lerp(cameraAngle, settings.cameraAngle, Time.deltaTime *CameraSpeed);
            cameraRotation = Mathf.Lerp(cameraRotation, settings.cameraRotation, Time.deltaTime * CameraSpeed);

            gameObject.transform.rotation = Quaternion.Euler(cameraAngle, cameraRotation, 0);

            distanceToPlayer = Mathf.Lerp(distanceToPlayer, settings.distanceToPlayer, Time.deltaTime * CameraSpeed);

            position = player.transform.position + (-transform.forward * distanceToPlayer);


            VerticalPan = Mathf.Lerp(VerticalPan, settings.VerticalPan, Time.deltaTime * CameraSpeed);
            HorizontalPan = Mathf.Lerp(HorizontalPan, settings.HorizontalPan, Time.deltaTime * CameraSpeed);

            position.y += VerticalPan;
            position += HorizontalPan * transform.right;

            if(Vector3.Distance( gameObject.transform.position, position) < 0.1f)
            {
                CameraEndMove?.Invoke();
            }

            gameObject.transform.position = position;

           


        }
    }
}
