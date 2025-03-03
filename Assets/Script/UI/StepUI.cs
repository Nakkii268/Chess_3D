using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepUI : MonoBehaviour
{
    [SerializeField] private Slider stepUI;
    
    void Start()
    {
        ChessBoard.Instance.OnStepReduce += ChessBoard_OnStepReduce;
        stepUI.value = 1f;
    }

    private void ChessBoard_OnStepReduce(object sender, float e)
    {
       
        stepUI.value = e;
    }
}
 