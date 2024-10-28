using UnityEngine;
using UnityEngine.UI;

public class FirstNPCTrigger : MonoBehaviour
{
    public GameObject popupPanel;    // Referência ao popup de UI
    public Button actionButton;      // Referência ao botão no popup
    public FilaTriggerController filaController; // Referência ao controlador da fila
    public Transform destino1;       // Destino para NPCs com valor 1
    public Transform destino2;       // Destino para NPCs com valor 2
    public Transform destino3;       // Destino para NPCs com valor 3
    private int totalPoints = 0;     // Total de pontos acumulados

    private void Start()
    {
        popupPanel.SetActive(false); // Inicia com o popup escondido
        actionButton.onClick.AddListener(OnActionButtonClick); // Configura o botão para acionar o método
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered first NPC interaction!");
            OpenPopup(); // Abre o popup quando o jogador interage
        }
    }

    private void OpenPopup()
    {
        popupPanel.SetActive(true); // Ativa o popup
    }

    private void OnActionButtonClick()
    {
        // Adiciona 10 pontos e mostra no console
        totalPoints += 10;
        Debug.Log("+10 pontos! Total parcial: " + totalPoints);

        MoveFirstNPC(); // Move o NPC após o clique
        popupPanel.SetActive(false); // Fecha o popup
    }

    private void MoveFirstNPC()
    {
        Transform firstNPC = filaController.GetFirstNPC();
        if (firstNPC != null)
        {
            NPCController npcController = firstNPC.GetComponent<NPCController>();
            if (npcController != null)
            {
                switch (npcController.valorNPC)
                {
                    case 1:
                        npcController.MoveToQueue(destino1);
                        break;
                    case 2:
                        npcController.MoveToQueue(destino2);
                        break;
                    case 3:
                        npcController.MoveToQueue(destino3);
                        break;
                }

                filaController.RemoveFirstNPCAndUpdateQueue(); // Atualiza a fila
            }
        }
    }
}
