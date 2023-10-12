using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : SingletonManager<UIManager>
{
    //public TextMeshProUGUI levelCounter;

    [SerializeField] private TextMeshProUGUI ballRatio;
    [SerializeField] private GameObject winPanel, gamePanel;

    private void Start()
    {
        UpdateCollectedAmount(0);
    }

    public void UpdateCollectedAmount(int _collectedAmount)
    {
        ballRatio.text = _collectedAmount + "/" + BallManager.Instance.TotalAmount;
    }

    public void WinPanelSwitch()
    {
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    #region BUTTONS

    public void NextButton()
    {
        int _index = SceneManager.GetActiveScene().buildIndex;

        if (_index < 4)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    public void RefreshButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

}
