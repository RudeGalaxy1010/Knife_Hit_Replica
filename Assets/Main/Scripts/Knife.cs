using System.Collections;
using System.Collections.Generic;
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
            transform.SetParent(logEnemy.transform);
            // Disable rigidbody
            GetComponent<Rigidbody2D>().Sleep();
        }
        // Hit another knife
        else if (collision.gameObject.TryGetComponent(out Knife knife))
        {
            Debug.Log("You're lose");
            transform.SetParent(null);
            // Enable gravity for this knife
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
