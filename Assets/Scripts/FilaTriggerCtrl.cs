using UnityEngine;

public class FilaTriggerCtrl : MonoBehaviour
{
    public Transform[] filaPoints;  // Pontos para onde os NPCs devem ir
    private Transform[] npcInFila;  // NPCs que estão na fila
    private int currentNPCIndex = 0;  // Índice atual na fila
    private bool isPlayerNear = false; // Verifica se o player está próximo

    private void Start()
    {
        npcInFila = new Transform[filaPoints.Length];
    }

    private void Update()
    {
        // Verifica se o player está próximo e se a tecla "T" foi pressionada
        if (isPlayerNear && Input.GetKeyDown(KeyCode.T))
        {
            MoveNPCsToFila();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true; // Marca o player como próximo ao trigger
            Debug.Log("Pressione 'T' para mover os NPCs para a fila.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false; // Marca o player como distante ao sair do trigger
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
                    Debug.Log("Movendo NPC " + i + " para o ponto de fila");
                    npcController.MoveToQueue(filaPoints[i]);
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
