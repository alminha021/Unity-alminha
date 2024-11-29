using UnityEngine;

public class NPCEmoteController : MonoBehaviour
{
    public Sprite correctSprite; // Sprite para quando o NPC for correto
    public Sprite wrongSprite;   // Sprite para quando o NPC for errado

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Pegue o SpriteRenderer do NPC para modificar os sprites
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Método para exibir o sprite de acerto
    public void ShowCorrectEmote()
    {
        spriteRenderer.sprite = correctSprite;
    }

    // Método para exibir o sprite de erro
    public void ShowWrongEmote()
    {
        spriteRenderer.sprite = wrongSprite;
    }
}
