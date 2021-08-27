using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TopDownCombat.Characters;
using UnityEngine;

namespace TopDownCombat
{
  public class SpawnerManager : MonoBehaviour
  {
    [Header("Prefabs")]
    [SerializeField] private Character playerPrefab;
    [SerializeField] private Character NPCPrefab;

    [Header("Intances")]
    [SerializeField] private GameObject playerGO;
    [SerializeField] private List<GameObject> npcGOList;
    [SerializeField] private Transform npcInstances;

    [Header("Spawners")]
    [SerializeField] private Transform playerSpawner;
    [SerializeField] private GameObject NPCSpawnerParent;
    [SerializeField] private List<Spawner> NPCSpawners;

    [Header("Checkers")]
    [SerializeField] private bool instantiatePlayer = true;
    [SerializeField] private bool instantiateNPCs = true;

    private bool isInited = false;

    #region Init
    public void Init()
    {
      isInited = true;

      AddListeners();

      if (NPCSpawnerParent != null)
        NPCSpawners = NPCSpawnerParent.GetComponentsInChildren<Spawner>().ToList();

      SpawnPlayer();

      SpawnMultipleNPCs();
    }
    #endregion

    #region Listeners
    private void AddListeners()
    {
      Messenger.AddListener(BroadcastName.Spawner.OnSpawnerOneNPC, OnSpawnerOneNPC);
      Messenger.AddListener<int>(BroadcastName.Health.OnNPCDie, OnNPCDie);
    }

    private void RemoveListeners()
    {
      Messenger.RemoveListener(BroadcastName.Spawner.OnSpawnerOneNPC, OnSpawnerOneNPC);
      Messenger.RemoveListener<int>(BroadcastName.Health.OnNPCDie, OnNPCDie);
    }

    private void OnDestroy()
    {
      if (isInited)
      {
        RemoveListeners();
        isInited = false;
      }
    }
    #endregion


    private void SpawnPlayer()
    {
      if (!instantiatePlayer) return;

      playerGO = Instantiate(playerPrefab, playerSpawner).gameObject;
      playerGO.GetComponent<Character>().PreInit();
      playerGO.GetComponent<Character>().Init();
    }

    private void SpawnMultipleNPCs()
    {
      npcGOList = new List<GameObject>();

      SpawnNPC();
    }

    private void SpawnNPC()
    {
      if (!instantiateNPCs) return;

      GameObject npcGO = Instantiate(NPCPrefab, GetRandomNPCSpawner()).gameObject;
      npcGO.transform.parent = npcInstances;
      npcGOList.Add(npcGO);

      npcGO.GetComponent<Character>().PreInit();
      npcGO.GetComponent<Character>().Init();
    }

    private Transform GetRandomNPCSpawner()
    {
      int randomSpawnerNum = Random.Range(0, NPCSpawners.Count - 1);
      return NPCSpawners[randomSpawnerNum].transform;
    }

    private void OnSpawnerOneNPC()
    {
      SpawnNPC();
    }

    private void OnNPCDie(int npcInstanceID)
    {
      Debug.Log("# SpawnerManager # OnNPCDie");
      for (int i = 0; i < npcGOList.Count; i++)
      {
        GameObject npcGO = npcGOList[i];
        if (npcInstanceID == npcGO.GetInstanceID())
        {
          npcGOList.RemoveAt(i);
          Debug.Log("# SpawnerManager # Destroy npc: " + npcGO);
          Destroy(npcGO);
        }
      }
    }
  }
}