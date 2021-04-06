using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour
{
    public float Speed = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Plug into log
        if (collision.gameObject.TryGetComponent(out LogEnemy logEnemy))
        {
            // Set parent to rotate with log
            transform.SetParent(logEnemy.transform);
            GetComponent<Rigidbody2D>().Sleep();
        }
        // Hit another knife
        else if (collision.gameObject.TryGetComponent(out Knife knife))
        {
            // Unset parent
            transform.SetParent(null);

            // Make the knife fall
            var rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.WakeUp();
            rigidbody.gravityScale = 1f;

            // Destroy falling knife after 4 seconds
            Destroy(gameObject, 4f);
        }
    }
}
