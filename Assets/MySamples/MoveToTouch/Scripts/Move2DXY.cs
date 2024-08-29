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

    [SerializeField]
    Transform ownTarget;

    //[SerializeField]
    float fireInterval = 3.0f;
    float reloadingTime;

    bool IsDirectionReady = false;
    bool IsRangeReady = false;
    Vector3 targetPosition;
    Vector3 targetDirection;

    void Start()
    {
        reloadingTime = fireInterval;
    }

    void Update()
    {
        if (ownTarget)
        {
            targetPosition = ownTarget.transform.position;
            Move(fireRange);
            Fire();
        }
        else
        {
            targetPosition = MoveToTouchManager.WorldPositionTarget;
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

        if (reloadingTime < fireInterval)
        {
            reloadingTime += Time.deltaTime;
        }
    }

    void Fire()
    {
        if (IsDirectionReady && IsRangeReady)
        {
            if (reloadingTime >= fireInterval)
            {
                Debug.Log(this.name + " Fires");
                reloadingTime = 0.0f;
            }
        }
    }

    void Move(float range)
    {
        targetDirection = (targetPosition - transform.position).normalized;
        if (targetDirection.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
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
        if (Vector3.Distance(transform.position, targetPosition) > range)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            IsRangeReady = false;
        }
        else
        {
            IsRangeReady = true;
        }
    }

}
