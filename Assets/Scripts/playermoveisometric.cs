using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    CustomActions input;
    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] NPCChatController npcChatController;  // Referência ao controlador de chat

    float lookRotationSpeed = 8f;

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        input = new CustomActions();
        AssignInputs();
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    void ClickToMove()
    {
        // Verifica se o chat está aberto antes de permitir o movimento
        if (npcChatController != null && npcChatController.chatPanel.activeSelf) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers)) 
        {
            agent.destination = hit.point;
        }
    }

    void OnEnable() 
    { 
        input.Enable(); 
    }

    void OnDisable() 
    { 
        input.Disable();
    }

    void Update() 
    {
        // Verifica se o chat está aberto antes de permitir a rotação para a direção de movimento
        if (npcChatController != null && npcChatController.chatPanel.activeSelf)
        {
            agent.isStopped = true; // Para o movimento enquanto o chat está aberto
            return;
        }
        
        agent.isStopped = false;

        if (agent.velocity.sqrMagnitude > 0.1f) // Apenas atualiza a rotação se o agente estiver se movendo
        {
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;

        if (direction.magnitude > 0.1f) // Impede rotação para o vetor zero
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }
}
