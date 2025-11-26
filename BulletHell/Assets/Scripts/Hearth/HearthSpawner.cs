using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    // Prefab del corazón que se va a generar
    public GameObject heartPrefab;

    // Impide que el corazón se genere más de una vez
    private bool heartSpawned = false;

    private void OnEnable()
    {
        // Se suscribe al evento del TimeManager que notifica cada vez que cambia la hora o el minuto
        TimeManager.OnTimeChanged += CheckSpawn;
    }

    private void OnDisable()
    {
        // Se desuscribe del evento para evitar errores cuando el objeto esté desactivado
        TimeManager.OnTimeChanged -= CheckSpawn;
    }

    // Función que es llamada automáticamente cada vez que el tiempo cambia
    void CheckSpawn(int hour, int minute)
    {
        // Condición exacta para generar el corazón: a la 1:08
        if (!heartSpawned && hour == 1 && minute == 8)
        {
            // Instancia un corazón en la posición del objeto que contiene este script
            Instantiate(heartPrefab, transform.position, Quaternion.identity);

            // Marcar que ya se creó para no volver a generarlo
            heartSpawned = true;
        }
    }
}
