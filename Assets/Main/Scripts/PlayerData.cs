using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public const string BEST_SCORE_SAVE_KEY = "BestScore";
    public static PlayerData Instance;

    public int Score { get; private set; }
    public int BestScore { get; private set; }
    public int PassedLeveles { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        if (PlayerPrefs.HasKey(BEST_SCORE_SAVE_KEY))
        {
            BestScore = PlayerPrefs.GetInt(BEST_SCORE_SAVE_KEY);
        }
        else
        {
            BestScore = 0;
            PlayerPrefs.SetInt(BEST_SCORE_SAVE_KEY, BestScore);
        }
    }

    public void AddScorePoints(int scorePoints)
    {
        if (scorePoints > 0)
        {
            Score += scorePoints;
            if (Score > BestScore)
            {
                BestScore = Score;
            }
        }
    }

    public void IncPassedLevels(int value)
    {
        if (value <= 0)
        {
            Debug.LogError("incorrect value");
            return;
        }

        PassedLeveles += value;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(BEST_SCORE_SAVE_KEY, BestScore);
    }
}
