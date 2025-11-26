using UnityEngine;
using System.Collections;

public class BulletRotator : MonoBehaviour
{
    // Cantidad de grados que rotará cada vez
    public float rotateAmount = 45f;

    // Tiempo que tardará en completar la rotación
    public float rotateDuration = 0.5f;

    // Cada cuántos segundos se ejecuta una rotación
    public float waitTime = 2f;

    private void Start()
    {
        // Iniciar la rutina que rota automáticamente
        StartCoroutine(RotateRoutine());
    }

    // Corrutina que espera, rota y repite para siempre
    IEnumerator RotateRoutine()
    {
        while (true)
        {
            // Esperar el tiempo definido antes de rotar
            yield return new WaitForSeconds(waitTime);

            // Ejecutar rotación suave
            yield return StartCoroutine(SmoothRotate(rotateAmount, rotateDuration));
        }
    }

    // Realiza una rotación suave (interpolada)
    IEnumerator SmoothRotate(float angle, float duration)
    {
        Quaternion startRotation = transform.rotation;                 // Rotación inicial
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 0, angle); // Rotación final deseada

        float t = 0; // Progreso de la animación
        while (t < duration)
        {
            t += Time.deltaTime;
            // Interpolar entre la rotación inicial y final
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t / duration);

            yield return null; // Esperar al siguiente frame
        }
    }
}
