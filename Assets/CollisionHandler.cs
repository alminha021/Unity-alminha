using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    public int value;

    void OnCollisionEnter(Collision collision)
    {
        Text scoreText = GameObject.Find("Text_TotalPacientes").GetComponent<Text>();
        scoreText.text = (int.Parse(scoreText.text) + value).ToString();
    }
}

