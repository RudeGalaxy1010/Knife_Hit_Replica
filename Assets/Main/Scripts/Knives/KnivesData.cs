using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnivesData : MonoBehaviour
{
    // Global access
    public static KnivesData Instance;
    
    // Save key for PlayerPrefs
    public const string CURRENT_KNIFE_INDEX_SAVE_KEY = "KnifesData";

    public Knife CurrentKnifePrefab;
    public List<Knife> AllKnives = new List<Knife>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        // Load Data
        if (PlayerPrefs.HasKey(CURRENT_KNIFE_INDEX_SAVE_KEY))
        {
            var index = PlayerPrefs.GetInt(CURRENT_KNIFE_INDEX_SAVE_KEY);
            CurrentKnifePrefab = AllKnives[index];
        }
        else
        {
            SetCurrentKnife(0);
        }
    }

    public void SetCurrentKnife(int index)
    {
        CurrentKnifePrefab = AllKnives[index];
        PlayerPrefs.SetInt(CURRENT_KNIFE_INDEX_SAVE_KEY, index);
    }
}
