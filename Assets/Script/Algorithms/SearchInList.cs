using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SearchInList 
{
    public static bool NameSearch(List<PlayerData> list, string target)
    {
        for (int i = 0; i < list.Count-1; i++)
        {
            if (list[i].Name == target)
            {
                return true;
            }
        }
        return false;
    }
    public static int UserSearch(List<PlayerData> list, string target)
    {
        for (int i = 0; i <= list.Count ; i++)
        {
            if (list[i].Name == target)
            {
                return i;
            }
        }
        return -1;
    }
    public static PlayerData GetUserInfomation(List<PlayerData> list, string target)
    {
        for (int i = 0; i <= list.Count ; i++)
        {
            if (list[i].Name == target)
            {
               
                return list[i];
            }
        }
        return null;
    }
    public static MaterialData FindMaterialDataByID(MaterialData[] list, int id) {
        for (int i = 0; i <= list.Length; i++)
        {
            if (list[i].materialID == id)
            {

                return list[i];
            }
        }
        return null;
    }
}
