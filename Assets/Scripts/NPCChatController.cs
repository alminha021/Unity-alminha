using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;  // Adicione esta linha para usar o Button

public class NPCChatController : MonoBehaviour
{
    public GameObject chatPanel;
    public TMP_InputField inputQuestion;  // TextMesh Pro InputField
    public TMP_Text textResponse;         // TextMesh Pro Text
    public Button askButton;              // Botão para fazer perguntas
    public FirstNPCTrigger firstNPCTrigger; // Referência ao FirstNPCTrigger

    private Dictionary<string, string> questionAnswers = new Dictionary<string, string>();

    private void Start()
    {
        // Definindo perguntas e respostas para o NPC
        questionAnswers.Add("qual é o seu problema?", "Eu estou com dor de cabeça e febre.");
        questionAnswers.Add("onde dói?", "A dor está principalmente na cabeça e nos músculos.");
        questionAnswers.Add("quanto tempo está assim?", "Já fazem cerca de três dias.");

        // Esconde o painel de chat no início
        chatPanel.SetActive(false);

        // Associa o botão de perguntar ao método de busca de resposta
        askButton.onClick.AddListener(HandleQuestion);
    }

    private void HandleQuestion()
    {
        string playerQuestion = inputQuestion.text.ToLower();
        string bestMatch = FindClosestQuestion(playerQuestion);

        if (!string.IsNullOrEmpty(bestMatch))
        {
            textResponse.text = questionAnswers[bestMatch];
        }
        else
        {
            textResponse.text = "Desculpe, não entendi a pergunta.";
        }

        // Obtém o valor do destino correto do NPC
        Transform nextNPC = firstNPCTrigger.GetNextNPC();
        if (nextNPC != null)
        {
            int correctRoom = firstNPCTrigger.GetNPCDestinationRoom(nextNPC);
            textResponse.text += $" O destino correto é a sala {correctRoom}.";
        }

        inputQuestion.text = ""; // Limpa o campo de entrada após a pergunta
    }

    private string FindClosestQuestion(string inputQuestion)
    {
        string closestQuestion = null;
        int shortestDistance = int.MaxValue;

        foreach (var question in questionAnswers.Keys)
        {
            int distance = LevenshteinDistance(inputQuestion, question.ToLower());
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestQuestion = question;
            }
        }

        // Definindo um limite de distância para considerar a pergunta próxima
        return shortestDistance <= 5 ? closestQuestion : null;
    }

    private int LevenshteinDistance(string source, string target)
    {
        int[,] matrix = new int[source.Length + 1, target.Length + 1];

        for (int i = 0; i <= source.Length; i++)
            matrix[i, 0] = i;

        for (int j = 0; j <= target.Length; j++)
            matrix[0, j] = j;

        for (int i = 1; i <= source.Length; i++)
        {
            for (int j = 1; j <= target.Length; j++)
            {
                int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                matrix[i, j] = Mathf.Min(
                    Mathf.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                    matrix[i - 1, j - 1] + cost);
            }
        }

        return matrix[source.Length, target.Length];
    }

    public void OpenChat()
    {
        chatPanel.SetActive(true);
    }

    public void CloseChat()
    {
        chatPanel.SetActive(false);
    }
}
