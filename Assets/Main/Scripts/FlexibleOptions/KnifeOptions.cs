using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKnifeOptions", menuName = "FlexibleOptions/KnifeOptions", order = 0)]
public class KnifeOptions : ScriptableObject
{
    public Knife KnifePrefab => KnivesData.Instance.CurrentKnifePrefab;

    [Range(0f, 1f)]
    [SerializeField] private float _spawnChance;
    public float SpawnChance => _spawnChance;
}
