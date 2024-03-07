using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CookieClicker : MonoBehaviour
{
    public Button cookieButton; // botón que representa la galleta
    public TextMeshProUGUI scoreText; // texto de la UI donde se mostrará el contador
    private int score = 0; // el contador
    private float cookiesPerSecond = 0.0f;

    void Start()
    {
        cookieButton.onClick.AddListener(IncrementScore);
    }

    void Update()
    {
        cookiesPerSecond = score / Time.time;

        scoreText.text = "Cookies: " + score + "\nCookies per Second: " + cookiesPerSecond.ToString("F2");
    }

    void IncrementScore()
    {
        score++; // incrementa el contador
    }
}
