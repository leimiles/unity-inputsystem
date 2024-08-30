using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
    Vector3 attackDirection;
    float rotationSpeed = 1080.0f;
    void Start()
    {
        attackDirection = transform.forward;
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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

    public void OnAttack()
    {
        Debug.Log("sent");
    }
}
