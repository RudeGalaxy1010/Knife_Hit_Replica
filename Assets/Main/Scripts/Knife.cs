using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour
{
    public UnityAction<GameObject> OnHit;
    public float Speed = 10f;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void ActivatePhysics()
    {
        _rigidbody.gravityScale = 1f;
        _rigidbody.WakeUp();
    }

    public void DeactivatePhysics()
    {
        _rigidbody.gravityScale = 0f;
        _rigidbody.Sleep();
    }

    public void ActivateCollider()
    {
        _collider.enabled = true;
    }

    public void DeactivateCollider()
    {
        _collider.enabled = false;
    }

    public void Throw(Vector3 throwDirection)
    {
        _rigidbody.AddForce(throwDirection * Speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit?.Invoke(collision.gameObject);

        // Hit another knife
        if (collision.gameObject.TryGetComponent(out Knife knife))
        {
            // Unset parent
            transform.SetParent(null);

            // Make the knife fall
            ActivatePhysics();
        }
        // Plug into log
        else if (collision.gameObject.TryGetComponent(out LogEnemy logEnemy))
        {
            // Set parent to rotate with log
            transform.SetParent(logEnemy.transform);
            DeactivatePhysics();
        }
    }
}
