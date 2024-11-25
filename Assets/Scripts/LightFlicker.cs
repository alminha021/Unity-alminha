using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light light1; // Primeira luz da sirene
    public Light light2; // Segunda luz da sirene
    public float flickerSpeed = 1.0f; // Velocidade do flicker

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime * flickerSpeed;

        // Alterna a intensidade das luzes com base no tempo
        if (light1 != null)
            light1.enabled = Mathf.Sin(timer) > 0;

        if (light2 != null)
            light2.enabled = Mathf.Sin(timer + Mathf.PI) > 0; // Luz 2 alterna inversamente à luz 1
    }
}
