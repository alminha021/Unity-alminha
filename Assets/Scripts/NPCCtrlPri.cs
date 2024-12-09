using UnityEngine;
using UnityEngine.AI;

public class NPCCtrlPri : MonoBehaviour
{
    public int valorNPC; // O valor do NPC que vai definir a resposta dele
    private NavMeshAgent agent;
    private Animator animator;
    public float rotationSpeed = 5f;
    private bool hasReachedRoom = false;
    private bool isDestroyed = false;
    public PatientCondition patientCondition;

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

        // Verifica se o NPC chegou ao destino e deve ser destruído
        if (hasReachedRoom && !isDestroyed)
        {
            Invoke("OnNPCDestroyedOrMoved", 15f);
            isDestroyed = true; // Garantir que a verificação só ocorra uma vez
        }
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
            // Marcar o NPC para ser verificado
            isDestroyed = false; // Reinicia o flag quando o NPC se move
        }
    }

    private void OnNPCDestroyedOrMoved()
    {
        if (hasReachedRoom && !isDestroyed)
        {
            // Aqui você pode chamar a função que deve ser executada após a movimentação ou destruição
            Debug.Log("Priority NPC moved or destroyed!");
            // Você pode destruir o NPC ou realizar outra lógica aqui
            Destroy(gameObject);
            isDestroyed = true; // Marca como destruído
        }
    }
}
