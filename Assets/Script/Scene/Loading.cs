using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public static Loading Instance;
    private static AsyncOperationHandle<SceneInstance> m_sceneLoadOpHandler;
    [SerializeField] private Slider loadingSlider;
    public event EventHandler OnSceneLoadDone;
   
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        StartCoroutine(LoadingLevel(GameManager.Instance.nextSceneLoad));

    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        GameManager.Instance.SceneSetup();
    }

    IEnumerator LoadingLevel(string level)
    {
        
        m_sceneLoadOpHandler = Addressables.LoadSceneAsync(level,activateOnLoad:true);
        m_sceneLoadOpHandler.Completed += OnSceneLoadComplete;

        while (!m_sceneLoadOpHandler.IsDone)
        {
            loadingSlider.value = m_sceneLoadOpHandler.PercentComplete;

            yield return null;
        }
           
        

    }

    private void OnSceneLoadComplete(AsyncOperationHandle<SceneInstance> obj)
    {
       
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;   
        
    }
}
