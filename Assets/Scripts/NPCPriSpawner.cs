using UnityEngine;

public class NPCPriSpawner : MonoBehaviour
{
    public PatientConditionLoader conditionLoader;
    public GameObject[] npcPrefabs;  // Array de diferentes prefabs de NPC
    public FilaTriggerPriCtrl filaControllerPri;  // Referência ao controlador da fila prioritária
    public int npcCount = 500;  // Quantidade inicial de NPCs prioritários para spawnar
    private int spawnedPriNPCs = 0;  // Contador de NPCs prioritários já spawnados
    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();  // Obtém o BoxCollider para a área de spawn
    }

    public void SpawnNPCs()
    {
        // Verifica o número de NPCs prioritários na cena e ajusta o limite se necessário
        if (spawnedPriNPCs < npcCount)
        {
            // Spawna NPCs prioritários até o número total desejado
            for (int i = spawnedPriNPCs; i < npcCount; i++)
            {
                // Debug.Log("Spawning Priority NPC " + i);
                GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];  // Escolhe um NPC aleatório
                Vector3 spawnPosition = GetRandomPositionWithinArea();  // Obtém uma posição aleatória dentro da área de spawn
                GameObject npcInstance = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);  // Cria o NPC


                NPCCtrlPri npcController = npcInstance.GetComponent<NPCCtrlPri>();  // Obtém o controlador do NPC
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
                    filaControllerPri.RegisterNPC(npcInstance.transform);
                    // Debug.Log("NPC registered: " + npcController.valorNPC);
                }

                spawnedPriNPCs++;  // Incrementa o contador de NPCs prioritários spawnados
            }
        }
        else
        {
            // Se o número de NPCs prioritários na cena for menor que o limite, aumenta o limite e tenta novamente
            npcCount += 2;  // Incrementa o limite de NPCs prioritários para 2
            Debug.Log("Número de NPCs prioritários na cena é baixo. Aumentando o limite de spawn para " + npcCount);



            // Chama o método de spawn novamente, caso ainda seja necessário
            SpawnNPCs();
            // Chama diretamente a função de mover os NPCs para a fila prioritária
            filaControllerPri.MoveNPCsToFila();
            Debug.Log("WAVE PRIORITARIA SE APORXIMA");
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
