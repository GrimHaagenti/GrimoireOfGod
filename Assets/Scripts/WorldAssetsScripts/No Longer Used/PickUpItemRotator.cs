using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemRotator : MonoBehaviour
{
    [SerializeField] float RotationSpeed;

    Vector3 currentRotation;

    private void Update()
    {
        currentRotation = gameObject.transform.rotation.eulerAngles;
        currentRotation.y += RotationSpeed *Time.deltaTime;
        gameObject.transform.rotation= Quaternion.Euler(currentRotation);
    }


}
