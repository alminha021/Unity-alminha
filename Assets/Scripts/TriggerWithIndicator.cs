using UnityEngine;

public class TriggerWithIndicator : MonoBehaviour
{
    public GameObject indicatorSprite; // O sprite de indicação
    private bool isPlayerNearby = false; // Para verificar se o jogador está próximo

    private void Start()
    {
        // Garante que o sprite começa desativado
        if (indicatorSprite != null)
        {
            indicatorSprite.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Certifique-se que o jogador tem a tag "Player"
        {
            isPlayerNearby = true;

            // Ativa o sprite de indicação
            if (indicatorSprite != null)
            {
                indicatorSprite.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // Desativa o sprite de indicação
            if (indicatorSprite != null)
            {
                indicatorSprite.SetActive(false);
            }
        }
    }

    private void Update()
    {
        // Aqui você já tem o código para interagir (tecla T). 
        // Se necessário, pode adicionar mais lógica aqui.
    }
}
