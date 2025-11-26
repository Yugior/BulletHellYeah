using UnityEngine;

public class ShootTest : MonoBehaviour
{
    // Tiempo entre disparos automáticos
    [SerializeField] private float _shootColdown;

    // Configuración única de disparo radial
    [SerializeField] private RadialShotSettings _shotSettings;

    private float _shootColdownTimer = 0f;

    private void Update()
    {
        // Reducir el temporizador cada frame
        _shootColdownTimer -= Time.deltaTime;

        // Cuando llegue a 0, dispara un radial simple
        if (_shootColdownTimer <= 0f)
        {
            ShotAttack.RadialShot(transform.position, transform.up, _shotSettings);

            // Reiniciar cooldown
            _shootColdownTimer += _shootColdown;
        }
    }
}
