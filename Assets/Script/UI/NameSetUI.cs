using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameSetUI : UIElement
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Transform invalidNameAlert;
    [SerializeField] private Api api;
    public event EventHandler OnNameSet; 

    private void Start()
    {
        invalidNameAlert.gameObject.SetActive(false);
        if(PlayerPrefs.HasKey("UserName"))
        {
            Hide();
        }
        else
        {
            Show();
        }
        

        api = GameManager.Instance.GetApi();
        confirmBtn.onClick.AddListener(() =>
        {
            Debug.Log(nameInput.text);
            if (!SearchInList.NameSearch(api.GetUserList(), nameInput.text) &&  nameInput.text !="") {
                PlayerPrefs.SetString("UserName",nameInput.text);
                Hide();
                Debug.Log(PlayerPrefs.GetString("UserName"));
                OnNameSet?.Invoke(this, EventArgs.Empty);

            }
            else
            {
                invalidNameAlert.gameObject.SetActive(true);
                Debug.Log(PlayerPrefs.GetString("UserName"));
            }
        });
    }



   
}
