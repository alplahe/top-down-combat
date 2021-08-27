using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownCombat.Characters
{
  public class CharacterHealth : MonoBehaviour, IDamageable
  {
    private int health;
    private int maxHealth;
    private int minHealth;
    private bool canTakeDamage;
    private bool canDie;
    private CharacterType characterType;
    private bool isInited = false;

    private Character character;

    #region Setters and Getters
    public int Health { set => health = value; }
    public int MaxHealth { set => maxHealth = value; }
    public int MinHealth { set => minHealth = value; }
    public bool CanTakeDamage { set => canTakeDamage = value; }
    public bool CanDie { set => canDie = value; }
    public CharacterType CharacterType { set => characterType = value; }

    private void SetEveryVariable()
    {
      if (character == null)
      {
        Debug.Log("character is NULL!");
        return;
      }

      
    }
    #endregion

    #region Init
    private void Awake()
    {
      Debug.Log("# CharacterHealth # Awake");
      //Init();
    }

    public void Init()
    {
      Debug.Log("# CharacterHealth # Init");

      isInited = true;
      character = GetComponent<Character>();

      AddListeners();
    }
    #endregion

    #region Listeners
    private void AddListeners()
    {
      Messenger.AddListener<int>(BroadcastName.Health.OnHealthPercentageAnimationEnds, OnHealthPercentageAnimationEnds);
    }

    private void RemoveListeners()
    {
      Messenger.RemoveListener<int>(BroadcastName.Health.OnHealthPercentageAnimationEnds, OnHealthPercentageAnimationEnds);
    }

    private void OnHealthPercentageAnimationEnds(int characterID)
    {
      Debug.Log("# CharacterHealth # OnHealthPercentageAnimationEnds");

      if (health <= minHealth)
      {
        health = minHealth;
        Die();
      }
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

    #region Damage
    public bool TakeDamage(int damage)
    {
      if (!canTakeDamage) return false;

      health -= damage;
      character.Health = health;

      float currentHealthPercentage = (float)health / (float)maxHealth;
      Messenger.Broadcast<float, int>(BroadcastName.Health.OnHealthPercentageChanged, 
                                      currentHealthPercentage, gameObject.GetInstanceID());
      
      return true;
    }

    public bool DoDamage(int damage)
    {
      throw new System.NotImplementedException();
    }

    public bool Die()
    {
      if (!canDie) return false;

      if (characterType == CharacterType.Player)
      {
        Messenger.Broadcast(BroadcastName.Health.OnPlayerDie);
      }
      else if (characterType == CharacterType.NPC)
      {
        Messenger.Broadcast<int>(BroadcastName.Health.OnNPCDie, character.gameObject.GetInstanceID());
        if (isInited)
        {
          RemoveListeners();
          isInited = false;
        }
      }

      return true;
    }
    #endregion
  }
}