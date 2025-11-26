using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 5;                 // Vida máxima del jugador
    public float InvulnerabilityTime = 1.0f;  // Tiempo en el que no recibe daño tras ser golpeado
    public float FlashSpeed = 0.1f;           // Velocidad del parpadeo al ser invulnerable

    private int currentHealth;
    private bool isInvulnerable = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Evento para notificar cambios de vida a la UI
    public delegate void HealthChanged();
    public event HealthChanged onHealthChanged;

    private void Start()
    {
        currentHealth = MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        // Actualizamos la UI al comenzar
        onHealthChanged?.Invoke();
    }

    public void TakeDamage(int dmg)
    {
        // No recibe daño si está en invulnerabilidad
        if (isInvulnerable)
            return;

        currentHealth -= dmg;
        Debug.Log("Player HP: " + currentHealth);

        // Avisamos que la vida cambió
        onHealthChanged?.Invoke();

        // Si la vida llega a 0 → Reinicia nivel
        if (currentHealth <= 0)
        {
            Debug.Log("GAME OVER");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Activamos invulnerabilidad temporal
        StartCoroutine(StartInvulnerability());
    }

    private IEnumerator StartInvulnerability()
    {
        isInvulnerable = true;

        float timePassed = 0f;
        bool flashState = false;

        // Parpadeo mientras dura la invulnerabilidad
        while (timePassed < InvulnerabilityTime)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = flashState ? Color.red : originalColor;
                flashState = !flashState;
            }

            yield return new WaitForSeconds(FlashSpeed);
            timePassed += FlashSpeed;
        }

        // Restablecemos color original
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        isInvulnerable = false;
    }

    // Propiedad para leer la vida actual
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    // Restaura la vida al máximo
    public void HealToMax()
    {
        currentHealth = MaxHealth;
        Debug.Log("Vida restaurada al máximo");

        onHealthChanged?.Invoke();
    }
}
