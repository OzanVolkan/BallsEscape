using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : SingletonManager<GameManager>
{
    public GameData gameData;

    private Transform currentGrill;

    public Transform CurrentGrill
    {
        get { return currentGrill; }
        set { currentGrill = value; }
    }


    private LayerMask mask = 1 << 6;



    private void OnEnable()
    {
        
        EventManager.AddHandler(GameEvent.OnSave, new Action(OnSave));
    }

    private void OnDisable()
    {
        
        EventManager.RemoveHandler(GameEvent.OnSave, new Action(OnSave));
    }
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

            // Layer mask kullanarak çarpýþmayý kontrol et
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                // Belirli layer'a sahip objeye dokunuldu
                Debug.Log("Belirli layer'a sahip objeye dokunuldu: " + hit.collider.gameObject.name);
                currentGrill = hit.transform;
            }
        }
    }

    #region EVENTS


    #endregion

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
}
