using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider123 : MonoBehaviour
{
    private static float pontos = 0.0f;
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Player"))
        {
        Destroy(gameObject);
        pontos += 10f;
        Debug.Log("Esfera Destruida");
        Debug.Log("Pontos adquiridos =" + pontos);
        }
    }
}
