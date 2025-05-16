using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnNotify : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CloseUI());  
    }
    private IEnumerator CloseUI()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
