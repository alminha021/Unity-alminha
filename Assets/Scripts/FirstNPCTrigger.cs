using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    public Transform posicaoIntermediaria; // Posição intermediária
    public PlayerController playerController; // Referência ao controlador do player
    private int totalPoints = 30;
    private int totalPatients = 0;
    private bool playerInTrigger = false; // Verifica se o player está no trigger
    public float tempoParaDesaparecer = 30f; // Tempo para o NPC desaparecer

    // Variáveis para os sprites de acerto e erro
    public Sprite spriteCorreto;  // Sprite para o acerto
    public Sprite spriteErrado;   // Sprite para o erro

    private Transform currentNPC;

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
            MoveNextNPCToIntermediate(); // Move o próximo NPC para a posição intermediária
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player triggered first NPC interaction!");
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

    private void MoveNextNPCToIntermediate()
    {
        Transform nextNPC = GetNextNPC();

        if (nextNPC != null)
        {

            NPCCtrl npcController = nextNPC.GetComponent<NPCCtrl>();
            if (npcController != null)
            {
                npcController.MoveToDestination(posicaoIntermediaria);
                StartCoroutine(WaitForNPCToReachPosition(npcController, posicaoIntermediaria));
            }
            else
            {
                NPCCtrlPri npcControllerPri = nextNPC.GetComponent<NPCCtrlPri>();
                if (npcControllerPri != null)
                {
                    npcControllerPri.MoveToDestination(posicaoIntermediaria);
                    StartCoroutine(WaitForNPCToReachPosition(npcControllerPri, posicaoIntermediaria));
                }
            }

            currentNPC = nextNPC;
        }
    }

    private IEnumerator WaitForNPCToReachPosition(Component npcController, Transform targetPosition)
    {
        float tolerancia = 1.5f; // Defina a tolerância desejada (ajuste conforme necessário)

        // Debug.Log("Aguardando NPC alcançar posição intermediária...");

        while (Vector3.Distance(npcController.transform.position, targetPosition.position) > tolerancia)
        {
            //Debug.Log("Distância atual: " + Vector3.Distance(npcController.transform.position, targetPosition.position));
            yield return null;
        }

        // Debug.Log("NPC chegou à posição intermediária.");
        OpenPopup(); // Abre o popup após o NPC chegar na posição intermediária
    }

    private void OpenPopup()
    {
        popupPanel.SetActive(true);
        playerController.enabled = false; // Desativa o movimento do player

        if (currentNPC) {

            Debug.Log(currentNPC);

            PatientCondition patientCondition = getPatientCondition(currentNPC);

            Text nomePaciente = GameObject.Find("NomePaciente").GetComponent<Text>();
            nomePaciente.text = patientCondition.nome;

            Text historiaPaciente = GameObject.Find("historiaPaciente").GetComponent<Text>();
            historiaPaciente.text = patientCondition.queixa;

        }
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

            // Aqui, obtemos o SpriteHolder do NPC
            Transform spriteHolder = nextNPC.Find("SpriteHolder");
            if (spriteHolder != null)
            {
                SpriteRenderer spriteRenderer = spriteHolder.GetComponent<SpriteRenderer>();

                // Verifica se o SpriteRenderer foi encontrado
                if (spriteRenderer != null)
                {
                    if (selectedRoom == correctRoom)
                    {
                        // Acertou a sala, pode colocar um sprite de "felicidade" ou algo que indique o acerto
                        spriteRenderer.sprite = spriteCorreto;  // Substitua 'spriteCorreto' com o sprite desejado
                        totalPoints += 10;
                        Debug.Log("CORRETO! +10 pontos. Total: " + totalPoints);
                    }
                    else
                    {
                        // Errou a sala, pode colocar um sprite de "tristeza" ou algo que indique o erro
                        spriteRenderer.sprite = spriteErrado;  // Substitua 'spriteErrado' com o sprite desejado
                        totalPoints -= 10;
                        Debug.Log("ERRADO! O NPC foi enviado para a sala " + selectedRoom);
                    }
                    totalPatients++;
                }
            }

            Text pacientes = GameObject.Find("Text_Pacientes").GetComponent<Text>();
            pacientes.text = totalPatients.ToString();

            Text reputacao = GameObject.Find("Text_Pontuacao").GetComponent<Text>();
            reputacao.text = totalPoints.ToString();


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

            // Inicia a contagem para destruir o NPC após mover para a sala
            StartCoroutine(DestruirNPC(npc, tempoParaDesaparecer));
        }
    }

    private IEnumerator DestruirNPC(Transform npc, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (npc != null)
        {
            Debug.Log("NPC destruído após o tempo.");
            Destroy(npc.gameObject);
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

    public PatientCondition getPatientCondition(Transform npc)
    {
        NPCCtrl npcCtrl = npc.GetComponent<NPCCtrl>();
        if (npcCtrl != null)
        {
            return npcCtrl.patientCondition;
        }

        NPCCtrlPri npcCtrlPri = npc.GetComponent<NPCCtrlPri>();
        if (npcCtrlPri != null)
        {
            return npcCtrlPri.patientCondition;
        }

        return null;
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
