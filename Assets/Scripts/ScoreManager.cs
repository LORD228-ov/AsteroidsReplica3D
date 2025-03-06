using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class ScoreData
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}

public class ScoreManager : MonoBehaviour
{
    private const string SaveKey = "TopScores";
    public List<ScoreEntry> scores = new List<ScoreEntry>();

    private void Awake()
    {
        LoadScores();
    }

    public void AddScore(string playerName, int score)
    {
        scores.Add(new ScoreEntry { playerName = playerName, score = score });
        scores = scores.OrderByDescending(s => s.score).Take(10).ToList(); // Оставляем только топ-10
        SaveScores();
    }

    public void SaveScores()
    {
        string json = JsonUtility.ToJson(new ScoreData { scores = scores });
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            scores = JsonUtility.FromJson<ScoreData>(json).scores;
        }
    }
}
