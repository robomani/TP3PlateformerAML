using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectScript : MonoBehaviour
{
    public void SelectBatman()
    {
        LevelManager.Instance.m_Char = EPoolType.Batman;
        LevelManager.Instance.ChangeLevel("Game");
    }

    public void SelectRanma()
    {
        LevelManager.Instance.m_Char = EPoolType.Ranma;
        LevelManager.Instance.ChangeLevel("Game");
    }

    public void SelectSalor()
    {
        LevelManager.Instance.m_Char = EPoolType.Sailor;
        LevelManager.Instance.ChangeLevel("Game");
    }
}
