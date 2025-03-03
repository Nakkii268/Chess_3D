using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;


public class GameManager : PersistentSingleton<GameManager> 
{
    public MapSO targetmapSO;
    public List<MapSO> mapList;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameSceneLoad;
    
    [SerializeField] private MaterialDataSO skyboxs;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Api _api;
    public string nextSceneLoad;
    private AsyncOperationHandle<GameObject> m_chessBoardLoadOpHandler;
    public PlayerData GetPlayerData()
    {
        return playerData;
    }
    public Api GetApi() { return _api; }
    public MaterialDataSO GetMaterialData()
    {
        return skyboxs;
    }
    public event EventHandler OnDataUpdate;//to api
    public event EventHandler OnDataChange;//to other 


    


    private void OnEnable()
    {

        MenuUIManager.Instance.GetNameSetUI().OnNameSet += NameSetUI_OnNameSet;
        MenuUIManager.Instance.GetMapSelectUI().OnMapSelect += MapSelectUI_OnMapSelect;

        SceneController.OnSceneLoad += SceneController_OnSceneLoad;
        _api.OnGetDataComplete += _api_OnGetDataComplete;




    }

    private void Loading_OnSceneLoadDone(object sender, EventArgs e)
    {
      
        Debug.Log(SceneController.GetCurrentScene());
    }
    public void SceneSetup()
    {
        SceneController.UnsubscriptionEvent();
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {

            MenuUIManager.Instance.GetMapSelectUI().OnMapSelect += MapSelectUI_OnMapSelect;
            SceneController.UnloadGameScene();
        }
        else if(SceneManager.GetActiveScene().name == "GameScene")
        {
            Debug.Log("ame load ok");
            SetRandomSkyBox();
            GameObject go = GameObject.FindGameObjectWithTag("ChessBoard");

            
            m_chessBoardLoadOpHandler = targetmapSO.MapAsset.LoadAssetAsync<GameObject>();
            m_chessBoardLoadOpHandler.Completed += M_chessBoardLoadOpHandler_Completed;

            ChessBoard.Instance.IsWinningGame += ChessBoard_IsWinningGame;

            

            InGameUIManager.Instance.OnPauseBtnCick += InGameUIManager_OnPauseBtnCick;
            OnGameSceneLoad?.Invoke(this, EventArgs.Empty);
        }
    }

    private void M_chessBoardLoadOpHandler_Completed(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log(obj.Result);
            
              Instantiate(obj.Result);
           
            
        }
    }

    private void NameSetUI_OnNameSet(object sender, EventArgs e)
    {
        playerData.Name = PlayerPrefs.GetString("UserName");
        playerData.ownedBG = new List<int> { 0,1 };
        PlayerPrefs.SetInt("InUse", 0);
        _api.UpdateToDB(playerData);   
    }

    private void _api_OnGetDataComplete(object sender, EventArgs e)
    {
        if (PlayerPrefs.HasKey("UserName"))
          playerData = SearchInList.GetUserInfomation(_api.GetUserList(), PlayerPrefs.GetString("UserName"));
        OnDataChange?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
       
        MenuUIManager.Instance.GetMapSelectUI().OnMapSelect -= MapSelectUI_OnMapSelect;

        SceneController.OnSceneLoad -= SceneController_OnSceneLoad;
        _api.OnGetDataComplete -= _api_OnGetDataComplete;
        if (Loading.Instance != null)
        {
            Loading.Instance.OnSceneLoadDone -= Loading_OnSceneLoadDone;
        }

    }

    private void InGameUIManager_OnPauseBtnCick(object sender, EventArgs e)
    {
       
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }

    private void ChessBoard_IsWinningGame(object sender, EndGameData e)
    {
        if (e.winteam == 0)
        {
            InGameUIManager.Instance.gameEndUI.SetScore(e.moveleft * 100);
            InGameUIManager.Instance.gameEndUI.ShowWin();
          
                
                if(playerData.Point < e.moveleft * 100)
                {
                    playerData.Point = e.moveleft * 100;
                }
                
            
            playerData.Progress = e.MapID+1 ;
            playerData.Coin += 1;
            
            _api.UpdateToDB(playerData);
            OnDataUpdate?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            InGameUIManager.Instance.gameEndUI.ShowLose();
        }
        
    }

    private void SceneController_OnSceneLoad()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == SceneController.Scene.LoadingScene.ToString())
        {
        Loading.Instance.OnSceneLoadDone += Loading_OnSceneLoadDone;
            Debug.Log("assigned");

        }
        else
        {
            Loading.Instance.OnSceneLoadDone -= Loading_OnSceneLoadDone;
            Debug.Log("removed");

        }


    }

    private void MapSelectUI_OnMapSelect(object sender, MapSO e)
    {
        targetmapSO = e;
        SceneController.LoadScene(SceneController.Scene.GameScene);
        
    }
    public void NextLevel()
    {
       int prevIndex = GetCurrentMapIndex();
        targetmapSO = mapList[prevIndex + 1];

    }
    public int GetCurrentMapIndex()
    {
        return mapList.IndexOf(targetmapSO);
    }
    public int GetMapListCount()
    {
        return mapList.Count;
    }
    private void SetRandomSkyBox()
    {
        int num = PlayerPrefs.GetInt("InUse");
        RenderSettings.skybox = skyboxs.skyboxsMaterial[num].material;
    }
    public void BuyItem(int itemid,int price)
    {
        playerData.ownedBG.Add(itemid);
        playerData.Coin -= price;
        _api.UpdateToDB(playerData);

    }
    public void TestFunc()
    {
        m_chessBoardLoadOpHandler = targetmapSO.MapAsset.LoadAssetAsync<GameObject>();
        m_chessBoardLoadOpHandler.Completed += M_chessBoardLoadOpHandler_Completed;
    }

}
