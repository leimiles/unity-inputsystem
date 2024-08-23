using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMove : MonoBehaviour
{
    public InputActionReference move;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = move.action.ReadValue<Vector2>();
        //Debug.Log(movement);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }
}
