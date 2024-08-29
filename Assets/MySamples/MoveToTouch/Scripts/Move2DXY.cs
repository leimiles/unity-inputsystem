using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class Move2DXY : MonoBehaviour
{
    [SerializeField] Transform attackTarget;
    [SerializeField] GameObject bullet;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float rotationSpeed = 720.0f;
    [SerializeField] float fireRange = 5.0f;
    [SerializeField] float fireInterval = 3.0f;
    [SerializeField] float fireSpeed = 5.0f;
    [SerializeField] Transform fireOrigin;
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
        if (attackTarget)
        {
            targetPosition = attackTarget.transform.position;
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
        if (bullet == null)
        {
            return;
        }
        if (IsDirectionReady && IsRangeReady)
        {
            if (reloadingTime >= fireInterval)
            {
                var newBullet = Instantiate(bullet, (fireOrigin == null) ? this.transform.position : fireOrigin.position, this.transform.rotation);
                newBullet.GetComponent<Bullet>().TargetPosition = targetPosition;
                newBullet.GetComponent<Bullet>().Speed = fireSpeed;
                reloadingTime = 0.0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            IsRangeReady = false;
        }
        else
        {
            IsRangeReady = true;
        }
    }

}
