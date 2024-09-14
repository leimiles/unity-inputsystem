using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class RotationInput : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Light mainLight;
    bool validated = false;

    Vector3 eyesTarget = Vector3.zero;

    void Start()
    {
        if (mainCamera == null || mainLight == null)
        {
            validated = false;
        }
        else
        {
            validated = true;
        }
    }

    void Update()
    {

    }

    void RotateCamera()
    {
        Vector3 targetDirection = (eyesTarget - mainCamera.transform.position).normalized;
        if (targetDirection.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        }

    }

    void RotateLight()
    {

    }
}
