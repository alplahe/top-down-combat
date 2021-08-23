using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TopDownCombat;
using LoadingSystem;

namespace TopDownCombat.UI
{
  public class InitialMenuUI : MonoBehaviour
  {
    [SerializeField] private Button playGameButton;
    [SerializeField] private Button quitGameButton;

    public void PlayGame()
    {
      Debug.Log("Play game");
      Messenger.Broadcast(BroadcastName.Game.OnPlayGame);
    }

    public void QuitGame()
    {
      Debug.Log("Quit game");
      Messenger.Broadcast(BroadcastName.Game.OnGameApplicationQuit);
    }
  }
}