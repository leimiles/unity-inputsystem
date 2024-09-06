using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask buttonLayer;
    [SerializeField] Text debugText;

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


    public void TurnFront()
    {
        attackDirection = Vector3.forward;
    }
    public void TurnBack()
    {
        attackDirection = Vector3.back;
    }

    // invoked by playerinput which is a part of input system
    public void OnTouchPosition(InputValue inputValue)
    {
        Debug.Log("pos");
        debugText.text = inputValue.Get<Vector2>().ToString();
    }

    private void GetButtonNameByInputPosition(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, buttonLayer))
        {
        }
    }
}
