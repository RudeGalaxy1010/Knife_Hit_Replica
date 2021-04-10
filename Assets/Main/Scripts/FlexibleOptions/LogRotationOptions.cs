using UnityEngine;

[CreateAssetMenu(fileName = "NewLogRotationOptions", menuName = "FlexibleOptions/LogRotationOptions", order = 0)]
public class LogRotationOptions : ScriptableObject
{
    [Header("Basics")]
    public int StartDirection = 1;
    public float RotationSpeed;

    [Header("Time")]
    [Tooltip("min time to change direction in seconds")]
    public float ChangeDirectionMinTime;
    [Tooltip("max time to change direction in seconds")]
    public float ChangeDirectionMaxTime;
    [Tooltip("time to stop and change direction in seconds")]
    public float StopTime;

    [Header("Chance")]
    [Range(0, 1)]
    public float DirectionChangeChance;
}
