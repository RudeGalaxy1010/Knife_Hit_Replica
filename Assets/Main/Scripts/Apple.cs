using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Apple : MonoBehaviour
{
    public int ScorePoints = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If got hit by knife
        if (collision.gameObject.TryGetComponent(out Knife knife))
        {
            Destroy(gameObject);
        }
    }
}
