using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialShotWeapon : MonoBehaviour
{
    // Patrón radial que será ejecutado por esta arma
    [SerializeField] private RadialShotPattern _shotPattern;

    // Evita que el patrón se ejecute varias veces a la vez
    private bool _onShootPattern = false;

    private void Update()
    {
        // Si ya está ejecutando un patrón, no volver a llamar la corrutina
        if (_onShootPattern)
            return;

        // Ejecutar el patrón completo
        StartCoroutine(ExcecuteRadialShotPattern(_shotPattern));
    }

    // Corrutina que ejecuta el patrón radial completo
    private IEnumerator ExcecuteRadialShotPattern(RadialShotPattern pattern)
    {
        _onShootPattern = true;

        int lap = 0;                         // Repetición actual
        Vector2 aimDirection = transform.up; // Dirección inicial de disparo
        Vector2 center = transform.position; // Centro del disparo

        // Espera inicial antes de arrancar
        yield return new WaitForSeconds(pattern.StartWait);

        // Ejecutar todas las repeticiones del patrón
        while (lap < pattern.Repetitions)
        {
            // Aplicar offset entre repeticiones si existe
            if (lap > 0 && pattern.AngleOffsetBetweenRepetitions != 0f)
                aimDirection = aimDirection.Rotate(pattern.AngleOffsetBetweenRepetitions);

            // Ejecutar cada grupo interno del patrón
            for (int i = 0; i < pattern.PatternSettings.Length; i++)
            {
                // Dispara una ráfaga radial usando las configuraciones del índice actual
                ShotAttack.RadialShot(center, aimDirection, pattern.PatternSettings[i]);

                // Espera antes del siguiente sub-shot para controlar cadencia
                yield return new WaitForSeconds(pattern.PatternSettings[i].CooldownAfterShot);
            }

            lap++; // Siguiente repetición
        }

        // Espera final al terminar todo el patrón
        yield return new WaitForSeconds(pattern.EndWait);

        // Permitir que el patrón pueda iniciar otra vez
        _onShootPattern = false;
    }
}
