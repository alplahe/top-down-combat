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

  public enum AttackType
  {
    Short,
    Long
  }

  public class Character : MonoBehaviour
  {
    [Header("Components")]
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CharacterHealth characterHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private CharacterAttack characterAttack;

    [Header("Character")]
    [SerializeField] private CharacterType characterType;
    [SerializeField] private AttackType attackType;
    [SerializeField] private bool playerHasBothAttackTypes = true;

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

    [Header("Short attack")]
    [SerializeField] private float shortAttackRange;
    [SerializeField] private int shortAttackDamage;
    [SerializeField] private float shortAttackCooldown;
    [SerializeField] private float shortAttackStoppingDistance_NPC;
    [SerializeField] private float shortAttackAnimationDuration; // Must always be a lower value than shortAttackCooldown

    [Header("Long attack")]
    [SerializeField] private int longAttackDamage;
    [SerializeField] private float longAttackShotFrecuency;
    [SerializeField] private float longAttackProjectileSpeed;
    [SerializeField] private float longAttackProjectileRange;
    [SerializeField] private float longAttackShotRange_NPC;
    [SerializeField] private float longAttackShotTimePreparation_NPC;
    [SerializeField] private float longAttackStoppingDistance_NPC;

    #region Getters and setters
    public int Health { get => health; set => health = value; }
    #endregion

    private void Awake()
    {
      //PreInit();
    }

    public void PreInit()
    {
      characterMovement = GetComponent<CharacterMovement>();
      if (characterMovement == null) Debug.Log("characterMovement is NULL!");

      characterHealth = GetComponent<CharacterHealth>();
      if (characterHealth == null) Debug.Log("characterHealth is NULL!");

      //healthBar = GetComponent<HealthBar>(); // It is attached in the inspector
      if (healthBar == null) Debug.Log("healthBar is NULL!");

      characterAttack = GetComponent<CharacterAttack>();
      if (characterAttack == null) Debug.Log("characterAttack is NULL!");
    }

    private void OnValidate()
    {
      SetCharacterMovementVariables();
      SetCharacterHealthVariables();
      SetCharacterAttackVariables();
    }

    private void Start()
    {
      //Init();
    }

    public void Init()
    {
      characterMovement.Init();
      characterHealth.Init();
      healthBar.Init(gameObject.GetInstanceID());
      characterAttack.Init();

      SetCharacterMovementVariables();
      SetCharacterHealthVariables();
      SetCharacterAttackVariables();
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

    private void SetCharacterAttackVariables()
    {
      characterAttack.AttackType = attackType;
      characterAttack.CharacterType = characterType;
      characterAttack.PlayerHasBothAttackTypes = playerHasBothAttackTypes;

      characterAttack.ShortAttackRange = shortAttackRange;
      characterAttack.ShortAttackDamage = shortAttackDamage;
      characterAttack.ShortAttackCooldown = shortAttackCooldown;
      characterAttack.ShortAttackStoppingDistance_NPC = shortAttackStoppingDistance_NPC;
      characterAttack.ShortAttackAnimationDuration = shortAttackAnimationDuration;

      characterAttack.LongAttackDamage = longAttackDamage;
      characterAttack.LongAttackShotFrecuency = longAttackShotFrecuency;
      characterAttack.LongAttackProjectileSpeed = longAttackProjectileSpeed;
      characterAttack.LongAttackProjectileRange = longAttackProjectileRange;
      characterAttack.LongAttackShotRange_NPC = longAttackShotRange_NPC;
      characterAttack.LongAttackShotTimePreparation_NPC = longAttackShotTimePreparation_NPC;
      characterAttack.LongAttackStoppingDistance_NPC = longAttackStoppingDistance_NPC;

      characterAttack.SetAttackTypeBehaviour();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
      if (value.started)
      {
        Debug.Log("# Character # OnAttack");
        if (characterType == CharacterType.Player) // DELETE THIS: This is just a test
        {
          //Debug.Log("# Character # Player take damage");
          //characterHealth.TakeDamage(maxHealth/2);
        }
      }
    }
  }
}