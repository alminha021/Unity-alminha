using UnityEngine;

public class LightFlickerNOTMENU : MonoBehaviour
{
    public Light alight1; // Primeira luz da sirene
    public Light alight2; // Segunda luz da sirene
    public float aflickerSpeed = 1.0f; // Velocidade do flicker

    private float atimer;

    private void Update()
    {
        atimer += Time.deltaTime * aflickerSpeed;

        // Alterna a intensidade das luzes com base no tempo
        if (alight1 != null)
            alight1.enabled = Mathf.Sin(atimer) > 0;

        if (alight2 != null)
            alight2.enabled = Mathf.Sin(atimer + Mathf.PI) > 0; // Luz 2 alterna inversamente à luz 1
    }
}
