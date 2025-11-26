using UnityEngine;

public static class ShotAttack 
{
    // Disparo simple: solo una bala con dirección específica
    public static void SimpleShot(Vector2 origin, Vector2 velocity)
    {
        // Obtener una bala del pool
        Bullet bullet = BulletPool.Instance.RequestBullet();

        // Posicionar y aplicar velocidad
        bullet.transform.position = origin;
        bullet.Velocity = velocity;
    }

    // Disparo radial: crea múltiples balas distribuidas en un círculo
    public static void RadialShot(Vector2 origin, Vector2 aimDirection, RadialShotSettings settings)
    {
        // Ángulo entre balas según la cantidad
        float angleBetweenBullets = 360f / settings.NumberofBullets;

        // Aplicar offsets iniciales si existen
        if (settings.AngleOffSet != 0f || settings.PhaseOffset != 0f)
        {
            aimDirection = aimDirection.Rotate(settings.AngleOffSet + (settings.PhaseOffset * angleBetweenBullets));
        }

        // Crear cada bala del disparo
        for (int i = 0; i < settings.NumberofBullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;

            // Aplicar máscara angular si está activada
            if (settings.radialMask && bulletDirectionAngle > settings.maskAngle)
                break;

            // Rotar la dirección base para cada bala
            Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);

            // Crear la bala usando SimpleShot
            SimpleShot(origin, bulletDirection * settings.BulletSpeed);
        }
    }
}
