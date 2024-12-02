using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // Referência para a câmera principal
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        // Garante que o sprite esteja sempre de frente para a câmera
        if (mainCamera != null)
        {
            transform.forward = mainCamera.transform.forward;
        }
    }
}
