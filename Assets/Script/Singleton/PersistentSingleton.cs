using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T :Component
{
    private static T s_Instance;
    public static T Instance
    {
        get { 
            if(s_Instance == null)
            {
                s_Instance = GameObject.FindFirstObjectByType<T>(); 

                if(s_Instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    s_Instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            
            return s_Instance; 
        }
    }

    protected virtual void Awake()
    {
        if(s_Instance == null)
        {
            s_Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
         if(s_Instance != this)
        {
            Debug.Log("dup");
            Destroy(gameObject);
        }
    }

}
