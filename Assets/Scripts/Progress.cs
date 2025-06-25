using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int Score = 0;
}

public class Progress : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;

            LoadScore(); // New - загрузка очков при запуске
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit() // New - сохранение очков
    {
        SaveScore();
    }

    public void SaveScore() // New
    {
        PlayerPrefs.SetInt("PlayerScore", PlayerInfo.Score);
        PlayerPrefs.Save();
    }

    public void LoadScore() // New
    {
        PlayerInfo.Score = PlayerPrefs.GetInt("PlayerScore", 0);
    }

    public void ResetScore() // New (для 6 части workshop)
    {
        PlayerInfo.Score = 0;
        PlayerPrefs.DeleteKey("PlayerScore");
    }
}