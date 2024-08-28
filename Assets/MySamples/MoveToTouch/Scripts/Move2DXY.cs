using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move2DXY : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float rotationSpeed = 720f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, MoveToTouchManager.WorldPositionTarget) > 0.1f)
        {
            Vector3 directionToTarget = (MoveToTouchManager.WorldPositionTarget - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.back);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, MoveToTouchManager.WorldPositionTarget, speed * Time.deltaTime);
            }

        }
    }

}
