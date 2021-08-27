using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace TopDownCombat.Characters
{
  public class CharacterAttack : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject capsule;
    [SerializeField] private GameObject shortAttackWeapon;
    
    private GameObject raycastObject;

    private AttackType attackType;
    private CharacterType characterType;
    private bool playerHasBothAttackTypes;

    private float shortAttackRange;
    private int shortAttackDamage;
    private float shortAttackCooldown;
    private float shortAttackStoppingDistance_NPC;

    private float shortAttackTimer;
    private bool doShortAttack_Player = false;
    private float shortAttackAnimationDuration = 0.2f; // Must always be a less value than shortAttackCooldown

    private int longAttackDamage;
    private float longAttackShotFrecuency;
    private float longAttackProjectileSpeed;
    private float longAttackProjectileRange;
    private float longAttackShotRange_NPC;
    private float longAttackShotTimePreparation_NPC;
    private float longAttackStoppingDistance_NPC;

    private float longAttackTimer;

    #region Getters and setters
    public AttackType AttackType { get => attackType; set => attackType = value; }
    public CharacterType CharacterType { get => characterType; set => characterType = value; }
    public bool PlayerHasBothAttackTypes { get => playerHasBothAttackTypes; set => playerHasBothAttackTypes = value; }

    public float ShortAttackRange { get => shortAttackRange; set => shortAttackRange = value; }
    public int ShortAttackDamage { get => shortAttackDamage; set => shortAttackDamage = value; }
    public float ShortAttackCooldown { get => shortAttackCooldown; set => shortAttackCooldown = value; }
    public float ShortAttackStoppingDistance_NPC { get => shortAttackStoppingDistance_NPC; set => shortAttackStoppingDistance_NPC = value; }

    public int LongAttackDamage { get => longAttackDamage; set => longAttackDamage = value; }
    public float LongAttackShotFrecuency { get => longAttackShotFrecuency; set => longAttackShotFrecuency = value; }
    public float LongAttackProjectileSpeed { get => longAttackProjectileSpeed; set => longAttackProjectileSpeed = value; }
    public float LongAttackProjectileRange { get => longAttackProjectileRange; set => longAttackProjectileRange = value; }
    public float LongAttackShotRange_NPC { get => longAttackShotRange_NPC; set => longAttackShotRange_NPC = value; }
    public float LongAttackShotTimePreparation_NPC { get => longAttackShotTimePreparation_NPC; set => longAttackShotTimePreparation_NPC = value; }
    public float LongAttackStoppingDistance_NPC { get => longAttackStoppingDistance_NPC; set => longAttackStoppingDistance_NPC = value; }
    #endregion

    public void Init()
    {
      agent = GetComponent<NavMeshAgent>();

      if (characterType == CharacterType.Player)
      {
        raycastObject = capsule;
      }
      else if (characterType == CharacterType.NPC)
      {
        raycastObject = gameObject;
      }

      ClampShortAttackAnimationDuration();
    }

    private void ClampShortAttackAnimationDuration()
    {
      if(shortAttackAnimationDuration >= shortAttackCooldown)
      {
        // Set shortAttackAnimationDuration as a 80% the shortAttackCooldown
        shortAttackAnimationDuration = shortAttackCooldown * 0.8f;
      }
    }

    public void SetAttackTypeBehaviour()
    {
      if (attackType == AttackType.Short)
      {
        agent.stoppingDistance = shortAttackStoppingDistance_NPC;
      }
      else if (attackType == AttackType.Long)
      {
        agent.stoppingDistance = longAttackStoppingDistance_NPC;
      }
    }

    private void Update()
    {
      shortAttackTimer += Time.deltaTime;

      if (attackType == AttackType.Short || characterType == CharacterType.Player)
      {
        CheckForShortAttackHit();
      }
    }

    void CheckForShortAttackHit()
    {
      RaycastHit objectHit;

      Vector3 forward = raycastObject.transform.TransformDirection(Vector3.forward);
      Debug.DrawRay(raycastObject.transform.position, forward * shortAttackRange, Color.green);

      if (Physics.Raycast(raycastObject.transform.position, forward, out objectHit, shortAttackRange))
      {
        ManagePlayerShortAttack(objectHit);
        ManageNPCShortAttack(objectHit);
      }

      doShortAttack_Player = false;
    }

    #region Short attack
    private void ManagePlayerShortAttack(RaycastHit objectHit)
    {
      if (!doShortAttack_Player) return;

      Debug.Log("objectHit.transform.tag: " + objectHit.transform.tag);

      if (objectHit.transform.parent.CompareTag("NPC"))
      {
        CharacterHealth characterHealth_Victim = objectHit.transform.parent.GetComponent<CharacterHealth>();
        DoShortAttack(characterHealth_Victim);
      }
    }

    public void DoShortAttack(CharacterHealth characterHealth_Victim)
    {
      Debug.Log("DoShortAttack");

      if (shortAttackTimer >= shortAttackCooldown)
      {
        Debug.Log("Attack to enemy");
        shortAttackTimer = 0;

        // do damage
        characterHealth_Victim.TakeDamage(shortAttackDamage);

        Debug.Log("# CharacterAttack # Attacker: " + gameObject.name + 
          ". Victim: " + characterHealth_Victim + 
          ". Damage: " + shortAttackDamage);
      }
    }

    private void ShowShortAttackAnimation()
    {
      if (shortAttackWeapon.activeSelf) return; // Is already doing the animation
      if (shortAttackTimer < shortAttackCooldown) return;

      shortAttackWeapon.SetActive(true);
      Debug.Log("shortAttackWeapon.activeSelf: " + shortAttackWeapon.activeSelf);
      StartCoroutine(CoShowShortAttackAnimation());
    }

    private IEnumerator CoShowShortAttackAnimation()
    {
      yield return new WaitForSeconds(shortAttackAnimationDuration);
      shortAttackWeapon.SetActive(false);
    }

    private void ManageNPCShortAttack(RaycastHit objectHit)
    {
      // TO DO...
      Debug.Log("objectHit.transform.tag: " + objectHit.transform.tag);

      if (objectHit.transform.parent.CompareTag("Player"))
      {
        ShowShortAttackAnimation();
        CharacterHealth characterHealth_Victim = objectHit.transform.parent.GetComponent<CharacterHealth>();
        DoShortAttack(characterHealth_Victim);
      }
    }
    #endregion

    #region Input System
    public void OnAttack(InputAction.CallbackContext value)
    {
      if (value.started)
      {
        Debug.Log("# CharacterAttack # OnAttack");

        if(characterType == CharacterType.Player)
        {
          doShortAttack_Player = true;
          ShowShortAttackAnimation();
        }
      }
    }
    #endregion
  }
}