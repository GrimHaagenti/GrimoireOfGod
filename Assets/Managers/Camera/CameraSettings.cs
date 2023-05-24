using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newCameraSettings", menuName ="New/CameraSettings")]
public class CameraSettings : ScriptableObject
{
    [SerializeField] public float cameraAngle = 45;
    [SerializeField] public float cameraRotation = 0;
    [SerializeField] public float distanceToPlayer = 0;
    [SerializeField] public float VerticalPan = 0;
    [SerializeField] public float HorizontalPan = 0;
}