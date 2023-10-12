using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class GameManager : SingletonManager<GameManager>
{
    private GameObject[] gridalGroup;

    private Transform currentGrill;
    public Transform CurrentGrill
    {
        get { return currentGrill; }
        set { currentGrill = value; }
    }

    private bool isGridMoving;
    public bool IsGridMoving
    {
        get { return isGridMoving; }
        set { isGridMoving = value; }
    }


    private LayerMask mask = 1 << 6;

    private void Start()
    {
        gridalGroup = GameObject.FindGameObjectsWithTag("Gridal");

        foreach (var item in gridalGroup)
        {
            item.transform.DOShakeScale(1f, 0.25f, 5);
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                currentGrill = hit.transform;
            }
        }
    }

    public void GrillMovement(Vector3 vector)
    {
        if (CurrentGrill == null)
            return;

        isGridMoving = true;

        Vector3 initialPosition = CurrentGrill.localPosition;
        Vector3 targetPosition = initialPosition + vector;

        CurrentGrill.DOLocalMove(targetPosition, 0.25f).OnComplete(() =>
        {

            CurrentGrill.GetComponent<Grill>().CollisionCheck(initialPosition);
            CurrentGrill = null;
            isGridMoving = false;

            StartCoroutine(BallManager.Instance.CheckIfBallsFree());
        });

    }

    public void MoveAwayGridals()
    {
        foreach (var item in gridalGroup)
        {
            item.transform.DOMoveZ(item.transform.position.z + 15f, 0.5f).SetEase(Ease.InBack);
        }
    }
}
