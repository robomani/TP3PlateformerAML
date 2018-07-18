using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text m_Lifes;

	private void Awake ()
    {
        m_Lifes.text = LevelManager.Instance.m_Lives.ToString();
        PoolManager.Instance.m_OnLifeLost += LoseLife;
	}

    private void LoseLife()
    {
        m_Lifes.text = LevelManager.Instance.m_Lives.ToString();
    }

    private void OnDestroy()
    {
        PoolManager.Instance.m_OnLifeLost -= LoseLife;
    }
}
