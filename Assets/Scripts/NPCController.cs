using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public int valorNPC;  // Valor que define o destino do NPC
    public PatientCondition patientCondition;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToQueue(Transform target)
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void MoveToDestination(Transform destino)
    {
        if (agent != null && destino != null)
        {
            agent.SetDestination(destino.position);
        }
    }
}