using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectUI : UIElement

{
    [SerializeField] private GameObject levelSelectBtn;
    [SerializeField] private Transform levelList;
    public event EventHandler<MapSO> OnMapSelect;
    [SerializeField] private Button backBtn;
    [SerializeField] private List<MapSO> Maps;
    [SerializeField] private int playerProgress=1;
  
    private void Start()
    {
                  
        Maps = GameManager.Instance.mapList;
        GameManager.Instance.OnDataChange += GameManager_OnDataChange;
        backBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        MapSelectSetup();


    }

    private void GameManager_OnDataChange(object sender, EventArgs e)
    {
        MapSelectSetup();

    }

    private void MapSelectSetup()
    {
        for(int i=0; i < levelList.childCount; i++)
        {
            Destroy(levelList.GetChild(i).gameObject);  
        }
        levelList.DetachChildren();
        playerProgress = GameManager.Instance.GetPlayerData().Progress;


        for (int i = 0; i <= Maps.Count - 1; i++)
        {
            GameObject levelSelector = Instantiate(levelSelectBtn, levelList);
            MapSelectSingleUI mapSelectSingleUI = levelSelector.GetComponent<MapSelectSingleUI>();
            mapSelectSingleUI.MapSelectSetUp(Maps[i], playerProgress);
            if (Maps[i].MapID <= playerProgress)
            {
                mapSelectSingleUI.mapSelect.onClick.AddListener(() =>
                {

                    OnMapSelect?.Invoke(this, mapSelectSingleUI.GetMapSO());


                });
            }
        }
    }



}
