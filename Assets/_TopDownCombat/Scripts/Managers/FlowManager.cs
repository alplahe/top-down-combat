using System;
using System.Collections;
using System.Collections.Generic;
using TopDownCombat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadingSystem
{
  public enum SceneToLoad
  {
    InitialScreen,
    Menu,
    CombatScene
  }

  public class FlowManager : MonoBehaviour
  {
    public SceneToLoad sceneToLoad;

    #region Singleton
    private static FlowManager instance;

    public static FlowManager Instance 
    { 
      get 
      { 
        return instance; 
      } 
    }

    public void Init()
    {
      InitSingleton();
      AddListeners();
      //GotoCombatScene();
    }

    void InitSingleton()
    {
      if (instance != null && instance != this)
      {
        Debug.Log("Destroy duplicated FlowManager instance");
        Destroy(this.gameObject);
      }
      else
      {
        Debug.Log("Creating FlowManager instance");
        instance = this;
        DontDestroyOnLoad(instance);
      }
    }
    #endregion

    #region Listeners
    private void AddListeners()
    {
      Messenger.AddListener(BroadcastName.Game.OnPlayGame, OnPlayGame);
      Messenger.AddListener(BroadcastName.Game.OnGameApplicationQuit, OnGameApplicationQuit);
      Messenger.AddListener(BroadcastName.Game.OnReturnToInitialScreen, OnReturnToInitialScreen);
    }

    private void RemoveListeners()
    {
      Messenger.RemoveListener(BroadcastName.Game.OnPlayGame, OnPlayGame);
      Messenger.RemoveListener(BroadcastName.Game.OnGameApplicationQuit, OnGameApplicationQuit);
      Messenger.RemoveListener(BroadcastName.Game.OnReturnToInitialScreen, OnReturnToInitialScreen);
    }

    private void OnPlayGame()
    {
      Debug.Log("FlowManager. OnPlayGame");
      GotoCombatScene();
    }

    private void OnGameApplicationQuit()
    {
      Debug.Log("FlowManager. OnGameApplicationQuit");
      Application.Quit();
    }

    private void OnReturnToInitialScreen()
    {
      Debug.Log("GameManager. OnReturnToInitialScreen");
      GotoInit();
    }

    private void OnDestroy()
    {
      RemoveListeners();
    }
    #endregion

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
        Messenger.Broadcast(BroadcastName.Game.OnReturnToInitialScreen);
    }

    void GotoInit()
    {
      sceneToLoad = SceneToLoad.InitialScreen;
      LoadLoadingScreenScene();
    }

    void GotoMenu()
    {
      sceneToLoad = SceneToLoad.Menu;
      LoadLoadingScreenScene();
    }

    void GotoCombatScene()
    {
      sceneToLoad = SceneToLoad.CombatScene;
      LoadLoadingScreenScene();
    }

    void LoadLoadingScreenScene()
    {
      Debug.Log("LoadLoadingScreenScene");
      //RemoveListeners();
      SceneManager.LoadScene("LoadingScreen");
    }
  }
}