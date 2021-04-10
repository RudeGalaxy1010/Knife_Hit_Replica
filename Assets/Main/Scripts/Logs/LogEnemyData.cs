using System.Collections.Generic;
using UnityEngine;

public class LogEnemyData : MonoBehaviour
{
    public static LogEnemyData Instance;

    [SerializeField]
    private Transform LogPosition;
    [SerializeField]
    private List<LogEnemy> logEnemyPrefabs;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        var logPrefab = logEnemyPrefabs[Random.Range(0, logEnemyPrefabs.Count)];
        Instantiate(logPrefab, LogPosition.position, Quaternion.identity);

        var knifeThrower = FindObjectOfType<KnifeThrower>();
        knifeThrower.OnAppleHit += ProcessAppleHit;
        knifeThrower.OnKnifeHit += ProcessKnifeHit;
        knifeThrower.OnLogHit += ProcessLogHit;
    }

    public void ProcessAppleHit(Apple apple)
    {
        PlayerData.Instance.AddScorePoints(apple.ScorePoints);
    }

    public void ProcessKnifeHit(Knife knife)
    {
        Debug.Log("You're lose");
        // Pop up menu
    }

    public void ProcessLogHit(LogEnemy log)
    {
        log.ApplyDamage(1);
        if (log.Health <= 0)
        {
            Debug.Log("You win");
            // Pop up menu
            SceneTransition.Instance.ReloadCurrentScene(3);
        }
    }
}
