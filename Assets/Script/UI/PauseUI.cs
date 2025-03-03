using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : UIElement
{
    [SerializeField] private Button MenuBtn;
    [SerializeField] private Button ReplayBtn;
    [SerializeField] private Button ResumeBtn;
    void Start()
    {
        Hide();
        MenuBtn.onClick.AddListener(() => {
            SceneController.LoadScene(SceneController.Scene.MenuScene);
        });
        ReplayBtn.onClick.AddListener(() => {
            SceneController.ReloadScene();
        });
        ResumeBtn.onClick.AddListener(() => {
            Hide() ;
        });
        InGameUIManager.Instance.OnPauseBtnCick += InGameUIManager_OnPauseBtnCick;
    }

    private void InGameUIManager_OnPauseBtnCick(object sender, System.EventArgs e)
    {
        Show();
    }

 
}
