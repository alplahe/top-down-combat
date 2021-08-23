using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadingSystem;
using UnityEditor;

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
      if (hierarchyManager == null)
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
        flowManager = GameObject.Find("FlowManager").GetComponent<FlowManager>();
        flowManager.Init();
        flowManager = FlowManager.Instance.GetComponent<FlowManager>();
        Debug.Log("flowManager: " + flowManager);
        //EditorGUIUtility.PingObject(flowManager.gameObject);

        if (flowManager == null)
        {
          Debug.Log("FlowManager is REALLY NULL!");
          flowManager = GameObject.Find("FlowManager").GetComponent<FlowManager>();
        }
        else
        {
          flowManager.Init();
        }
      }
      else
      {
        flowManager.Init();
      }
    }
  }
}