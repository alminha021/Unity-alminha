using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // O jogador
    public float smoothSpeed = 8f;
    public Vector3 offset; // Offset para a posição da câmera

    private float currentAngle; // Armazena o ângulo atual da câmera

    void Start()
    {
        currentAngle = 45f; // Inicializa o ângulo da câmera
    }

    void Update()
    {
        if (target == null) return;

        // Lógica para girar a câmera
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentAngle -= 90; // Gira para a esquerda
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentAngle += 90; // Gira para a direita
        }

        // Calcula a nova posição da câmera com base no ângulo atual
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 desiredPosition = target.position + rotation * offset; // Mantém o jogador no centro
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Faz a câmera olhar para o jogador
        transform.LookAt(target);
    }
}
