using UnityEngine;

public class NPCPriSpawner : MonoBehaviour
{
    public GameObject[] npcPrefabs;  // Array de diferentes prefabs de NPC
    public FilaTriggerPriCtrl filaControllerPri;  // Referência ao controlador da fila prioritária
    public int npcCount = 5;  // Quantidade de NPCs para spawnar
    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();  // Obtém o BoxCollider para a área de spawn
        
        // Verificar se a filaControllerPri está atribuída
        if (filaControllerPri == null)
        {
            Debug.LogError("filaControllerPri não está atribuída no NPCPriSpawner!");
            return; // Se não estiver atribuída, não continua a execução
        }

        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)  // Spawn de NPCs prioritários
        {
            Debug.Log("Spawning Priority NPC " + i);
            GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];  // Seleção aleatória do prefab
            Vector3 spawnPosition = GetRandomPositionWithinArea();  // Posição de spawn aleatória
            GameObject npcInstance = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);  // Instancia o NPC

            NPCCtrlPri npcController = npcInstance.GetComponent<NPCCtrlPri>();  // Obtém o controlador do NPC
            if (npcController != null)
            {
                npcController.valorNPC = Random.Range(1, 4);  // Atribui um valor aleatório para o NPC
                filaControllerPri.RegisterNPC(npcInstance.transform);  // Registra o NPC na fila prioritária
                Debug.Log("Priority NPC registered with value: " + npcController.valorNPC);
            }
        }
    }

    private Vector3 GetRandomPositionWithinArea()
    {
        Vector3 basePosition = spawnArea.transform.position;
        Vector3 size = spawnArea.size;

        float randomX = Random.Range(-size.x / 2, size.x / 2);
        float randomZ = Random.Range(-size.z / 2, size.z / 2);

        return new Vector3(basePosition.x + randomX, basePosition.y, basePosition.z + randomZ);
    }
}
