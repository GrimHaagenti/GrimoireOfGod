using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractionScript : MonoBehaviour
{

    [SerializeField] WorldInteractIE ObjectToTrigger;
    [SerializeField] float maxCastDistance;
    [SerializeField] int levelLayer;
    [SerializeField] int DetectCone = 30;
    float SphereCastSize = 6f;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);

        if (GameManager._GAME_MANAGER._INPUT_MANAGER.ActionButtonPressed)
        {
            RaycastHit hit;
            Vector3 fwd = other.gameObject.transform.forward;

            Vector3 ForwardDir = (other.gameObject.transform.position + (fwd* maxCastDistance) ) - other.gameObject.transform.position;
            Vector3 AngleDir1 = (other.gameObject.transform.position + (Quaternion.AngleAxis(DetectCone, Vector3.up)*fwd)* maxCastDistance) - other.gameObject.transform.position;
            Vector3 AngleDir2 = (other.gameObject.transform.position + (Quaternion.AngleAxis(-DetectCone, Vector3.up)*fwd)* maxCastDistance) - other.gameObject.transform.position;
            Debug.DrawRay(other.gameObject.transform.position, ForwardDir, Color.blue, 5f);
            Debug.DrawRay(other.gameObject.transform.position, AngleDir1, Color.blue, 5f);
            Debug.DrawRay(other.gameObject.transform.position, AngleDir2, Color.blue, 5f);


            if (Physics.SphereCast(other.gameObject.transform.position, SphereCastSize, ForwardDir, out hit,maxCastDistance, 1 << (levelLayer)) ||
                Physics.SphereCast(other.gameObject.transform.position, SphereCastSize, AngleDir1, out hit, maxCastDistance, 1 << (levelLayer)) ||
                Physics.SphereCast(other.gameObject.transform.position, SphereCastSize, AngleDir2, out hit, maxCastDistance, 1 << (levelLayer))
                )
            {
                Debug.Log("AAA");
                ObjectToTrigger.Interact();
            }
        }
    }
}
