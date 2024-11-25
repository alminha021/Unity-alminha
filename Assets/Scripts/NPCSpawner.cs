using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public PatientConditionLoader conditionLoader;
    public GameObject[] npcPrefabs;  // Array de diferentes prefabs de NPC
    public FilaTriggerCtrl filaController;  // Referência ao controlador da fila
    public int npcCount = 5;  // Quantidade de NPCs para spawnar
    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();  // Obtém o BoxCollider para a área de spawn
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("spawn");
            GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
            Vector3 spawnPosition = GetRandomPositionWithinArea();
            GameObject npcInstance = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("cheguei");
            NPCCtrl npcController = npcInstance.GetComponent<NPCCtrl>();
            Debug.Log("cheguei2");
            if (npcController != null)
            {
                Debug.Log("cheguei3");
                npcController.valorNPC = Random.Range(1, 4);

                npcController.patientCondition = randomPatientCondition();

                // Registra o NPC no FilaTriggerController
                filaController.RegisterNPC(npcInstance.transform);
                Debug.Log("NPC registered: " + npcController.valorNPC + " with " + npcController.patientCondition.disease);
            }
            Debug.Log("fim");
        }
    }

    private PatientCondition randomPatientCondition()
    {
        return conditionLoader.patientConditions[Random.Range(0, conditionLoader.patientConditions.Length)];
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
