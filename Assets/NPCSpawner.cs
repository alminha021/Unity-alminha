using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;  // Prefab do NPC
    public FilaTriggerController filaController;  // Referência ao controlador da fila

    void Start()
    {
        SpawnNPCs();  // Chama o método para spawnar NPCs
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < 5; i++)  // Exemplo: cria 5 NPCs
        {
            GameObject npcInstance = Instantiate(npcPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            NPCController npcController = npcInstance.GetComponent<NPCController>();

            if (npcController != null)
            {
                // Atribui um valor aleatório para o NPC (de 1 a 3 por exemplo)
                npcController.valorNPC = Random.Range(1, 4);

                // Registra o NPC no FilaTriggerController
                filaController.RegisterNPC(npcInstance.transform);
                Debug.Log("NPC registered: " + npcController.valorNPC);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Retorne uma posição aleatória dentro da sua área de spawn
        return new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
    }
}
