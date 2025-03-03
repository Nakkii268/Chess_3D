using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectSingleUI : MonoBehaviour
{
    [SerializeField] private MapSO mapSO;
    [SerializeField] private Image levelSprite;
    [SerializeField] private Transform mapLock;
    [SerializeField] public Button mapSelect;
   
    public MapSO GetMapSO() { return mapSO; }   
   
    public void MapSelectSetUp(MapSO map,int progress)
    {
        mapSO = map;
        levelSprite.sprite = map.lvSprite;
        if(map.MapID <= progress) { 
            mapLock.gameObject.SetActive(false);
        }
        else
        {
            mapLock.gameObject.SetActive(true);

        }
    }
}
