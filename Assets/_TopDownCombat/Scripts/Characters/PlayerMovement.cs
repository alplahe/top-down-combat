using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownCombat.Characters
{
  public enum CharacterType
  {
    Player,
    NPC
  }

  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private CharacterType characterType;

    private void Awake()
    {
      agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
      if(characterType == CharacterType.NPC)
      {
        agent.SetDestination(target.position);
        return;
      }
      else if(characterType == CharacterType.Player)
      {

      }
    }
  }
}