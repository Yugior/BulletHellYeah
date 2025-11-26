using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Buscar si el objeto que entró tiene PlayerHealth
        PlayerHealth player = collision.GetComponent<PlayerHealth>();

        if (player != null)
        {
            // Curar al máximo
            player.HealToMax();

            // Destruir el corazón de la escena
            Destroy(gameObject);
        }
    }
}
