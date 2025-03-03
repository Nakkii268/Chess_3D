using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIElement
{
    [SerializeField] private MaterialDataSO materialDataSO;
    [SerializeField] private Transform Holder;
    [SerializeField] private Transform InventoryHolder;
    [SerializeField] private Button backGroundShopBtn;
    [SerializeField] private Button inventoryBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private List<ShopSingleUI> shopSingleUI;
    [SerializeField] private List<InventorySingleUI> inventorySingleUI;
    [SerializeField] private Transform ShopSlot;
    [SerializeField] private Transform InventorySlot;
    [SerializeField] private bool isShopOpen;
    [SerializeField] private TextMeshProUGUI userCoinText;

    private void Start()
    {
        GameManager.Instance.OnDataChange += GameManager_OnDataChange;
        materialDataSO = GameManager.Instance.GetMaterialData();

        InventoryHolder.gameObject.SetActive(false);
        isShopOpen = true;

        backGroundShopBtn.onClick.AddListener(() =>
        {
            if (isShopOpen)
            {
                return;
            }
            else
            {
                isShopOpen = true;
                Holder.gameObject.SetActive(true);
                InventoryHolder.gameObject.SetActive(false);

            }
        });
        inventoryBtn.onClick.AddListener(() =>
        {
            if (isShopOpen)
            {
                isShopOpen=false;
                Holder.gameObject.SetActive(false);
                InventoryHolder.gameObject.SetActive(true);
            }
            else
            {
                return;
            }
        });
        closeBtn.onClick.AddListener(() =>
        {
            Hide();
        });
        userCoinText.text = GameManager.Instance.GetPlayerData().Coin.ToString();
        SetupShopSlot();
        SetupInventorySlot();
    }

    private void GameManager_OnDataChange(object sender, System.EventArgs e)
    {
        userCoinText.text = GameManager.Instance.GetPlayerData().Coin.ToString();
        SetupShopSlot();
        SetupInventorySlot();
    }

    private void SpawnShopSlot()
    {
        for( int i = 0; i <= materialDataSO.skyboxsMaterial.Length-1; i++)
        {
           Instantiate(ShopSlot, Holder);
            
        }
    }
    private void SpawnInventorySlot()
    {
        
        for (int i = 0; i <= GameManager.Instance.GetPlayerData().ownedBG.Count-1; i++)
        {
        
            Instantiate(InventorySlot, InventoryHolder);
            
        }
    }
    public void SetupInventorySlot()
    {
        for (int i = 0; i <= InventoryHolder.childCount - 1; i++)
        {
            Destroy(InventoryHolder.GetChild(i).gameObject);
        }
        InventoryHolder.DetachChildren();
        inventorySingleUI.Clear();
        SpawnInventorySlot();

        inventorySingleUI.AddRange(InventoryHolder.GetComponentsInChildren<InventorySingleUI>());

        List<int> bg = GameManager.Instance.GetPlayerData().ownedBG;
        
        

        for (int i = 0; i <= inventorySingleUI.Count-1; i++)
        {
            

            MaterialData md = SearchInList.FindMaterialDataByID(materialDataSO.skyboxsMaterial, bg[i]);
            inventorySingleUI[i].SetItem(md);
            inventorySingleUI[i].gameObject.SetActive(true);
        }
    }
  
    public void SetupShopSlot()
    {
        for(int i =0; i <= Holder.childCount - 1; i++)
        {
            Destroy(Holder.GetChild(i).gameObject);
           
        }
        Holder.DetachChildren();
        shopSingleUI.Clear();
        SpawnShopSlot();
       

        shopSingleUI.AddRange(Holder.GetComponentsInChildren<ShopSingleUI>());

        for (int i = 0; i <= shopSingleUI.Count -1; i++)
        {
            if (!GameManager.Instance.GetPlayerData().ownedBG.Contains(materialDataSO.skyboxsMaterial[i].materialID))
            {

            shopSingleUI[i].SetItem(materialDataSO.skyboxsMaterial[i]);
            shopSingleUI[i].gameObject.SetActive(true);
            }
            else
            {
                shopSingleUI[i].gameObject.SetActive(false);

            }
        }
    }
    


}
