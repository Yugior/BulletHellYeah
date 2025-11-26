using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Tiempo máximo que la bala puede existir antes de desactivarse automáticamente
    private const float MAX_TIME_LIFE = 5f;

    // Contador interno para medir cuánto tiempo lleva activa la bala
    private float _lifetime = 0;

    // Velocidad y dirección de la bala (definido externamente)
    public Vector2 Velocity;

    private void Update()
    {
        // Mover la bala cada frame según su velocidad
        transform.position += (Vector3)Velocity * Time.deltaTime;

        // Incrementar el tiempo de vida
        _lifetime += Time.deltaTime;

        // Si supera el tiempo máximo permitido, desactivar la bala (volver al pool)
        if (_lifetime > MAX_TIME_LIFE)
        {
            Disable();
        }
    }

    // Desactiva la bala y reinicia su tiempo de vida
    private void Disable()
    {
        _lifetime = 0f;                 // Reiniciar tiempo de vida
        gameObject.SetActive(false);    // Apagar el objeto (pooling)
    }

    // Se ejecuta cada vez que la bala se activa desde el pool
    private void OnEnable()
    {
        BulletCounter.ActiveBullets++;  // Incrementa el contador global de balas activas
    }

    // Se ejecuta cada vez que la bala se desactiva
    private void OnDisable()
    {
        BulletCounter.ActiveBullets--;  // Reduce el contador global
    }

    // Detecta colisiones usando trigger (para balas suele ser lo ideal)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si golpeó al jugador
        if (other.CompareTag("Player"))
        {
            // Intentar obtener el componente de salud del jugador
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            // Si existe, aplicar daño
            if (player != null)
            {
                player.TakeDamage(1);
            }

            // No destruir — desactivar para que funcione el pooling
            Disable();
        }
    }
}
