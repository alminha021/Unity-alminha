using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject taskCanvas;  // O Canvas da Task

    // Exibe a Task
    public void StartTask()
    {
        taskCanvas.SetActive(true);  // Ativa o Canvas da Task
        Time.timeScale = 0f;  // Pausa o jogo enquanto a task está ativa
    }

    // Fecha a Task
    public void EndTask()
    {
        taskCanvas.SetActive(false);  // Desativa o Canvas da Task
        Time.timeScale = 1f;  // Retoma o jogo
    }
}
