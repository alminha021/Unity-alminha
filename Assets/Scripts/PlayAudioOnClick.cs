using UnityEngine;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Arraste o AudioSource aqui no Inspector

    public void PlaySound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (audioSource == null)
        {
            Debug.LogWarning("AudioSource não atribuído no script!");
        }
    }
}
