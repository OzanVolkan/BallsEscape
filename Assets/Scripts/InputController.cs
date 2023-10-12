using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private float deltaX, deltaY;
    private float moveMultiplier = 1.93f;
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        deltaX = eventData.delta.x;
        deltaY = eventData.delta.y;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.IsGridMoving || GameManager.Instance.CurrentGrill == null)
            return;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > 0)
            {
                GameManager.Instance.GrillMovement(Vector3.right * moveMultiplier);
                AnimationManager.Instance.GridalShake("Right");
            }
            else
            {
                GameManager.Instance.GrillMovement(Vector3.left * moveMultiplier);
                AnimationManager.Instance.GridalShake("Left");
            }
        }
        else
        {
            if (deltaY > 0)
            {
                GameManager.Instance.GrillMovement(Vector3.forward * moveMultiplier);
                AnimationManager.Instance.GridalShake("Up");
            }
            else
            {
                GameManager.Instance.GrillMovement(Vector3.back * moveMultiplier);
                AnimationManager.Instance.GridalShake("Down");
            }
        }
    }

    
}
