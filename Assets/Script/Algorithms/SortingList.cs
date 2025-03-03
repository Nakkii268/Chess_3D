using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SortingList 
{
    public static void SortingScore(List<PlayerData> list)
    {
        List<PlayerData> result = new List<PlayerData>();
        for(int i = 0; i < list.Count-1; i++)
        {
            for(int j = 0; j < list.Count-i-1; j++)
            {
                if (list[j].Point  < list[j + 1].Point)
                {
                    PlayerData temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }
}
