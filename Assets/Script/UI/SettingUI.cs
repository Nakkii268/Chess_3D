using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIElement
{
   
    [SerializeField] private Button BackBtn;
    [SerializeField] private TMP_Dropdown solution;
    void Start()
    {
       
        BackBtn.onClick.AddListener(() =>
        {
            Hide();
        });
        solution.onValueChanged.AddListener((int value) =>
        {
            value=solution.value;
            switch (value)
            {
           
                case 0: Screen.SetResolution(1920, 1080, true);
                    Debug.Log("1920");
                    break;
                case 1: Screen.SetResolution(1440,900, false);
                    Debug.Log("1440");

                    break;
                case 2: Screen.SetResolution(1280, 720, false);
                    Debug.Log("1280");

                    break;
                default: Screen.SetResolution(1920, 1080, true);
                    break;
            }
            
        });

    }

   
}
