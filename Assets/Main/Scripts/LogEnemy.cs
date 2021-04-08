using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LogEnemy : MonoBehaviour
{
    public float RotationSpeed = 10f;

    [Header("Apples")]
    public AppleOptions AppleOptions;
    [SerializeField] private List<Transform> _appleHolders = new List<Transform>();

    [Header("Knives")]
    public KnifeOptions KnifeOptions;
    [SerializeField] private List<Transform> _knifeHolders = new List<Transform>();

    private void Start()
    {
        GenerateApples();
        GenerateKnives();
    }

    private void Update()
    {
        transform.Rotate(Vector3.back, RotationSpeed * Time.deltaTime);
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
}
