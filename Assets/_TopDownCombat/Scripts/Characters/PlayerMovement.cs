using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

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

    public void OnAttack(InputAction.CallbackContext value)
    {
      if (value.started)
      {
        Debug.Log("OnAttack");
      }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
      Vector2 inputMovement = value.ReadValue<Vector2>();
      Debug.Log("inputMovement: " + inputMovement);
    }
    public void OnReturnToInitialScreen(InputAction.CallbackContext value)
    {
      if (value.started)
      {
        Debug.Log("OnReturnToInitialScreen");
        Messenger.Broadcast(BroadcastName.Game.OnReturnToInitialScreen);
      }
    }
  }
}