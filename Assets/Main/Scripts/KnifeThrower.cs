using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KnifeThrower : MonoBehaviour
{
    public UnityAction OnLogHit;
    public UnityAction OnKnifeHit;
    public UnityAction OnAppleHit;

    public Vector2 ThrowDirection = Vector2.up;
    public float MinTimeToThrow = 0.5f;

    private Knife _knifePrefab;    
    private Knife _preparedKnife = null;

    private void Start()
    {
        // Set knife prefab from database
        _knifePrefab = KnivesData.Instance.CurrentKnifePrefab;
        // Initialize
        PrepareNextKnife(_knifePrefab);
    }

    public void PrepareNextKnife(Knife knifePrefab)
    {
        if (_preparedKnife != null)
        {
            Debug.LogWarning("Knife already prepared");
            return;
        }

        _preparedKnife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
        _preparedKnife.DeactivatePhysics();
        _preparedKnife.DeactivateCollider();

        _preparedKnife.OnHit += OnGameObjectHit;
    }

    public void ThrowNextKnife()
    {
        if (_preparedKnife == null)
        {
            Debug.LogWarning("Should call 'PrepareNextKnife(Knife knifePrefab)' first");
            return;
        }

        // Throw
        _preparedKnife.ActivateCollider();
        _preparedKnife.ActivatePhysics();
        _preparedKnife.Throw(ThrowDirection);

        // Prepare new knife
        _preparedKnife = null;
        StartCoroutine(WaitAndSpawnNextKnife(_preparedKnife, MinTimeToThrow));
    }

    public IEnumerator WaitAndSpawnNextKnife(Knife knifePrefab, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PrepareNextKnife(_knifePrefab);
    }

    public void OnGameObjectHit(GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out LogEnemy log))
        {
            OnLogHit?.Invoke();
        }
        else if (hitObject.TryGetComponent(out Knife knife))
        {
            OnKnifeHit?.Invoke();
        }
        else if (hitObject.TryGetComponent(out Apple apple))
        {
            OnAppleHit?.Invoke();
        }
        else
        {
            Debug.LogWarning("Unknown object got hit!");
        }
    }
}
