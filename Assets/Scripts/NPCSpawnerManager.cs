using UnityEngine;

public class NPCSpawnerManager : MonoBehaviour
{
    public NPCSpawner npcSpawner; // Referência ao NPCSpawner (regular)
    public NPCPriSpawner npcPriSpawner; // Referência ao NPCPriSpawner (prioritário)
    public float checkInterval = 5f; // Intervalo para checar se há NPCs no mapa
    private float nextCheckTime = 0f;

    void Update()
    {
        // Verifica periodicamente se há NPCs no mapa
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            CheckAndRespawnNPCs();
        }
    }

    // Verifica se há NPCs na cena e spawn novos se necessário
    void CheckAndRespawnNPCs()
    {
        if (!AnyNPCsInScene())  // Verifica se há NPCs no mapa
        {
            Debug.Log("Não há NPCs na cena, spawnando NPCs...");
            npcSpawner.SpawnNPCs();  // Spawna NPCs regulares
            npcPriSpawner.SpawnNPCs();  // Spawna NPCs prioritários
        }
    }

    // Método para verificar se há NPCs na cena
    bool AnyNPCsInScene()
    {
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("NPC");
        return npcObjects.Length > 0;  // Retorna true se houver NPCs
    }
}
