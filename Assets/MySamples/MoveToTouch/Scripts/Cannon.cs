using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Cannon : MonoBehaviour
{
    Rigidbody bulletPhysics;

    void Start()
    {
        bulletPhysics = this.GetComponent<Rigidbody>();
        Fire();
    }

    void Update()
    {
    }

    void Fire()
    {
        bulletPhysics.AddForce(this.transform.forward * 500.0f);
    }
}
