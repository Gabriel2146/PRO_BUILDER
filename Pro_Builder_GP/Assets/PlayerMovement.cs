
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;              // Velocidad de movimiento
    public float sensitivity = 2f;        // Sensibilidad de la rotaci�n con el mouse
    public float jumpHeight = 1.0f;       // Altura del salto
    public float gravity = -9.81f;        // Valor de gravedad
    private float xRotation = 0f;         // Control de la rotaci�n en el eje X (vertical)
    private Vector3 playerVelocity;       // Velocidad del jugador

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Obtener el componente CharacterController
        Cursor.lockState = CursorLockMode.Locked;         // Bloquea el cursor en el centro de la pantalla
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        // Chequear si el jugador est� en el suelo y resetear la velocidad en Y si es necesario
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Obtener entradas de teclado
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calcular direcci�n de movimiento en el plano horizontal
        Vector3 move = transform.right * x + transform.forward * z;

        // Aplicar movimiento al CharacterController
        controller.Move(move * speed * Time.deltaTime);

        // Realizar el salto cuando se presiona Espacio y el personaje est� en el suelo
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplicar la gravedad
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        // Obtener movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Aplicar rotaci�n horizontal al jugador
        transform.Rotate(Vector3.up * mouseX);

        // Aplicar rotaci�n vertical de la c�mara con l�mite
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
