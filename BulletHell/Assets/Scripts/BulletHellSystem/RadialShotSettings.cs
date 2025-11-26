using UnityEngine;

[System.Serializable]
public class RadialShotSettings
{
    [Header("Base Settings")]
    // Cuántas balas se generan en el disparo radial
    public int NumberofBullets = 5;

    // Velocidad que usarán las balas en la previsualización
    public float BulletSpeed = 10f;

    // Tiempo antes del siguiente sub-patrón
    public float CooldownAfterShot;

    [Header("OffSets")]
    // Desplazamiento de fase (0 a 1), recorre el patrón
    [Range(-1f,1f)] public float PhaseOffset = 0f;

    // Gira el patrón completamente por un valor definido
    [Range(-180, 180)] public float AngleOffSet = 0f;

    [Header("Mask")] 
    // Habilita limitar la cantidad de balas por ángulo
    public bool radialMask;

    // Ángulo máximo donde se dibujarán balas
    [Range(0f,360f)] public float maskAngle = 360f;
}
