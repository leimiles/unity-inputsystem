using System;
using UnityEngine;

[DisallowMultipleComponent]
public class RotationController : MonoBehaviour
{
    [SerializeField] Transform lookAtTarget;
    [SerializeField] float lookAtTargetMoveSpeed = 10.0f;
    [SerializeField] Camera mainCamera;
    [SerializeField] Light mainLight;


    void Update()
    {
        SetLookAtTargetPosition();
        SetCameraPosition();
    }

    void SetCameraPosition()
    {
        mainCamera.transform.position = RotateAroundManager.sphericalCoordinateSystem.GetCartesianPosition();
    }
    void SetLookAtTargetPosition()
    {
        lookAtTarget.position = Vector3.MoveTowards(lookAtTarget.position, RotateAroundManager.HitPosition, Time.deltaTime * lookAtTargetMoveSpeed);
    }

}
