using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownCombat.Characters
{
  public class CharacterAttack : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent agent;

    private AttackType attackType;
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

    #region Getters and setters
    public AttackType AttackType { get => attackType; set => attackType = value; }
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
  }
}