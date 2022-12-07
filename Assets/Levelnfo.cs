using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Levelnfo : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject CameraObject;
    public GameObject enemyPosition;
    public Vector3 LastPlayerPosition;


    [SerializeField] public TextMeshPro PlayerHealth; 
    [SerializeField] public TextMeshPro EnemyHealth; 

}
