using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    [SerializeField] private MinimapCameraSetting settings;
    [SerializeField] private float cameraHeight;

    private void Awake()
    {
        settings = GetComponentInParent<MinimapCameraSetting>();
        cameraHeight = transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 targetPosition
            = settings.targetToFollow.transform.position;

        transform.position
            = new Vector3(
                targetPosition.x,
                targetPosition.y + cameraHeight,
                targetPosition.z
            );

            if(settings.rotateWithTheTarget)
            {
                Quaternion targetRotation
                    = settings.targetToFollow.transform.rotation;
                
                transform.rotation =
                Quaternion.Euler(
                    90,
                    targetRotation.eulerAngles.y,
                    0
                );
            }
    }
}
