using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownCombat.Characters
{
  public class HealthBar : MonoBehaviour
  {
    [SerializeField] private Image foregroundImage;

    private bool isInited = false;
    private int parentID;

    #region Init
    private void Start()
    {
      //Init();
    }

    public void Init(int _parentID)
    {
      Debug.Log("# HealthBar # Init");
      isInited = true;

      parentID = _parentID;

      AddListeners();
    }

    private void AddListeners()
    {
      Messenger.AddListener<float, int>(BroadcastName.Health.OnHealthPercentageChanged, OnHealthPercentageChanged);
    }

    private void RemoveListeners()
    {
      Messenger.RemoveListener<float, int>(BroadcastName.Health.OnHealthPercentageChanged, OnHealthPercentageChanged);
    }

    private void OnDestroy()
    {
      if (isInited)
      {
        RemoveListeners();
        isInited = false;
      }
    }
    #endregion

    private void OnHealthPercentageChanged(float healthPercentage, int instanceID)
    {
      if (parentID == instanceID)
      {
        foregroundImage.fillAmount = healthPercentage;
      }
    }
  }
}