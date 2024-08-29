using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 targetPosition;
    public float Speed { get => speed; set => speed = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }

    void Update()
    {
        Fly();
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    void Fly()
    {
        if (Vector3.Distance(transform.position, TargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
