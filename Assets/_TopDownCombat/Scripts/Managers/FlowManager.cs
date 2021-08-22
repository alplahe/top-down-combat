using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadingSystem
{
  public enum SceneToLoad
  {
    Init,
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
      GotoCombatScene();
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

    void GotoInit()
    {
      sceneToLoad = SceneToLoad.Init;
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
      SceneManager.LoadScene("LoadingScreen");
    }
  }
}