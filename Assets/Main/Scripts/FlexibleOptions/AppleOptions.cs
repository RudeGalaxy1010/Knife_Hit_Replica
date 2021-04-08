using UnityEngine;

[CreateAssetMenu(fileName = "NewAppleOptions", menuName = "FlexibleOptions/AppleOptions", order = 0)]
public class AppleOptions : ScriptableObject
{
    [SerializeField] private Apple _applePrefab;
    public Apple ApplePrefab => _applePrefab;

    [Range(0f, 1f)]
    [SerializeField] private float _spawnChance;
    public float SpawnChance => _spawnChance;
}
