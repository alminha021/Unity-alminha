using UnityEngine;

public class FilaTriggerCtrl : MonoBehaviour
{
    public Transform[] filaPoints;
    private Transform[] npcInFila;
    private int currentNPCIndex = 0;

    private void Start()
    {
        npcInFila = new Transform[filaPoints.Length];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered the FilaTriggerController!");
            MoveNPCsToFila();
        }
    }

    public void RegisterNPC(Transform npc)
    {
        if (currentNPCIndex < filaPoints.Length)
        {
            npcInFila[currentNPCIndex] = npc;
            currentNPCIndex++;
        }
    }

    private void MoveNPCsToFila()
    {
        for (int i = 0; i < currentNPCIndex && i < filaPoints.Length; i++)
        {
            if (npcInFila[i] != null)
            {
                NPCCtrl npcController = npcInFila[i].GetComponent<NPCCtrl>();
                if (npcController != null)
                {
                    Debug.Log("Moving NPC " + i + " to queue point");
                    npcController.MoveToQueue(filaPoints[i]);
                }
            }
        }
    }

    public void RemoveFirstNPCAndUpdateQueue()
    {
        if (currentNPCIndex > 0)
        {
            npcInFila[0].GetComponent<NPCCtrl>().MoveToDestination(null);
            for (int i = 1; i < currentNPCIndex; i++)
            {
                npcInFila[i - 1] = npcInFila[i];
            }
            npcInFila[currentNPCIndex - 1] = null;
            currentNPCIndex--;
        }
    }

    public Transform GetFirstNPC()
    {
        return currentNPCIndex > 0 ? npcInFila[0] : null;
    }
}