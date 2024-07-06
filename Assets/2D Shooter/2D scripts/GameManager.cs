using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum DifficultLevel
    {
        Easy,
        Medium,
        Hard
    }

    /// <summary>
    /// Defines the difficult level of the game.
    /// </summary>
    [field:SerializeField] public DifficultLevel Difficult_Level { get; private set; }

    public DifficultLevel GetDifficultLevel => Difficult_Level;


    #region Monobehaviour callbacks
    void Start()
    {
        
    }
    #endregion


}
