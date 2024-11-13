using UnityEngine;
using UnityEngine.UI;

public class FirstNPCTrigger : MonoBehaviour
{
    public GameObject popupPanel;
    public Button room1Button;
    public Button room2Button;
    public Button room3Button;
    public FilaTriggerCtrl filaController; // Fila regular
    public FilaTriggerPriCtrl filaControllerPri; // Fila prioritária
    public Transform destino1;
    public Transform destino2;
    public Transform destino3;
    public PlayerController playerController; // Referência ao controlador do player
    private int totalPoints = 0;
    private bool playerInTrigger = false; // Verifica se o player está no trigger

    private void Start()
    {
        popupPanel.SetActive(false);

        // Adiciona a função de clique aos botões
        room1Button.onClick.AddListener(() => OnRoomButtonClick(1));
        room2Button.onClick.AddListener(() => OnRoomButtonClick(2));
        room3Button.onClick.AddListener(() => OnRoomButtonClick(3));
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.T))
        {
            OpenPopup(); // Abre o popup ao pressionar 'T'
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered first NPC interaction!");
            playerInTrigger = true; // Marca que o player entrou no trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; // Marca que o player saiu do trigger
        }
    }

    private void OpenPopup()
    {
        popupPanel.SetActive(true);
        playerController.enabled = false; // Desativa o movimento do player
    }

    private void ClosePopup()
    {
        popupPanel.SetActive(false);
        playerController.enabled = true; // Reativa o movimento do player
    }

    private void OnRoomButtonClick(int selectedRoom)
    {
        // Verifica qual NPC é o próximo
        Transform nextNPC = GetNextNPC();

        if (nextNPC != null)
        {
            int correctRoom = GetNPCDestinationRoom(nextNPC);

            if (selectedRoom == correctRoom)
            {
                totalPoints += 10;
                Debug.Log("CORRETO! +10 pontos. Total: " + totalPoints);
            }
            else
            {
                Debug.Log("ERRADO! O NPC foi enviado para a sala " + selectedRoom);
            }

            // Mover o NPC para a sala selecionada
            MoveNPCToRoom(nextNPC, selectedRoom);
        }
        else
        {
            Debug.Log("Não há NPCs disponíveis.");
        }

        ClosePopup(); // Fecha o popup após a escolha
    }

    private void MoveNPCToRoom(Transform npc, int room)
    {
        Transform destino = null;

        // Determina o destino do NPC baseado na sala selecionada
        switch (room)
        {
            case 1:
                destino = destino1;
                break;
            case 2:
                destino = destino2;
                break;
            case 3:
                destino = destino3;
                break;
        }

        if (destino != null)
        {
            // Move o NPC para a sala selecionada
            NPCCtrl npcController = npc.GetComponent<NPCCtrl>();
            if (npcController != null)
            {
                npcController.MoveToDestination(destino);
                filaController.RemoveFirstNPCAndUpdateQueue();
            }
            else
            {
                NPCCtrlPri npcControllerPri = npc.GetComponent<NPCCtrlPri>();
                if (npcControllerPri != null)
                {
                    npcControllerPri.MoveToDestination(destino);
                    filaControllerPri.RemoveFirstNPCAndUpdateQueue();
                }
            }
        }
    }

    public Transform GetNextNPC()
    {
        // Verifica a fila prioritária primeiro, depois a fila regular
        Transform nextNPC = null;
        if (filaControllerPri.GetFirstNPC() != null)
        {
            nextNPC = filaControllerPri.GetFirstNPC();
        }
        else if (filaController.GetFirstNPC() != null)
        {
            nextNPC = filaController.GetFirstNPC();
        }
        return nextNPC;
    }

    public int GetNPCDestinationRoom(Transform npc)
    {
        // Obtém o valor associado ao NPC para determinar sua sala correta
        NPCCtrl npcCtrl = npc.GetComponent<NPCCtrl>();
        if (npcCtrl != null)
        {
            return npcCtrl.valorNPC;
        }

        NPCCtrlPri npcCtrlPri = npc.GetComponent<NPCCtrlPri>();
        if (npcCtrlPri != null)
        {
            return npcCtrlPri.valorNPC;
        }

        return -1; // Caso não encontre o NPC ou o tipo correto
    }
}
