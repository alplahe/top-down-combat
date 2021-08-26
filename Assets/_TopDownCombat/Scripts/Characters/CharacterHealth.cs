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
      character = GetComponent<Character>();
    }
    #endregion

    #region Damage
    public bool TakeDamage(int damage)
    {
      if (!canTakeDamage) return false;

      health -= damage;
      character.Health = health;

      float currentHealthPercentage = (float)health / (float)maxHealth;
      Messenger.Broadcast<float, int>(BroadcastName.Health.OnHealthPercentageChanged, currentHealthPercentage, gameObject.GetInstanceID());

      if (health <= minHealth)
      {
        health = minHealth;
        Die();
      }

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
        Messenger.Broadcast(BroadcastName.Health.OnNPCDie);
      }

      return true;
    }
    #endregion
  }
}