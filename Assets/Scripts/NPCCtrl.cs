using UnityEngine;
using UnityEngine.AI;

public class NPCCtrl : MonoBehaviour
{
    public int valorNPC;
    private NavMeshAgent agent;
    private bool hasReachedRoom = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToQueue(Transform target)
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
            Debug.Log("Moving NPC to queue point at: " + target.position);
        }
    }

    public void MoveToDestination(Transform destination)
    {
        if (agent != null && destination != null)
        {
            agent.SetDestination(destination.position);
            hasReachedRoom = true;  // Marcamos que o NPC está indo para uma das rooms finais
            Invoke("DestroyNPC", 15f);  // Começa a contagem para destruir o NPC após 15 segundos
        }
    }

    private void DestroyNPC()
    {
        if (hasReachedRoom)
        {
            Destroy(gameObject);
            Debug.Log("NPC destroyed after reaching the room");
        }
    }
}