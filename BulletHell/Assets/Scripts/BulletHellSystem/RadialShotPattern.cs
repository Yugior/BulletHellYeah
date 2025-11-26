using UnityEngine;

[CreateAssetMenu(menuName = "BulletHell System/Radial Shot Pattern")]
public class RadialShotPattern : ScriptableObject
{
    // Cuántas veces completo el patrón será repetido
    public int Repetitions;

    // Ángulo que se rota entre repetición y repetición del patrón principal
    [Range(-180,180)] public float AngleOffsetBetweenRepetitions = 0f;

    // Tiempo al inicio antes de comenzar a disparar
    public float StartWait = 0f;

    // Tiempo al terminar de ejecutar todos los patrones
    public float EndWait = 0f;

    // Lista de configuraciones que componen el patrón radial
    // Cada elemento tiene #balas, velocidad, offsets, máscaras, etc.
    public RadialShotSettings[] PatternSettings;
}
