using UnityEngine;

public static class Vector2Extensions 
{
    // Extensión que rota un Vector2 usando un ángulo en grados
    public static Vector2 Rotate(this Vector2 originalVector, float rotateAngleinDegrees)
    {
        // Crear rotación usando Quaternion (eje Z = 2D)
        Quaternion rotation = Quaternion.AngleAxis(rotateAngleinDegrees, Vector3.forward);

        // Multiplicar para aplicar la rotación
        return rotation * originalVector;
    }
}
