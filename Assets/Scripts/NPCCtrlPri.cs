using UnityEngine;
using UnityEngine.AI;

public class NPCCtrlPri : MonoBehaviour
{
    public int valorNPC; // O valor do NPC que vai definir a resposta dele
    private NavMeshAgent agent;
    private Animator animator;
    public float rotationSpeed = 5f;
    private bool hasReachedRoom = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent != null && agent.velocity.sqrMagnitude > 0.01f)
        {
            // Rotaciona para olhar na direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Define a animação de movimento
        animator.SetBool("isRunning", agent.velocity.sqrMagnitude > 0.01f);
    }

    public void MoveToQueue(Transform target)
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
            Debug.Log("Moving Priority NPC to queue point at: " + target.position);
        }
    }

    public void MoveToDestination(Transform destination)
    {
        if (agent != null && destination != null)
        {
            agent.SetDestination(destination.position);
            hasReachedRoom = true;
            //Invoke("DestroyNPC", 15f);
        }
    }

    private void DestroyNPC()
    {
        if (hasReachedRoom)
        {
            //Destroy(gameObject);
            Debug.Log("Priority NPC destroyed debugprictrl");
        }
    }
}
