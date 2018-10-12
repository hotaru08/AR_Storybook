using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.Helper;

/// <summary>
/// For controlling the Battle Game, eg. Win, Lose and Pause events
/// </summary>
public class BattleSceneGameManager : SingletonBehaviour<BattleSceneGameManager>
{
    public enum GAME_STATES
    {
        GAME,
        WIN,
        LOSE
    }
    public GAME_STATES m_gameState;

    /// <summary>
    /// Unity Start, 
    /// </summary>
    private void Start()
    {
        
    }

}
