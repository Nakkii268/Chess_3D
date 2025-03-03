using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySingleUI : MonoBehaviour
{
    [SerializeField] private Button useBtn;
    [SerializeField] private Image itemImg;
    [SerializeField] private Transform overlayer;
    
    [SerializeField] private int itemID;
    [SerializeField] private TextMeshProUGUI useText;




    public void SetItem(MaterialData materialData)
    {

        itemID = materialData.materialID;
        itemImg.sprite = materialData.materialPreview;
        if (itemID != PlayerPrefs.GetInt("InUse"))
        {
            Debug.Log(PlayerPrefs.GetInt("InUse")+"pref");
            overlayer.gameObject.SetActive(false);
            useText.text = "Use";
            useBtn.onClick.AddListener(() =>
            {

                PlayerPrefs.SetInt("InUse", itemID);
               

                MenuUIManager.Instance.GetShopUI().SetupInventorySlot();


            });
        }
        else
        {
            overlayer.gameObject.SetActive(true);
            useText.text = "In use";

        }
    }
}
