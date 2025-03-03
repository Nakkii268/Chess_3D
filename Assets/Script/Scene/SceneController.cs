
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public static class SceneController 
{
    public enum Scene
    {
        MenuScene,
        GameScene,
        LoadingScene
    }
   public static event SceneLoad OnSceneLoad;
    public static Scene scene;
    private static AsyncOperationHandle<SceneInstance> SceneLoadOpHandler;
    public static void LoadScene(Scene scene)
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        
        GameManager.Instance.nextSceneLoad = scene.ToString();
        
        SceneLoadOpHandler = Addressables.LoadSceneAsync("LoadingScene",activateOnLoad:true);


        SubscriptionEvent();
        
    }
    public static void ReloadScene()
    {

        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SubscriptionEvent();

    }

    private static void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        OnSceneLoad?.Invoke();
        
    }
    public static void UnsubscriptionEvent()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;


    }
    public static void SubscriptionEvent()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

    }
    public static void UnloadGameScene()
    {
        SceneManager.UnloadSceneAsync(GetCurrentScene());
    }
    public static int GetCurrentScene()
    {
        
      return  SceneManager.GetActiveScene().buildIndex;
    }
   
}
public delegate void SceneLoad();