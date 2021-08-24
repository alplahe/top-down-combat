using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownCombat.Characters
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
      agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
      agent.SetDestination(target.position);
    }
  }
}