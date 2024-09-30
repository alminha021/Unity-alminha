using UnityEngine;

public class NPCController : MonoBehaviour
{
    public int valorNPC;  // Valor do NPC que determina o destino
    private Vector3 targetPosition;  // Posição alvo para o movimento
    private bool isMoving;  // Indica se o NPC está se movendo

    private void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    public void MoveToQueue(Transform target)
    {
        targetPosition = target.position;  // Define a posição alvo
        isMoving = true;  // Inicia o movimento
    }

    private void MoveToTarget()
    {
        // Move o NPC em direção ao ponto alvo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 3f);
        
        // Verifica se chegou ao alvo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;  // Para o movimento ao chegar
        }
    }

    public void LeaveQueue()
    {
        // Aqui você pode adicionar a lógica do que o NPC deve fazer ao deixar a fila, se necessário
    }
}
