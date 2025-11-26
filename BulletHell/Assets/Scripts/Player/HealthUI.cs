using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth player;              // Referencia al script de vida del jugador
    public TextMeshProUGUI healthText;       // Referencia al texto UI donde se mostrará la vida

    private void Start()
    {
        // Nos suscribimos al evento que se ejecuta cuando la vida del jugador cambia
        player.onHealthChanged += UpdateHealthUI;

        // Actualizamos la UI al inicio para mostrar la vida inicial
        UpdateHealthUI();
    }

    // Actualiza el texto de la UI con la vida actual del jugador
    private void UpdateHealthUI()
    {
        if (healthText != null && player != null)
        {
            healthText.text = "HP: " + player.CurrentHealth;
        }
    }

    private void OnDestroy()
    {
        // Evitamos errores: nos desuscribimos del evento al destruir la UI
        if (player != null)
            player.onHealthChanged -= UpdateHealthUI;
    }
}
