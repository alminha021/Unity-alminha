using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public PatientConditionLoader conditionLoader;  // Referência para carregar as condições dos pacientes
    public GameObject[] npcPrefabs;  // Array de diferentes prefabs de NPC
    public FilaTriggerCtrl filaController;  // Referência ao controlador da fila regular
    public int npcCount = 500;  // Quantidade inicial de NPCs para spawnar
    private int spawnedNPCs = 0;  // Contador de NPCs já spawnados
    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();  // Obtém o BoxCollider para a área de spawn
    }

    public void SpawnNPCs()
    {
        // Verifica o número de NPCs na cena e ajusta o limite se necessário
        if (spawnedNPCs < npcCount)
        {
            // Spawna NPCs até atingir o número total desejado
            for (int i = spawnedNPCs; i < npcCount; i++)
            {
                Debug.Log("Spawning NPC " + i);
                GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];  // Escolhe um NPC aleatório
                Vector3 spawnPosition = GetRandomPositionWithinArea();  // Obtém uma posição aleatória dentro da área de spawn
                GameObject npcInstance = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);  // Cria o NPC

                NPCCtrl npcController = npcInstance.GetComponent<NPCCtrl>();  // Obtém o controlador do NPC
                if (npcController != null)
                {
                    //npcController.valorNPC = Random.Range(1, 4);  // Atribui um valor aleatório ao NPC
                    int randomIndex = Random.Range(0, conditionLoader.patientConditions.Length);
                    npcController.patientCondition = conditionLoader.patientConditions[randomIndex];

                    if (npcController.patientCondition.tratamento == "Analgésico")
                    {
                        npcController.valorNPC = 1;
                    } else if (npcController.patientCondition.tratamento == "Antibiótico")
                    {
                        npcController.valorNPC = 2;
                    } else if (npcController.patientCondition.tratamento == "Vacina")
                    {
                        npcController.valorNPC = 3;
                    }

                    // Registra o NPC na fila
                    filaController.RegisterNPC(npcInstance.transform);
                    Debug.Log("NPC registered: " + npcController.valorNPC);
                }

                spawnedNPCs++;  // Incrementa o contador de NPCs spawnados
            }
        }
        else
        {
            // Se o número de NPCs na cena for menor que o limite, aumenta o limite e tenta novamente
            npcCount += 6;  // Incrementa o limite de NPCs para 6
            Debug.Log("Número de NPCs na cena é baixo. Aumentando o limite de spawn para " + npcCount);

            // Chama diretamente a função de mover os NPCs para a fila


            // Chama o método de spawn novamente, caso ainda seja necessário
            SpawnNPCs();
            Debug.Log("WAVE REGULAR SE APORXIMA");
            filaController.MoveNPCsToFila();
        }
    }

    private Vector3 GetRandomPositionWithinArea()
    {
        // Obtém uma posição aleatória dentro da área definida pelo BoxCollider
        Vector3 basePosition = spawnArea.transform.position;
        Vector3 size = spawnArea.size;

        float randomX = Random.Range(-size.x / 2, size.x / 2);
        float randomZ = Random.Range(-size.z / 2, size.z / 2);

        return new Vector3(basePosition.x + randomX, basePosition.y, basePosition.z + randomZ);
    }
}
