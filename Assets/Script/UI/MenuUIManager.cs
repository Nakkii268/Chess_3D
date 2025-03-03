using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : Singleton<MenuUIManager>
{
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private LeaderboardUI leaderboardUI;
    [SerializeField] private MapSelectUI mapSelectUI;
    [SerializeField] private NameSetUI nameSetUI;
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private SettingUI settingUI;
    [SerializeField] public Transform LoadingUI;

    public MenuUI GetMenuUI() { return menuUI; }
    public LeaderboardUI GetLeaderboardUI() {  return leaderboardUI; }
    public MapSelectUI GetMapSelectUI() {  return mapSelectUI; }
    public NameSetUI GetNameSetUI() {  return nameSetUI; }
    public ShopUI GetShopUI() { return shopUI; }
    public SettingUI GetSettingUI() { return settingUI; }
    private void Start()
    {
        shopUI.Hide();
        mapSelectUI.Hide();
        leaderboardUI.PreGetData();
        leaderboardUI.Hide();
        settingUI.Hide();
        Debug.Log("manager");
    }

    
  
}
