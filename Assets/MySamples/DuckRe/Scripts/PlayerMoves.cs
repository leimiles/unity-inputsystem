using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
    Vector3 attackDirection;
    float rotationSpeed = 2160.0f;
    bool canAttack = false;
    Camera mainCamera;
    void Start()
    {
        attackDirection = transform.forward;
        mainCamera = Camera.main;
    }
    void Update()
    {
        Turn();
    }

    void Turn()
    {
        Quaternion targetRotation = Quaternion.LookRotation(attackDirection, Vector3.up);
        if (Quaternion.Angle(transform.rotation, targetRotation) > 1.0f)
        {
            canAttack = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            canAttack = true;
        }
    }

}
