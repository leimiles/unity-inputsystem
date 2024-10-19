using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldingButton2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] UnityEvent onHolding;
    [SerializeField] float longPressThreshold = 0.1f;
    [SerializeField] float repeatInterval = 1.0f;

    private bool isPressing = false;
    private bool isLongPressing = false;
    private float pressTime = 0f;
    private float intervalTimer = 0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        isLongPressing = false;
        pressTime = 0f;
        intervalTimer = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetPressState();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetPressState();
    }

    private void FixedUpdate()
    {
        if (isPressing)
        {
            pressTime += Time.deltaTime;

            if (pressTime >= longPressThreshold)
            {
                isLongPressing = true;
            }

            if (isLongPressing)
            {
                intervalTimer += Time.deltaTime;

                if (intervalTimer >= repeatInterval)
                {
                    MyFunction();
                    intervalTimer = 0f;
                }
            }
        }
    }

    private void ResetPressState()
    {
        isPressing = false;
        isLongPressing = false;
        pressTime = 0f;
        intervalTimer = 0f;
    }

    private void MyFunction()
    {
        onHolding.Invoke();
    }
}