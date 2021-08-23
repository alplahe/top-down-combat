using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadingSystem
{
  public class AsyncLoad : MonoBehaviour
  {
    FlowManager flowManager;
    SceneToLoad sceneToLoad;

    private void Start()
    {
      Init();
    }

    public void Init()
    {
      FlowManager flowManager = FlowManager.Instance.GetComponent<FlowManager>();
      if (flowManager != null)
      {
        Debug.Log("flowManager is NOT NULL!");
        sceneToLoad = flowManager.sceneToLoad;
      }
      else
      {
        Debug.Log("flowManager is NULL!");
      }

      LoadScene();
    }

    void LoadScene()
    {
      if (!CheckScene(sceneToLoad.ToString()))
      {
        Debug.Log("The scene named \"" + sceneToLoad.ToString() + "\" does NOT EXIST!" +
          " Check the SceneToLoad enum list.");
        return;
      }

      GC.Collect();
      SceneManager.LoadSceneAsync(sceneToLoad.ToString());
    }

    bool CheckScene(string sceneName)
    {
      for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
      {
        if (sceneName == GetSceneNameFromBuildIndex(i))
          return true;
      }
      return false;
    }

    string GetSceneNameFromBuildIndex(int buildIndex)
    {
      string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
      int slash = path.LastIndexOf('/');
      string name = path.Substring(slash + 1);
      int dot = name.LastIndexOf('.');
      return name.Substring(0, dot);
    }
  }
}