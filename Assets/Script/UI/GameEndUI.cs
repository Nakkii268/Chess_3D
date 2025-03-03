using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndUI : UIElement
{
    [SerializeField] private Button MenuBtn;
    [SerializeField] private Button ReplayBtn;
    [SerializeField] private Button NextLvBtn;
    [SerializeField] private Transform WinText;
    [SerializeField] private Transform LoseText;
    [SerializeField] private TextMeshProUGUI score;
    public void SetScore(float point)
    {
        score.text = "Score:" + point;
    }

    private void Start()
    {
        MenuBtn.onClick.AddListener(() =>
        {
            SceneController.LoadScene(SceneController.Scene.MenuScene);
        });
        ReplayBtn.onClick.AddListener(() =>
        {
            SceneController.ReloadScene();
        });
        if(GameManager.Instance.GetCurrentMapIndex()+1 > GameManager.Instance.GetMapListCount()-1) {
            NextLvBtn.gameObject.SetActive(false);

        }
        NextLvBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.NextLevel();
            SceneController.ReloadScene();
        });

        Hide();
    }


    public void ShowWin()
    {
        gameObject.SetActive(true);
        WinText.gameObject.SetActive(true);
        LoseText.gameObject.SetActive(false);
    }
    public void ShowLose()
    {
        gameObject.SetActive(true);
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(true);
        NextLvBtn.gameObject.SetActive(false) ;
    }
  

}
