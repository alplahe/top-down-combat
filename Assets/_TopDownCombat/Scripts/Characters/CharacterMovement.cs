using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

  public class CharacterMovement : MonoBehaviour
  {
    [SerializeField] private Transform target;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private CharacterType characterType;
    [SerializeField] private Transform child;
    [SerializeField] private Camera characterCamera;
    [SerializeField] private GameObject characterCameraGO;

    [Header("Materials")]
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material NPCMaterial;

    [Header("Speed")]
    [SerializeField, Range(0, 10)] private float speed;
    [SerializeField] private float angularSpeed;

    private const float SPEED_MULTIPLIER = 0.01f;
    private Vector3 inputMovement;

    private void Awake()
    {
      agent = GetComponent<NavMeshAgent>();
      child = GetComponentsInChildren<Transform>()[1]; // Index 0 is the parent
      characterCamera = GetComponent<Camera>();

      SetCharactersBehaviour();
    }

    private void OnValidate()
    {
      agent.speed = speed;
      agent.angularSpeed = angularSpeed;

      SetCharactersBehaviour();
    }

    private void SetCharactersBehaviour()
    {
      if (characterType == CharacterType.NPC)
      {
        SetNPCBehaviour();
        return;
      }
      else if (characterType == CharacterType.Player)
      {
        SetPlayerBehaviour(); // TODO (24-08-2021): When changing an NPC to a Player in runtime, 
                              // automatically convert the previous Player to NPC
      }
    }

    private void Update()
    {
      if(characterType == CharacterType.NPC)
      {
        DoNPCBehaviour();
        return;
      }
      else if(characterType == CharacterType.Player)
      {
        DoPlayerBehaviour();
      }

      ApplyMovement();
    }

    #region Characters
    private void SetNPCBehaviour()
    {
      gameObject.tag = characterType.ToString();
      gameObject.name = characterType.ToString();

      if (NPCMaterial != null && child != null)
      {
        child.GetComponent<MeshRenderer>().material = NPCMaterial;
      }

      if (GameObject.FindGameObjectWithTag(CharacterType.Player.ToString()) != null)
      {
        playerTarget = GameObject.FindGameObjectWithTag(CharacterType.Player.ToString()).transform;
      }

      if (characterCameraGO != null)
      {
        characterCameraGO.SetActive(false);
        characterCameraGO.GetComponent<Camera>().enabled = false;
      }
    }

    private void DoNPCBehaviour()
    {
      agent.isStopped = false;
      //agent.SetDestination(target.position);

      if (playerTarget != null)
      {
        agent.SetDestination(playerTarget.position);
      }
    }

    private void SetPlayerBehaviour()
    {
      gameObject.tag = characterType.ToString();
      gameObject.name = characterType.ToString();

      if (agent.isOnNavMesh)
      {
        agent.isStopped = true;
      }

      if (NPCMaterial != null && child != null)
      {
        child.GetComponent<MeshRenderer>().material = playerMaterial;
      }

      if (characterCameraGO != null)
      {
        characterCameraGO.SetActive(true);
        characterCameraGO.GetComponent<Camera>().enabled = true;
      }
    }

    private void DoPlayerBehaviour()
    {
    }
    #endregion

    #region Movement
    public void ApplyMovement()
    {
      float xDirection = inputMovement.x;
      float zDirection = inputMovement.y;

      Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);
      transform.position += moveDirection * speed * SPEED_MULTIPLIER;
    }
    #endregion

    #region Input System
    public void OnAttack(InputAction.CallbackContext value)
    {
      if (value.started)
      {
        Debug.Log("OnAttack");
      }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
      Vector2 _inputMovement = value.ReadValue<Vector2>();
      Debug.Log("inputMovement: " + _inputMovement);
      inputMovement = _inputMovement;
    }

    public void OnReturnToInitialScreen(InputAction.CallbackContext value)
    {
      if (value.started)
      {
        Debug.Log("OnReturnToInitialScreen");
        Messenger.Broadcast(BroadcastName.Game.OnReturnToInitialScreen);
      }
    }
    #endregion
  }
}