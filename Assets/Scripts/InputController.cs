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
            // S�r�kleme yatay y�nde ger�ekle�iyor.
            if (deltaX > 0)
            {
                // Pozitif X y�nde s�r�klendi.
                Debug.Log("S�r�kleme sa�a do�ru.");
                GrillMovement(Vector3.right * moveMultiplier);
            }
            else
            {
                // Negatif X y�nde s�r�klendi.
                Debug.Log("S�r�kleme sola do�ru.");
                GrillMovement(Vector3.left * moveMultiplier);
            }
        }
        else
        {
            // S�r�kleme dikey y�nde ger�ekle�iyor.
            if (deltaY > 0)
            {
                // Pozitif Y y�nde s�r�klendi.
                Debug.Log("S�r�kleme yukar� do�ru.");
                GrillMovement(Vector3.forward * moveMultiplier);

            }
            else
            {
                // Negatif Y y�nde s�r�klendi.
                Debug.Log("S�r�kleme a�a�� do�ru.");
                GrillMovement(Vector3.back * moveMultiplier);

            }
        }
    }

    private void GrillMovement(Vector3 vector)
    {
        if (GameManager.Instance.CurrentGrill == null)
            return;

        Vector3 pos = GameManager.Instance.CurrentGrill.localPosition;

        GameManager.Instance.CurrentGrill.DOLocalMove(pos + vector, 0.25f);
        GameManager.Instance.CurrentGrill = null;
    }
}
