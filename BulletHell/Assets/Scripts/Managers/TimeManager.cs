using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
    // Eventos públicos
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    // Nuevo evento para saber la hora exacta
    public static Action<int, int> OnTimeChanged;

        public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    
    // Configuración del tiempo del juego

    [SerializeField] private float minuteToRealTime = 1f;
    private float timer;

    void Start()
    {
        Minute = 0;
        Hour = 0;
        timer = minuteToRealTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Cuando pasa el tiempo correspondiente a 1 minuto
        if (timer <= 0)
        {
            Minute++;

            // Evento solo para aviso de minuto
            OnMinuteChanged?.Invoke();

            // Nuevo evento indicando hora y minuto actual
            OnTimeChanged?.Invoke(Hour, Minute);

            // Si llegó a 60 minutos → suma 1 hora
            if (Minute >= 60)
            {
                Hour++;
                OnHourChanged?.Invoke();
                Minute = 0;

                // Notificar también el cambio de hora
                OnTimeChanged?.Invoke(Hour, Minute);
            }

            // Reiniciar el contador
            timer = minuteToRealTime;
        }
    }
}
