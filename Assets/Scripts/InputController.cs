using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
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
        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > 0)
            {
                GrillMovement(Vector3.right * moveMultiplier);
            }
            else
            {
                GrillMovement(Vector3.left * moveMultiplier);
            }
        }
        else
        {
            if (deltaY > 0)
            {
                GrillMovement(Vector3.forward * moveMultiplier);
            }
            else
            {
                GrillMovement(Vector3.back * moveMultiplier);
            }
        }
    }

    private void GrillMovement(Vector3 vector)
    {
        if (GameManager.Instance.CurrentGrill == null)
            return;

        Vector3 initialPosition = GameManager.Instance.CurrentGrill.localPosition;
        Vector3 targetPosition = initialPosition + vector;

        GameManager.Instance.CurrentGrill.DOLocalMove(targetPosition, 0.25f).OnComplete(() =>
        {

            GameManager.Instance.CurrentGrill.GetComponent<Grill>().CollisionCheck(initialPosition);
            GameManager.Instance.CurrentGrill = null;

        });

    }
}
