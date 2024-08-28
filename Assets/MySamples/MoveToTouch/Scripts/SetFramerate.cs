using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFramerate : MonoBehaviour
{
    [SerializeField]
    int framerate = 60;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = framerate;
    }

    // Update is called once per fram
}
