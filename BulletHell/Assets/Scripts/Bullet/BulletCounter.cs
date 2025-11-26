using UnityEngine;
using TMPro;

public class BulletCounter : MonoBehaviour
{
    // Variable estática que almacena cuántas balas están activas en la escena.
    // Es modificada por cada bala cuando se activa o desactiva.
    public static int ActiveBullets = 0;

    // Referencia al texto UI (TextMeshPro) donde se mostrará el contador.
    public TextMeshProUGUI counterText;

    void Update()
    {
        // Actualizar el texto cada frame para mostrar el número actual de balas activas.
        // Se usa interpolación de strings para una mejor lectura.
        counterText.text = $"Bullets: {ActiveBullets}";
    }
}
