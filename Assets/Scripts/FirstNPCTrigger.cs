using UnityEngine;

public class FirstNPCTrigger : MonoBehaviour
{
    public FilaTriggerController filaController;  // Referência ao controlador da fila
    public Transform destino1;    // Destino para NPCs com valor 1
    public Transform destino2;    // Destino para NPCs com valor 2
    public Transform destino3;    // Destino para NPCs com valor 3

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Verifica se o objeto é o jogador
        {
            Debug.Log("Player triggered first NPC movement!");
            MoveFirstNPC();
        }
    }

    private void MoveFirstNPC()
    {
        Transform firstNPC = filaController.GetFirstNPC();  // Obtém o primeiro NPC da fila
        if (firstNPC != null)
        {
            NPCController npcController = firstNPC.GetComponent<NPCController>();
            if (npcController != null)
            {
                // Verifica o valor do NPC e move-o para o destino correspondente
                switch (npcController.valorNPC)
                {
                    case 1:
                        npcController.MoveToQueue(destino1);
                        break;
                    case 2:
                        npcController.MoveToQueue(destino2);
                        break;
                    case 3:
                        npcController.MoveToQueue(destino3);
                        break;
                }

                // Remove o primeiro NPC da fila e reorganiza
                filaController.RemoveFirstNPCAndUpdateQueue();
            }
        }
    }
}
