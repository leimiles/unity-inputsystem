using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class Move2DXY : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    float rotationSpeed = 720.0f;

    [SerializeField]
    float fireRange = 0.1f;

    bool IsDirectionReady = false;
    bool IsRangeReady = false;

    void Update()
    {
        if (MoveToTouchManager.EnemyTarget)
        {
            Move(fireRange);
            Fire();
        }
        else
        {
            Move(0.1f);
        }
    }

    void Fire()
    {
        if (IsDirectionReady && IsRangeReady)
        {
        }

    }

    void Move(float range)
    {
        Vector3 directionToTarget = (MoveToTouchManager.WorldPositionTarget - transform.position).normalized;
        if (directionToTarget.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            if (Quaternion.Angle(transform.rotation, targetRotation) > 1.0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                IsDirectionReady = false;
            }
            else
            {
                IsDirectionReady = true;
            }
        }
        if (Vector3.Distance(transform.position, MoveToTouchManager.WorldPositionTarget) > range && IsDirectionReady)
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveToTouchManager.WorldPositionTarget, speed * Time.deltaTime);
            IsRangeReady = false;
        }
        else
        {
            IsRangeReady = true;
        }
    }

}
