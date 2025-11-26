using UnityEngine;

public class RadialShotPatternVisualizer : MonoBehaviour
{
   // Patrón radial que se dibujará en escena
   [SerializeField] private RadialShotPattern _pattern;

   // Tamaño del gizmo de las balas (solo visual)
   [SerializeField] private float _radius;

   // Color elegido (aunque no se usa todavía en Gizmos)
   [SerializeField] public Color _color;

   // Tiempo de simulación para previsualizar el patrón
   [SerializeField, Range(0f,5f)] private float _testTime;

    private void OnDrawGizmos()
    {
        // No dibujar si no hay patrón asignado
        if(_pattern == null)
            return;

        // Color del gizmo
        Gizmos.color = Color.red;
        
        int lap = 0;                         // Repetición actual
        Vector2 aimDirection = transform.up; // Dirección base (arriba)
        Vector2 center = transform.position; // Centro del disparo

        float timer = _testTime;             // Tiempo disponible para simular

        // Ejecutar simulación de disparos para visualizarla
        while (timer > 0f && lap < _pattern.Repetitions)
        {
            // Si hay offset entre repeticiones, ajustarlo
            if (lap > 0 && _pattern.AngleOffsetBetweenRepetitions != 0f)
                aimDirection = aimDirection.Rotate(_pattern.AngleOffsetBetweenRepetitions);

            // Ejecutar cada grupo/configuración del patrón
            for (int i = 0; i < _pattern.PatternSettings.Length; i++)
            {
                if (timer < 0f)
                    break;

                // Dibujar las balas de esta configuración
                DrawnRadialShot(_pattern.PatternSettings[i], timer, aimDirection);

                // Avanzar el tiempo según cooldown
                timer -= _pattern.PatternSettings[i].CooldownAfterShot;
            }

            lap++; // Siguiente repetición
        }
    }

    // Dibuja un conjunto radial según las configuraciones
    private void DrawnRadialShot(RadialShotSettings settings, float lifeTime, Vector2 aimDirection)
    {
        // Ángulo entre cada bala
        float angleBetweenBullets = 360f / settings.NumberofBullets;

        // Aplicar offsets de fase o ángulo si existen
        if (settings.PhaseOffset != 0f || settings.AngleOffSet != 0)
            aimDirection = aimDirection.Rotate(settings.AngleOffSet + (settings.PhaseOffset * angleBetweenBullets));

        // Crear cada bala en su posición simulada
        for (int i = 0; i < settings.NumberofBullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;

            // Si hay una máscara angular, limitar la creación de balas
            if (settings.radialMask && bulletDirectionAngle > settings.maskAngle)
                break;

            // Dirección final de la bala
            Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);

            // La posición proyectada es: centro + dirección * velocidad * tiempo
            Vector2 bulletPosition =
                (Vector2) transform.position + (bulletDirection * settings.BulletSpeed * lifeTime);

            // Dibujar la bala
            Gizmos.DrawSphere(bulletPosition, _radius);
        }
    }
}
