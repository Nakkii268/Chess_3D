using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : UIElement
{
    [SerializeField] private List<PlayerData> topten;
    [SerializeField] private Api api;
    [SerializeField] private List<LeaderboardSingle> single;
    [SerializeField] private LeaderboardSingle UserRank;
    [SerializeField] private Button hideBtn;
    
    void Start()
    {
        api = GameManager.Instance.GetApi();
        api.OnGetDataComplete += Api_OnGetDataComplete;
        hideBtn.onClick.AddListener(()=> { Hide(); });
        topten = api.GetUserList();

    }
        public void PreGetData()
    {
        api = GameManager.Instance.GetApi();
        topten = api.GetUserList();

    }


    private void Api_OnGetDataComplete(object sender, System.EventArgs e)
    {
        topten = api.GetUserList();

    }

    

    public override void Show()
    {
        if (topten != null)
        {
            gameObject.SetActive(true);
            topten = api.GetUserList();

            SortingList.SortingScore(topten);
            int userRank = SearchInList.UserSearch(topten, PlayerPrefs.GetString("UserName"));
            
            for (int i = 0; i < single.Count; i++)
            {
                single[i].PlayerName.text = topten[i].Name;
                single[i].PlayerPoint.text = topten[i].Point.ToString();

            }
            if (userRank == -1|| topten[userRank].Point==0)
            {
                UserRank.PlayerRank.text = "Unranked";

            }
            else
            {
                UserRank.PlayerRank.text = (userRank + 1).ToString();

            }

            UserRank.PlayerName.text = PlayerPrefs.GetString("UserName");
            UserRank.PlayerPoint.text = GameManager.Instance.GetPlayerData().Point.ToString();

        }

    }
    
}
