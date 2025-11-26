using UnityEngine;

public class AudioStopper : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Suscribirse al evento del cambio de minuto
        TimeManager.OnMinuteChanged += CheckTime;
    }

    private void OnDestroy()
    {
        TimeManager.OnMinuteChanged -= CheckTime;
    }

    private void CheckTime()
    {
        // Si llega al minuto 38 del hour 2
        if (TimeManager.Hour == 2 && TimeManager.Minute == 38)
        {
            // Apagar o detener audio
            audioSource.Stop();          // Detiene la música
            // audioSource.enabled = false;  // Alternativa: desactivar el componente
        }
    }
}
