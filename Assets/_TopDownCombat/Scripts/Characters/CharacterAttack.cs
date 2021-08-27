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
    
    private GameObject raycastObject;

    private AttackType attackType;
    private CharacterType characterType;
    private bool playerHasBothAttackTypes;

    private float shortAttackRange;
    private int shortAttackDamage;
    private float shortAttackCooldown;
    private float shortAttackStoppingDistance_NPC;

    private int longAttackDamage;
    private float longAttackShotFrecuency;
    private float longAttackProjectileSpeed;
    private float longAttackProjectileRange;
    private float longAttackShotRange_NPC;
    private float longAttackShotTimePreparation_NPC;
    private float longAttackStoppingDistance_NPC;

    private float shortAttackTimer;
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
    }

    private void Update()
    {
      //CheckForHit();

      shortAttackTimer += Time.deltaTime;
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

    public void DoShortAttack()
    {
      if(shortAttackTimer >= shortAttackCooldown)
      {
        shortAttackTimer = 0;
        CheckForHit();
      }
    }

    void CheckForHit()
    {
      RaycastHit objectHit;

      Vector3 forward = raycastObject.transform.TransformDirection(Vector3.forward);
      Debug.DrawRay(raycastObject.transform.position, forward * shortAttackRange, Color.green);

      if (Physics.Raycast(raycastObject.transform.position, forward, out objectHit, shortAttackRange))
      {
        Debug.Log("objectHit.transform.tag: " + objectHit.transform.tag);

        if (objectHit.transform.parent.CompareTag("NPC"))
        {
          Debug.Log("Close to enemy");
        }
        /*
        if (objectHit.transform.parent.CompareTag("Player"))
        {
          Debug.Log("Close to player");
        }
        */
      }
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
      if (value.started)
      {
        Debug.Log("# CharacterAttack # OnAttack");
        DoShortAttack();
      }
    }
  }
}