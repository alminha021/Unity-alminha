using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float wanderRadius = 10f;  // Raio dentro do qual o NPC pode andar aleatoriamente
    public float wanderInterval = 5f;  // Intervalo de tempo entre escolher novos destinos

    private NavMeshAgent agent;
    private float timer;
    private bool isInQueue = false;  // Adicionado para controlar se está na fila

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderInterval;  // Iniciar a contagem com o intervalo definido

        // Registra este NPC na fila no controlador
        FilaTriggerController filaController = FindObjectOfType<FilaTriggerController>();
        if (filaController != null)
        {
            filaController.RegisterNPC(transform);  // Adiciona o NPC à fila
        }
    }

    void Update()
    {
        // Se o NPC não estiver na fila, atualiza o timer
        if (!isInQueue)
        {
            timer += Time.deltaTime;

            // Se o tempo passado for maior que o intervalo definido, escolher um novo destino
            if (timer >= wanderInterval)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);
                agent.SetDestination(newPos);
                timer = 0;  // Resetar o timer
            }
        }
    }

    // Função para encontrar um ponto aleatório no NavMesh
    public Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;  // Gera um ponto aleatório em uma esfera
        randomDirection += origin;

        NavMeshHit navHit;
        // Tenta encontrar o ponto mais próximo no NavMesh a partir do ponto aleatório gerado
        NavMesh.SamplePosition(randomDirection, out navHit, dist, NavMesh.AllAreas);

        return navHit.position;
    }

    // Método para mover o NPC para a fila
    public void MoveToQueue(Transform target)
    {
        Debug.Log($"Moving NPC to position: {target.position}");  // Mensagem de depuração
        agent.SetDestination(target.position);  // Define o destino do NPC
        isInQueue = true;  // Marcar como estando na fila
    }
}
