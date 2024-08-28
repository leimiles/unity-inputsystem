using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move2DXY : MonoBehaviour
{

    void Update()
    {
        Move();
    }

    void Move()
    {
        this.transform.position = MoveToTouchManager.WorldPositionTarget;
    }

}
