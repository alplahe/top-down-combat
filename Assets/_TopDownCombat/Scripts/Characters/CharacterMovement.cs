using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace TopDownCombat.Characters
{
  public class CharacterMovement : MonoBehaviour
  {
    [SerializeField] private Transform target;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform child;
    [SerializeField] private Camera characterCamera;
    [SerializeField] private GameObject characterCameraGO;

    private CharacterType characterType;

    private Material playerMaterial;
    private Material NPCMaterial;

    private float speed;
    private float angularSpeed;

    private const float SPEED_MULTIPLIER = 0.01f;
    private const float MAX_ANGLE = 360.0f;

    private Vector3 inputMovement;
    private Vector3 worldPosition;
    private float angle;

    #region Init
    private void Awake()
    {
      agent = GetComponent<NavMeshAgent>();
      child = GetComponentsInChildren<Transform>()[1]; // Index 0 is the parent
      
      if(characterCameraGO != null)
      {
        characterCamera = characterCameraGO.GetComponent<Camera>();
      }

      SetCharactersBehaviour();
    }

    private void OnValidate()
    {
      SetAgentSpeeds();
      SetCharactersBehaviour();
    }

    public void SetAgentSpeeds()
    {
      agent.speed = speed;
      agent.angularSpeed = angularSpeed;
    }

    public void SetCharactersBehaviour()
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
    #endregion

    #region Setters
    public void SetCharacterType(CharacterType _characterType)
    {
      characterType = _characterType;
    }

    public void SetPlayerMaterial(Material material)
    {
      playerMaterial = material;
    }

    public void SetNPCMaterial(Material material)
    {
      NPCMaterial = material;
    }

    public void SetSpeed(float _speed)
    {
      speed = _speed;
    }

    public void SetAngularSpeed(float _angularSpeed)
    {
      angularSpeed = _angularSpeed;
    }
    #endregion

    private void Update()
    {
      if (characterType == CharacterType.NPC)
      {
        DoNPCBehaviour();
        return;
      }
      else if (characterType == CharacterType.Player)
      {
        DoPlayerBehaviour();
      }

      ApplyMovement();
      ApplyRotation();
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

      transform.rotation = Quaternion.identity; // Restart rotation

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
    private void ApplyMovement()
    {
      float xDirection = inputMovement.x;
      float zDirection = inputMovement.y;

      Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);
      transform.position += moveDirection * speed * SPEED_MULTIPLIER;
    }

    private void ApplyRotation()
    {
      AssignPlayerWithCursorAngle();
      child.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void AssignPlayerWithCursorAngle()
    {
      Vector3 mousePosition = Mouse.current.position.ReadValue();
      mousePosition.z = characterCamera.transform.position.y - transform.position.y;
      worldPosition = characterCamera.ScreenToWorldPoint(mousePosition);

      Vector3 cursorToBallVector = worldPosition - transform.position;

      float cursorToPlayerAngle = Vector3.Angle(cursorToBallVector, Vector3.forward);

      cursorToPlayerAngle = ReformatAngle(worldPosition, cursorToPlayerAngle);
      angle = cursorToPlayerAngle;
    }

    // Reformat angle to 0-360 degrees. Previously it was 0-180, 180-0.
    private float ReformatAngle(Vector3 worldPosition, float cursorToPlayerAngle)
    {
      if (worldPosition.x - transform.position.x < 0) cursorToPlayerAngle = MAX_ANGLE - cursorToPlayerAngle;
      return cursorToPlayerAngle;
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