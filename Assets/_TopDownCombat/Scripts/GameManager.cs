using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownCombat
{
  public class GameManager : MonoBehaviour
  {

    #region Init
    private void Start()
    {
      Init();
    }

    private void Init()
    {
      // Initialize scripts here

      AddListeners();
    }
    #endregion

    #region Listeners
    private void AddListeners()
    {
    }

    private void RemoveListeners()
    {
    }

    private void OnDestroy()
    {
      RemoveListeners();
    }
    #endregion
  }
}
