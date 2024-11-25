using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Image fadeImage; // Referência à imagem para o fade
    public float fadeDuration = 1f; // Tempo para o fade
    public AudioSource buttonSound; // Referência ao som do botão

    private void Start()
    {
        // Verifica se fadeImage está atribuído
        if (fadeImage == null)
        {
            // Tenta encontrar o componente Image no GameObject ou nos filhos
            fadeImage = GetComponentInChildren<Image>();
            if (fadeImage == null)
            {
                Debug.LogError("Nenhum componente Image encontrado! Por favor, atribua um no Inspector.");
                return;
            }
        }

        StartCoroutine(FadeIn());
    }

    // Método para iniciar o jogo com fade-out e som
    public void StartGame()
    {
        StartCoroutine(PlaySoundAndLoadScene("saladetestes"));
    }

    // Método para sair do jogo com fade-out e som
    public void QuitGame()
    {
        StartCoroutine(PlaySoundAndQuit());
    }

    // Reproduz o som e carrega uma nova cena com fade-out
    private IEnumerator PlaySoundAndLoadScene(string sceneName)
    {
        // Toca o som se houver um AudioSource configurado
        if (buttonSound != null)
        {
            buttonSound.Play();
            // Aguarda até que o som termine ou 0.1s, o que for maior
            yield return new WaitForSeconds(Mathf.Max(buttonSound.clip.length, 0.1f));
        }

        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName);
    }

    // Reproduz o som e fecha o jogo com fade-out
    private IEnumerator PlaySoundAndQuit()
    {
        // Toca o som se houver um AudioSource configurado
        if (buttonSound != null)
        {
            buttonSound.Play();
            // Aguarda até que o som termine ou 0.1s, o que for maior
            yield return new WaitForSeconds(Mathf.Max(buttonSound.clip.length, 0.1f));
        }

        yield return StartCoroutine(FadeOut());
        Debug.Log("Saindo do jogo!");
        Application.Quit();
    }

    // Fade-in (desvanece a tela para transparente)
    private IEnumerator FadeIn()
    {
        if (fadeImage == null)
        {
            Debug.LogError("fadeImage não foi atribuído no Inspector!");
            yield break;
        }

        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        for (float t = fadeDuration; t >= 0; t -= Time.deltaTime)
        {
            color.a = t / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }

    // Fade-out (desvanece a tela para preto)
    private IEnumerator FadeOut()
    {
        if (fadeImage == null)
        {
            Debug.LogError("fadeImage não foi atribuído no Inspector!");
            yield break;
        }

        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
        {
            color.a = t / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1;
        fadeImage.color = color;
    }
}
