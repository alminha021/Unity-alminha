using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] npcPrefabs;  // Array de diferentes prefabs de NPC
    public FilaTriggerController filaController;  // Referência ao controlador da fila
    public int npcCount = 5;  // Quantidade de NPCs para spawnar

    void Start()
    {
        SpawnNPCs();  // Chama o método para spawnar NPCs
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)  // Exemplo: cria uma quantidade de NPCs com base em npcCount
        {
            // Seleciona um prefab aleatório da lista de npcPrefabs
            GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];

            // Instancia o NPC na cena
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
        // Retorna uma posição aleatória dentro da sua área de spawn
        return new Vector3(Random.Range(-10f, -5f), 0.42f, -5f);
    }
}
