using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;
using UnityEditor;
using System.Linq;

public class Api : MonoBehaviour
{
    private string URL = "https://fir-leaderboard-6aa82-default-rtdb.firebaseio.com/";
    
    
    [SerializeField]private List<PlayerData> userList;
    public event EventHandler OnGetDataComplete;
    
    public List<PlayerData> GetUserList()
    {
        return userList;
    }

    private void Start()
    {
        userList = new List<PlayerData>();

        GameManager.Instance.OnDataUpdate += GameManager_OnDataUpdate;
        StartCoroutine(GetData());
        DontDestroyOnLoad(gameObject);


    }

    private void GameManager_OnDataUpdate(object sender, EventArgs e)
    {
        userList.Clear();
        StartCoroutine(GetData());
    }

    public void Runrequest()
    {
        StartCoroutine(GetData());
    }
    public void UpdateToDB(PlayerData PlayerData)
    {
        StartCoroutine(PostData(PlayerData));
    
    }
   
    public void GetListto()
    {

    }
    private List<int> StringToIntList(string str)
    {
        List<int> list = str.Split(",").Select(int.Parse).ToList();
        return list;
    }

    IEnumerator GetData()
    {
        using( UnityWebRequest request  = UnityWebRequest.Get(URL+ "User.json"))
        {
            
            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.ConnectionError ) { 
                Debug.LogError(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode jsonNode = SimpleJSON.JSON.Parse(json);
                for (int i = 0; i < jsonNode.Count; i++)
                {
                       TempPlayerData temp =(JsonUtility.FromJson<TempPlayerData>(jsonNode[i].ToString()));
                    
                     userList.Add( new PlayerData() { Name = temp.Name,Coin = temp.Coin,Point=temp.Point,Progress = temp.Progress, ownedBG = StringToIntList(temp.ownedBG) });
                   
                }
                OnGetDataComplete?.Invoke(this, EventArgs.Empty);





            }
        }
    }
    IEnumerator PostData(PlayerData data)
    {
        string userName = PlayerPrefs.GetString("UserName");
        string strlist = string.Join(",", data.ownedBG);
        string text = $"{{ \"Name\":\"{data.Name}\" , \"Point\":{data.Point},\"Progress\":{data.Progress},\"Coin\":{data.Coin},\"ownedBG\":\"{strlist}\"}}"; //,\"ownedBG\":[{data.ownedBG}]
        using (UnityWebRequest request = UnityWebRequest.Put("https://fir-leaderboard-6aa82-default-rtdb.firebaseio.com/User/"+userName+".json", text))
        {
            

            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.ConnectionError ) {  Debug.LogError(request.error); }
            else
            {
                 Debug.Log(request.downloadHandler.text);
                userList.Clear();

                StartCoroutine(GetData());
            }
        }
    }
    
    


}
[Serializable]
public class PlayerData
{
    public string Name;
    public float Point;
    public int Coin;
    public int Progress=1;
    public List<int> ownedBG=new List<int> { 1,2};
}
public class TempPlayerData
{
    public string Name;
    public float Point;
    public int Coin;
    public int Progress = 1;
    public string ownedBG;
}
