using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float cameraAngle = 45;
    [SerializeField] float cameraRotation = 0;
    [SerializeField] float distanceToPlayer;

    private void LateUpdate()
    {
        Vector3 position = Vector3.zero;

        gameObject.transform.rotation = Quaternion.Euler(cameraAngle, cameraRotation, 0);


        position = player.transform.position + (-transform.forward * distanceToPlayer);

        gameObject.transform.position = position;

    }
}
