using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public TaskManager taskManager;  // Referência ao TaskManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            taskManager.StartTask();  // Inicia a task quando o player entra no trigger
        }
    }
}
