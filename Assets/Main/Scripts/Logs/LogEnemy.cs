using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LogEnemy : MonoBehaviour
{
    [SerializeField]
    private LogRotationOptions RotationOptions;
    private int _rotationDirection;

    public int Health => _health;
    [SerializeField]
    private int _health = 5;

    [Header("Apples")]
    [SerializeField]
    private AppleOptions AppleOptions;
    [SerializeField] private List<Transform> _appleHolders = new List<Transform>();

    [Header("Knives")]
    [SerializeField]
    private KnifeOptions KnifeOptions;
    [SerializeField] private List<Transform> _knifeHolders = new List<Transform>();

    private void Start()
    {
        GenerateApples();
        GenerateKnives();

        _rotationDirection = RotationOptions.StartDirection;
        StartCoroutine(RandomizeRotationDirection(RotationOptions));
    }

    private void Update()
    {
        transform.Rotate(Vector3.back * _rotationDirection, RotationOptions.RotationSpeed * Time.deltaTime);
    }

    private void GenerateKnives()
    {
        for (int i = 0; i < _knifeHolders.Count; i++)
        {
            if (Random.value < KnifeOptions.SpawnChance)
            {
                var rotation = _knifeHolders[i].rotation;
                var knife = Instantiate(KnifeOptions.KnifePrefab, _knifeHolders[i].position, rotation);
                knife.DeactivatePhysics();
                // To save relative scale
                knife.transform.SetParent(_knifeHolders[i]);
            }
        }
    }

    private void GenerateApples()
    {
        for (int i = 0; i < _appleHolders.Count; i++)
        {
            if (Random.value < AppleOptions.SpawnChance)
            {
                var rotation = _appleHolders[i].rotation;
                var apple = Instantiate(AppleOptions.ApplePrefab, _appleHolders[i].position, rotation);
                // To save relative scale
                apple.transform.SetParent(_appleHolders[i]);
            }
        }
    }

    public void ApplyDamage(int damage)
    {
        if (damage > 0)
        {
            _health -= damage;
            if (Health <= 0)
            {
                // Spawn particles
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("incorrect damage value");
        }
    }

    private IEnumerator RandomizeRotationDirection(LogRotationOptions rotationOptions)
    {
        var minTime = rotationOptions.ChangeDirectionMinTime;
        var maxTime = rotationOptions.ChangeDirectionMaxTime;
        // Wait for some time
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        // Make a stop
        var direction = _rotationDirection;
        _rotationDirection = 0;
        yield return new WaitForSeconds(RotationOptions.StopTime);

        // Change direction with % chance
        if (Random.value <= RotationOptions.DirectionChangeChance)
        {
            direction *= (-1);
        }

        _rotationDirection = direction;
        StartCoroutine(RandomizeRotationDirection(rotationOptions));
    }
}
