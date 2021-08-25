using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownCombat.Characters
{
  public enum CharacterType
  {
    Player,
    NPC
  }
  public class Character : MonoBehaviour
  {
    [Header("Components")]
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CharacterHealth characterHealth;

    [Header("Character")]
    [SerializeField] private CharacterType characterType;

    [Header("Materials")]
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material NPCMaterial;

    [Header("Speed")]
    [SerializeField, Range(0, 10)] private float speed;
    [SerializeField] private float angularSpeed;

    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int minHealth;
    [SerializeField] private bool canTakeDamage;
    [SerializeField] private bool canDie;

    #region Getters and setters
    public int Health { get => health; set => health = value; }
    #endregion

    private void Awake()
    {
      characterMovement = GetComponent<CharacterMovement>();
      if (characterMovement == null) Debug.Log("characterMovement is NULL!");

      characterHealth = GetComponent<CharacterHealth>();
      if (characterHealth == null) Debug.Log("characterHealth is NULL!");
    }

    private void OnValidate()
    {
      SetCharacterMovementVariables();
      SetCharacterHealthVariables();
    }

    private void SetCharacterMovementVariables()
    {
      characterMovement.SetCharacterType(characterType);
      characterMovement.SetPlayerMaterial(playerMaterial);
      characterMovement.SetNPCMaterial(NPCMaterial);
      characterMovement.SetSpeed(speed);
      characterMovement.SetAngularSpeed(angularSpeed);

      characterMovement.SetAgentSpeeds();
      characterMovement.SetCharactersBehaviour();
    }

    private void SetCharacterHealthVariables()
    {
      characterHealth.Health = health;
      characterHealth.MaxHealth = maxHealth;
      characterHealth.MinHealth = minHealth;
      characterHealth.CanTakeDamage = canTakeDamage;
      characterHealth.CanDie = canDie;
      characterHealth.CharacterType = characterType;
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
      if (value.started)
      {
        Debug.Log("# Character # OnAttack");
        if (characterType == CharacterType.NPC)
        {
          Debug.Log("# Character # NPC take damage");
          characterHealth.TakeDamage(maxHealth/2);
        }
      }
    }
  }
}