using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopSingleUI : MonoBehaviour
{
    [SerializeField] private Button buyBtn;
    [SerializeField] private Image itemImg;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private int itemID;


  

    public void SetItem(MaterialData materialData)
    {
        
        itemID = materialData.materialID;
        itemImg.sprite = materialData.materialPreview;
        itemPrice.text = materialData.price.ToString();
        buyBtn.onClick.AddListener(() =>
        {
            if(GameManager.Instance.GetPlayerData().Coin >= materialData.price)
            {
                GameManager.Instance.BuyItem(materialData.materialID, materialData.price);
                

            }
            else
            {
                Debug.Log("insufficent coin");
            }
        });

    }
}
