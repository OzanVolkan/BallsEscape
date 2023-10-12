using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class GameManager : SingletonManager<GameManager>
{
    public GameData gameData;

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


    private void Awake()
    {
        //OnLoad();
    }
    private void Start()
    {


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

    #region EVENTS
    private void OnEnable()
    {

        EventManager.AddHandler(GameEvent.OnSave, new Action(OnSave));
    }

    private void OnDisable()
    {

        EventManager.RemoveHandler(GameEvent.OnSave, new Action(OnSave));
    }

    #endregion

    #region SAVE & LOAD
    void OnSave()
    {
        SaveManager.SaveData(gameData);
    }

    void OnLoad()
    {
#if !UNITY_EDITOR
        SaveManager.LoadData(gameData);
#endif
    }
    public void OnApplicationQuit()
    {
        OnSave();
    }
    public void OnApplicationFocus(bool focus)
    {
        if (focus == false) OnSave();
    }
    public void OnApplicationPause(bool pause)
    {
        if (pause == true) OnSave();
    }
    #endregion
}
