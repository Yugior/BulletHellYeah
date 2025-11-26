using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    // Referencia al texto donde se mostrará la hora
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        // Se suscribe a los eventos para actualizar el reloj cuando cambien los minutos u horas
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        // Se desuscribe de los eventos para evitar errores cuando el objeto esté desactivado
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    // Actualiza el texto del reloj
    void UpdateTime()
    {
        // Formato HH:MM con ceros a la izquierda (por ejemplo 01:05)
        timeText.text = $"{TimeManager.Hour.ToString("00")}:{TimeManager.Minute.ToString("00")}";
    }
}
