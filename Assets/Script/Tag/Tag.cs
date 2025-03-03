using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DirectionalSlope
{
    top=0, 
    bottom=1,
    left=2, 
    right=3,
    noSlope=4
}
public class Tag : MonoBehaviour
{
    public DirectionalSlope direction;
}
