using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Prefab_BarrierScript : MonoBehaviour
{

    [SerializeField] private float BarrierMaxSize = 20;

    [SerializeField] private float sizeSpeed = 1;

    private bool run = false;

    private Vector3 targetSize;

    [SerializeField] private Color FireColor;
    [SerializeField] private Color IceColor;
    [SerializeField] private Color ElecColor;


    [SerializeField] MeshRenderer mesh;

    public UnityEvent BarrierAnimationFinished;



    public void ActivateBarrier(Elements_Enum elem)
    {
        switch (elem)
        {
            case Elements_Enum.FIRE:
                mesh.material.SetColor("_Color", FireColor);
                break;
            case Elements_Enum.ICE:
                mesh.material.SetColor("_Color", IceColor);
                break;
            case Elements_Enum.ELEC:
                mesh.material.SetColor("_Color", ElecColor);
                break;
        }
        targetSize = Vector3.one * BarrierMaxSize;
        run= true;
    }
    public void DeactivateBarrier()
    {
        targetSize = Vector3.zero;
        run= true;
    }

    void Update()
    {
        if(run)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetSize, Time.deltaTime * sizeSpeed);

            

            if (Vector3.Distance(transform.localScale, targetSize) < 0.1f)
            {
                BarrierAnimationFinished?.Invoke();
                run = false;
            }
        }

    }
}
