using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;

    public PauseUI pauseUI;
    public GameEndUI gameEndUI;
    public SettingUI settingUI;
    [SerializeField] private Button SettingBtn;
    [SerializeField] private Button PauseBtn;
    public event EventHandler OnPauseBtnCick;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SettingBtn.onClick.AddListener(() => {
            settingUI.Show();
        });
        PauseBtn.onClick.AddListener(() =>
        {
            OnPauseBtnCick?.Invoke(this, EventArgs.Empty);
        });

    }

}
