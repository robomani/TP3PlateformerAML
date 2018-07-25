using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenScript : MonoBehaviour
{
    public Text m_Text;
    private void Start()
    {
        if (LevelManager.Instance && LevelManager.Instance.m_Win)
        {
            m_Text.text = "You Win!";
            LevelManager.Instance.m_Win = false;
        }
        else if (LevelManager.Instance && LevelManager.Instance.m_Lives > 0)
        {
            m_Text.text = "You lose a life! You have " + LevelManager.Instance.m_Lives.ToString() + " Lives left.";
        }
        else
        {
            m_Text.text = "You have no life left you lose!";
        }
    }

    public void ContinueGame()
    {
        if (LevelManager.Instance.m_Lives <= 0)
        {
            LevelManager.Instance.m_Lives = 3;
            LevelManager.Instance.m_SpawnPos = Vector3.zero;
            LevelManager.Instance.ChangeLevel("CaracterSelection");
        }
        else
        {
            LevelManager.Instance.ChangeLevel("Game");
        }

    }
}
