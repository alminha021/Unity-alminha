using UnityEngine;
using UnityEngine.UI;

public class FirstNPCTrigger : MonoBehaviour
{
    public GameObject popupPanel;
    public Button actionButton;
    public FilaTriggerCtrl filaController; // Fila regular
    public FilaTriggerPriCtrl filaControllerPri; // Fila prioritária
    public Transform destino1;
    public Transform destino2;
    public Transform destino3;
    private int totalPoints = 0;

    private void Start()
    {
        popupPanel.SetActive(false);
        actionButton.onClick.AddListener(OnActionButtonClick);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered first NPC interaction!");
            OpenPopup();
        }
    }

    private void OpenPopup()
    {
        popupPanel.SetActive(true);
    }

    public void OnActionButtonClick()
    {
        // Verificar se há um NPC na fila prioritária primeiro
        Transform firstNPC = filaControllerPri.GetFirstNPC(); 

        if (firstNPC == null) // Se não houver NPC na fila prioritária, verificar na fila regular
        {
            firstNPC = filaController.GetFirstNPC();
        }

        if (firstNPC != null)
        {
            totalPoints += 10;
            Debug.Log("+10 pontos! Total parcial: " + totalPoints);

            MoveFirstNPC(firstNPC);
        }
        else
        {
            Debug.Log("No more NPCs to move.");
        }

        ClosePopup();
    }

    private void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    private void MoveFirstNPC(Transform firstNPC)
    {
        // Verificar se o NPC é da fila prioritária ou da fila regular
        NPCCtrl npcController = firstNPC.GetComponent<NPCCtrl>();  // Para a fila regular
        if (npcController != null)  // Se for um NPC da fila regular
        {
            MoveNPC(npcController, filaController, firstNPC);
        }
        else
        {
            NPCCtrlPri npcControllerPri = firstNPC.GetComponent<NPCCtrlPri>();  // Para a fila prioritária
            if (npcControllerPri != null)  // Se for um NPC da fila prioritária
            {
                MoveNPC(npcControllerPri, filaControllerPri, firstNPC);
            }
        }
    }

    private void MoveNPC(NPCCtrl npcController, FilaTriggerCtrl filaCtrl, Transform npc)
    {
        Transform destino = null;
        switch (npcController.valorNPC)
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
            npcController.MoveToDestination(destino);  // Move o NPC da fila regular para o destino
        }

        filaCtrl.RemoveFirstNPCAndUpdateQueue();  // Remove da fila regular
    }

    private void MoveNPC(NPCCtrlPri npcControllerPri, FilaTriggerPriCtrl filaCtrlPri, Transform npc)
    {
        Transform destino = null;
        switch (npcControllerPri.valorNPC)
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
            npcControllerPri.MoveToDestination(destino);  // Move o NPC da fila prioritária para o destino
        }

        filaCtrlPri.RemoveFirstNPCAndUpdateQueue();  // Remove da fila prioritária
    }
}
