using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;          // Velocidad normal
    public float focusedSpeed = 3f;       // Velocidad precisa al mantener Shift
    public bool useFocusedMovement = true;

    [Header("Bounds")]
    public float minX = -8f;              // Límites del área de movimiento
    public float maxX =  8f;
    public float minY = -4.5f;
    public float maxY =  4.5f;

    private Vector2 movement;

    void Update()
    {
        HandleMovement();
        ClampPosition();
    }

    void HandleMovement()
    {
        float speed = moveSpeed;

        // Si mantiene Shift, reducimos velocidad para precisión
        if (useFocusedMovement && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            speed = focusedSpeed;
        }

        // Entrada del jugador
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical   = Input.GetAxisRaw("Vertical");

        // Normalizamos para evitar que en diagonal sea más rápido
        movement = new Vector2(horizontal, vertical).normalized;

        // Movemos al jugador
        transform.Translate(movement * speed * Time.deltaTime);
    }

    // Evita que el jugador salga de la pantalla
    void ClampPosition()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
