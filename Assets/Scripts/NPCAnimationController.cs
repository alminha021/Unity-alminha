using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent != null && animator != null)
        {
            // Define o valor do parâmetro isRunning com base na velocidade do agente
            bool isRunning = agent.velocity.magnitude > 0.1f; // Considera que o NPC está "correndo" se a velocidade é maior que 0.1

            animator.SetBool("isRunning", isRunning); // Atualiza o parâmetro no Animator
        }
    }
}
