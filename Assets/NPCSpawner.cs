using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;  // O prefab do NPC a ser instanciado
    public int npcCount = 5;  // Quantidade de NPCs a serem spawnados
    public Transform[] spawnPoints;  // Pontos onde os NPCs serão spawnados

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)
        {
            // Selecionar um ponto de spawn aleatório
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawnar o NPC no ponto selecionado
            Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
