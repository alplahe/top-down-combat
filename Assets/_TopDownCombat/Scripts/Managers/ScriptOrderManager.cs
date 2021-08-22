using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadingSystem;

namespace TopDownCombat
{
  public enum SceneName
  {
    InitialScreen,
    LoadingScreen,
    CombatScene
  }

  public class ScriptOrderManager : MonoBehaviour
  {
    [SerializeField] private HierarchyManager hierarchyManager;
    [SerializeField] private FlowManager flowManager;
    [SerializeField] private SceneName currentSceneName;

    private void Start()
    {
      if(hierarchyManager == null)
      {
        Debug.Log("HierarchyManager is NULL!");
      }
      else
      {
        hierarchyManager.Init();
      }

      if(flowManager == null)
      {
        Debug.Log("FlowManager is NULL!");
      }
      else
      {
        flowManager.Init();
      }
    }
  }
}