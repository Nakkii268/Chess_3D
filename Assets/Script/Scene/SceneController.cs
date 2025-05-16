using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public enum Scene
    {
        MenuScene,
        GameScene,
    }
    public static event SceneLoad OnSceneLoad;
    public static Scene scene;

    public static void LoadScene(Scene scene)
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        string targetSene = scene.ToString();
        SceneManager.LoadScene(targetSene);


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

        return SceneManager.GetActiveScene().buildIndex;
    }

}
public delegate void SceneLoad();