using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Character")]
    [SerializeField] private CharacterType characterType;

    [Header("Materials")]
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material NPCMaterial;

    [Header("Speed")]
    [SerializeField, Range(0, 10)] private float speed;
    [SerializeField] private float angularSpeed;

    private void Awake()
    {
      characterMovement = GetComponent<CharacterMovement>();
      if (characterMovement == null) Debug.Log("characterMovement is NULL!");
    }

    private void OnValidate()
    {
      SetCharacterMovementVariables();
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
  }
}