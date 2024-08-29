using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class KongFu : MonoBehaviour
{
    public static bool acting = false;

    public void TopRightFight()
    {
        Debug.Log("top right");
    }
    public void MiddleRightFight()
    {
        Debug.Log("middle right");
    }
    public void BottomRightFight()
    {
        Debug.Log("bottom right");
    }
    public void TopLeftFight()
    {
        Debug.Log("top left");
    }
    public void MiddleLeftFight()
    {
        Debug.Log("middle left");
    }
    public void BottomLeftFight()
    {
        Debug.Log("bottom left");
    }
}
