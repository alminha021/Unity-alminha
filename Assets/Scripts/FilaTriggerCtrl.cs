using UnityEngine;

public class FilaTriggerCtrl : MonoBehaviour
{
    public Transform[] filaPoints; // Pontos para onde os NPCs devem ir
    private Transform[] npcInFila; // NPCs que estão na fila
    private int currentNPCIndex = 0; // Índice atual na fila

    private void Start()
    {
        npcInFila = new Transform[filaPoints.Length];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoveNPCsToFila(); // Move os NPCs automaticamente quando o player entra no Trigger
        }
    }

    public void RegisterNPC(Transform npc)
    {
        if (currentNPCIndex < filaPoints.Length)
        {
            npcInFila[currentNPCIndex] = npc;
            currentNPCIndex++;
        }
    }

    public void MoveNPCsToFila()
    {
        for (int i = 0; i < currentNPCIndex && i < filaPoints.Length; i++)
        {
            if (npcInFila[i] != null)
            {
                NPCCtrl npcController = npcInFila[i].GetComponent<NPCCtrl>();
                if (npcController != null)
                {
                    npcController.MoveToQueue(filaPoints[i]); // Move cada NPC para seu ponto na fila
                }
            }
        }
    }

    public void RemoveFirstNPCAndUpdateQueue()
    {
        if (currentNPCIndex > 0)
        {
            npcInFila[0].GetComponent<NPCCtrl>().MoveToDestination(null);
            for (int i = 1; i < currentNPCIndex; i++)
            {
                npcInFila[i - 1] = npcInFila[i];
            }
            npcInFila[currentNPCIndex - 1] = null;
            currentNPCIndex--;
        }
    }

    public Transform GetFirstNPC()
    {
        return currentNPCIndex > 0 ? npcInFila[0] : null;
    }

    // Método para verificar se há NPCs na fila
    public bool HasNPCs()
    {
        return currentNPCIndex > 0;
    }
}
