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
        if (agent.velocity.sqrMagnitude > 0.1f) // Only update rotation if agent is moving
        {
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;

        if (direction.magnitude > 0.1f) // Prevent zero vector rotation
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }
}
