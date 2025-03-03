using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : UIElement
{
    [SerializeField]private Button playBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button leaderBoard;
    [SerializeField] private Button Shopbtn;
    [SerializeField] private Button settingBtn;

    [SerializeField] private TextMeshProUGUI UserName;
    [SerializeField] private TextMeshProUGUI UserCoin;



    private void Start()
    {
        GameManager.Instance.OnDataChange += GameManager_OnDataChange;
        MenuUIManager.Instance.GetNameSetUI().OnNameSet += MenuUI_OnNameSet;
        MenuUIManager.Instance.GetLeaderboardUI().Hide();
        playBtn.onClick.AddListener(()=>{
            MenuUIManager.Instance.GetMapSelectUI().Show();
        });
        quitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
        leaderBoard.onClick.AddListener(() => {
            
            MenuUIManager.Instance.GetLeaderboardUI().Show();
        });
        Shopbtn.onClick.AddListener(() =>
        {
            MenuUIManager.Instance.GetShopUI().Show();
        });
        settingBtn.onClick.AddListener(() =>
        {
            MenuUIManager.Instance.GetSettingUI().Show();
        });
        UserName.text = PlayerPrefs.GetString("UserName");

        UserCoin.text = GameManager.Instance.GetPlayerData().Coin.ToString();

    }

    private void MenuUI_OnNameSet(object sender, System.EventArgs e)
    {
        UserName.text = PlayerPrefs.GetString("UserName");

    }

    private void GameManager_OnDataChange(object sender, System.EventArgs e)
    {
        UserCoin.text = GameManager.Instance.GetPlayerData().Coin.ToString();
    }
}
