using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using LoadingSystem;
using TopDownCombat.UI;

// How to search where a broadcast is invoked:
// 1. Ctrl + Shift + F: search in all the project
// 2. Copy-paste this line

// Messenger\.Broadcast(<[^<>]+>)?\(

// 3. Continue that line with the Broadcast you want to search
// Example: Messenger\.Broadcast(<[^<>]+>)?\(BroadcastName.Game.OnGameOver

// 4. CHECK THE "Use regular expressions" ("Usar expresiones regulares") CHECKBOX!!!

// This Regular expression allows you to find usages of the broadcast with different signature
// In other words, it find the broadcast usages no matter the <,,> part, it will return the ones 
// doesnt ask for type, the ones that use 1 type, 2 types, ...

// Note: the dots used in "BroadcastName.Game.OnGameOver" should be written as special characters:
// "BroadcastName\.Game\.OnGameOver" for a correct search, but it also works the other way.

namespace TopDownCombat
{
  [System.Serializable]
  public class BroadcastNameType
  {
    public string name;

    public string prefix;
    public string suffix;

    public override string ToString()
    {
      return name;
    }
  }


  public class BroadcastName
  {
    public class Game
    {
      public static string prefix = "Game_";

      public static string OnGameOver = prefix + "OnGameOver";
      public static string OnReturnToInitialScreen = prefix + "OnReturnToInitialScreen";
      public static string OnGameApplicationQuit = prefix + "OnGameApplicationQuit";
      public static string OnPlayGame = prefix + "OnPlayGame";
    }

    public class Health
    {
      public static string prefix = "Health_";

      public static string OnPlayerDie = prefix + "OnPlayerDie";
      public static string OnNPCDie = prefix + "OnNPCDie";

      public static string OnHealthPercentageChanged = prefix + "OnHealthPercentageChanged";

      public static string OnPlayerTakeDamage = prefix + "OnPlayerTakeDamage";
      public static string OnNPCTakeDamage = prefix + "OnNPCTakeDamage";
    }

    public class Wave
    {
      public static string prefix = "Wave_";

      public static string OnAllNPCsOfWaveDie = prefix + "OnAllNPCsOfWaveDie";
      public static string OnAllWavesEnd = prefix + "OnAllWavesEnd";
    }
  }
}