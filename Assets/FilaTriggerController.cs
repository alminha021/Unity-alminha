using UnityEngine;
using System.Collections.Generic;

public class FilaTriggerController : MonoBehaviour
{
    public Transform[] filaPoints;  // Array de pontos da fila
    private Queue<Transform> filaNPCs = new Queue<Transform>();  // Fila dos NPCs

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered: " + other.name);  // Verifica qual objeto entrou

        if (other.CompareTag("Player"))  // Verifica se o objeto que entrou é o jogador
        {
            Debug.Log("Player entered the trigger!"); // Mensagem de depuração
            MoveNPCsToFila();  // Mover NPCs para a fila
        }
    }

    public void RegisterNPC(Transform npc)
    {
        filaNPCs.Enqueue(npc);  // Adiciona o NPC à fila
    }

    private void MoveNPCsToFila()
    {
        int i = 0;
        while (filaNPCs.Count > 0 && i < filaPoints.Length)
        {
            Transform npc = filaNPCs.Dequeue();  // Remove o NPC da fila
            Debug.Log($"Moving NPC: {npc.name} to point: {filaPoints[i].name}");  // Mensagem de depuração
            NPCController npcController = npc.GetComponent<NPCController>();
            if (npcController != null)
            {
                npcController.MoveToQueue(filaPoints[i]);  // Mover o NPC para o ponto da fila
            }
            i++;
        }
    }
}
