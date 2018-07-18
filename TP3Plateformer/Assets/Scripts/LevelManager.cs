using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public GameObject m_LoadingScreen;
    private static LevelManager m_Instance;
    public static LevelManager Instance
    {
        get { return m_Instance; }
    }

    public Action m_OnChangeScene;


    public EPoolType m_Char;
    public Vector3 m_SpawnPos;
    public int m_Lives = 3;

    private bool m_WaitTimeReady;
    private bool m_LoadingReady;


    private void Awake()
    {
        //Destroy the object if it is not the first
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        //else assign the first instance to show that the singleton exist and let it be called
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        m_LoadingScreen.SetActive(false);
    }

    private void StartLoading()
    {
        m_LoadingScreen.SetActive(true);
        //Play Animation
    }

    private void OnLoadingDone(Scene i_Scene, LoadSceneMode i_Mode)
    {
        //we remove the function to the action/event list
        SceneManager.sceneLoaded -= OnLoadingDone;
        if (m_SpawnPos == null)
        {
            m_SpawnPos = Vector3.zero;
        }
        if (i_Scene.name == "Game")
        {
            PoolManager.Instance.GetFromPool(m_Char, m_SpawnPos);
        }

        if (m_WaitTimeReady)
        {
            m_LoadingScreen.SetActive(false);
        }
        else
        {
            m_LoadingReady = true;
        }
    }

    public void ChangeLevel(string i_Scene, float m_Time = 1.5f)
    {
        m_WaitTimeReady = false;
        m_LoadingReady = false;
        StartLoading();
        if (m_OnChangeScene != null)
        {
            m_OnChangeScene();
        }
        SceneManager.LoadScene(i_Scene);
        StartCoroutine(WaitLoading(m_Time));
        SceneManager.sceneLoaded += OnLoadingDone;
    }

    private IEnumerator WaitLoading(float i_Time)
    {
        if (i_Time <= 0)
        {
            i_Time = 0.1f;
        }
        yield return new WaitForSeconds(i_Time);
        m_WaitTimeReady = true;
        if (m_LoadingReady)
        {
            m_LoadingScreen.SetActive(false);
        }
    }
}
