using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T: Component
{
    private static T s_Instance;
    private bool m_DelayDuplicateRemoval;

    public static T Instance
    {
        get
        {
            if(s_Instance == null)
            {
                s_Instance = (T)FindFirstObjectByType(typeof(T));
                if(s_Instance == null)
                {
                    SetupInstance();

                }
                else
                {
                    string typeName = typeof(T).Name;
                }
            }

            return s_Instance;
        }
    }
    public virtual void Awake()
    {
        if (!m_DelayDuplicateRemoval) RemoveDuplicate();
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }
    private void OnDisable()
    {
        if(s_Instance == this as T)
        {
            SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
        }
    }

    

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if(s_Instance != null)
        {
            Destroy(s_Instance.gameObject);
        }
        s_Instance = null;
    }

    private  void RemoveDuplicate()
    {
        if(s_Instance == null)
        {
            s_Instance = this as T;

        }else if(s_Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private static void SetupInstance()
    {
        s_Instance =(T)FindFirstObjectByType(typeof(T));    
        if(s_Instance == null)
        {
            GameObject singletonObject = new GameObject();
            singletonObject.name  = typeof(T).Name;
            s_Instance = singletonObject.AddComponent<T>();
            DontDestroyOnLoad(singletonObject);
            Debug.Log("spawn by null" +  singletonObject.name);
        }
    }
}
