using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownCombat
{
  public class GameManager : MonoBehaviour
  {
    private bool isInited = false;

    #region Init
    private void Start()
    {
      Init();
    }

    private void Init()
    {
      isInited = true;

      // Initialize scripts here

      AddListeners();
    }
    #endregion

    #region Listeners
    private void AddListeners()
    {
      Messenger.AddListener(BroadcastName.Health.OnPlayerDie, OnPlayerDie);
      Messenger.AddListener<int>(BroadcastName.Health.OnNPCDie, OnNPCDie);
    }

    private void RemoveListeners()
    {
      Messenger.RemoveListener(BroadcastName.Health.OnPlayerDie, OnPlayerDie);
      Messenger.RemoveListener<int>(BroadcastName.Health.OnNPCDie, OnNPCDie);
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

    private void OnPlayerDie()
    {
      //Messenger.Broadcast(BroadcastName.Game.OnGameOver);
      Messenger.Broadcast(BroadcastName.Game.OnReturnToInitialScreen);
    }

    private void OnNPCDie(int npcInstanceID)
    {
      // TO DO...
      Messenger.Broadcast(BroadcastName.Spawner.OnSpawnerOneNPC);
      Messenger.Broadcast(BroadcastName.Spawner.OnSpawnerOneNPC);
    }
  }
}
