using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    public Vector2 ThrowDirection = Vector2.up;
    public float MinTimeToThrow = 0.5f;

    [SerializeField]
    private Knife _knifePrefab;
    
    private Knife _preparedKnife = null;
    private Rigidbody2D _preparedKnifeRigidBody;
    private Collider2D _preparedKnifeCollider;

    private void Start()
    {
        // Set knife prefab from database
        PrepareNextKnife(_knifePrefab);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            ThrowNextKnife();
        }
    }

    public void PrepareNextKnife(Knife knifePrefab)
    {
        if (_preparedKnife != null)
        {
            Debug.LogWarning("Knife already prepared");
            return;
        }

        _preparedKnife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
        _preparedKnifeRigidBody = _preparedKnife.GetComponent<Rigidbody2D>();
        _preparedKnifeCollider = _preparedKnife.GetComponent<Collider2D>();
        _preparedKnifeCollider.enabled = false;
    }

    public void ThrowNextKnife()
    {
        if (_preparedKnife == null)
        {
            Debug.LogWarning("Should call 'PrepareNextKnife(Knife knifePrefab)' first");
            return;
        }

        // Throw
        _preparedKnifeCollider.enabled = true;
        _preparedKnifeRigidBody.AddForce(ThrowDirection * _preparedKnife.Speed, ForceMode2D.Impulse);

        // Prepare new knife
        _preparedKnife = null;
        StartCoroutine(WaitAndSpawnNextKnife(_preparedKnife, MinTimeToThrow));
    }

    public IEnumerator WaitAndSpawnNextKnife(Knife knifePrefab, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PrepareNextKnife(_knifePrefab);
    }
}
