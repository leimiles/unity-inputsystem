using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class HoldingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public float longPressThreshold = 0.2f; // how long holding to active
    private bool isPressing = false;
    private bool isLongPressing = false;

    // on click
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        isLongPressing = false;
        StartCoroutine(LongPressCoroutine());
    }

    // on release
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }

    // on not in the area
    public void OnPointerExit(PointerEventData eventData)
    {
        isPressing = false;
    }

    // holding and invoke
    private IEnumerator LongPressCoroutine()
    {
        float pressTime = 0f;

        while (isPressing)
        {
            pressTime += Time.deltaTime;

            if (pressTime >= longPressThreshold && !isLongPressing)
            {
                isLongPressing = true;
                //Debug.Log("Long press started");
                StartCoroutine(InvokeRepeatedFunction());
            }

            yield return null;
        }

        StopCoroutine(InvokeRepeatedFunction());
    }

    private IEnumerator InvokeRepeatedFunction()
    {
        while (isPressing)
        {
            MyFun();
            yield return new WaitForSeconds(0.1f);  // invoke interval
        }
    }

    private void MyFun()
    {
        Debug.Log("This is my fun");
    }
}