using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public const string BEST_SCORE_SAVE_KEY = "BestScore";
    public static PlayerData Instance;

    public int Score { get; private set; }

    public int BestScore { get; private set; }

    private void Awake()
    {
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

        var knifeThrower = FindObjectOfType<KnifeThrower>();
        knifeThrower.OnAppleHit += ProcessAppleHit;
        knifeThrower.OnKnifeHit += ProcessKnifeHit;
        knifeThrower.OnLogHit += ProcessLogHit;
    }

    public void ProcessAppleHit(Apple apple)
    {
        AddScorePoints(apple.ScorePoints);
    }

    public void ProcessKnifeHit(Knife knife)
    {
        Debug.Log("You're lose");
    }

    public void ProcessLogHit(LogEnemy log)
    {

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

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(BEST_SCORE_SAVE_KEY, BestScore);
    }
}
