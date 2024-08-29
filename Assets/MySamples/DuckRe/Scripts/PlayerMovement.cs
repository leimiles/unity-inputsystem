using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] float moveSpeed = 2.5f;

    void Update()
    {
        if (DuckReGameplay.gameStart && !KongFu.acting)
        {
            Move();
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, destination.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);
        }
    }
}
