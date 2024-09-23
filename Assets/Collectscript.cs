using UnityEngine;

public class Collectscript : MonoBehaviour
{
    public int points = 10;
    private static float a = 0.0f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Item Coletado! + " + points + " pontos!");
            a += 10;
            Destroy(gameObject);
            Debug.Log("TOTAL =  " + a + " pontos!");
        }
    }
}
