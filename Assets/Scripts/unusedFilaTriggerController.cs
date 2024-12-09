using UnityEngine;

public class FilaTriggerController : MonoBehaviour
{
    public Transform[] filaPoints;  // Array de pontos da fila
    private Transform[] npcInFila;  // Array para armazenar NPCs na fila
    private int currentNPCIndex = 0;  // Índice do NPC atual na fila

    private void Start()
    {
        npcInFila = new Transform[filaPoints.Length];  // Inicializa o array de NPCs
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Verifica se o objeto que ativou o trigger é o jogador
        {
            Debug.Log("Player triggered the FilaTriggerController!");
            MoveNPCsToFila();  // Move os NPCs para os pontos da fila
        }
    }

    public void RegisterNPC(Transform npc)
    {
        if (currentNPCIndex < filaPoints.Length)
        {
            npcInFila[currentNPCIndex] = npc;  // Adiciona o NPC ao array
            currentNPCIndex++;
        }
    }

    private void MoveNPCsToFila()
    {
        for (int i = 0; i < currentNPCIndex && i < filaPoints.Length; i++)
        {
            if (npcInFila[i] != null)
            {
                NPCController npcController = npcInFila[i].GetComponent<NPCController>();
                if (npcController != null)
                {
                    npcController.MoveToQueue(filaPoints[i]);  // Move o NPC para o ponto correspondente
                }
            }
        }
    }

    // Remove o primeiro NPC da fila e reorganiza
    public void RemoveFirstNPCAndUpdateQueue()
    {
        if (currentNPCIndex > 0)
        {
            npcInFila[0].GetComponent<NPCController>().MoveToDestination(null);  // Limpa o destino do primeiro NPC
            // Reorganiza os NPCs na fila
            for (int i = 1; i < currentNPCIndex; i++)
            {
                npcInFila[i - 1] = npcInFila[i];
            }
            npcInFila[currentNPCIndex - 1] = null;  // Limpa a última posição
            currentNPCIndex--;
        }
    }

    // Retorna o primeiro NPC da fila
    public Transform GetFirstNPC()
    {
        if (currentNPCIndex > 0)
        {
            return npcInFila[0];  // Retorna o primeiro NPC
        }
        return null;
    }
}