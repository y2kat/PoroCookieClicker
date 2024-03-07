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
    private float cookiesPerSecond = 0.0f; // contador de galletas por segundo (CPS)

    public Button cursorButton;
    public TextMeshProUGUI cursorButtonText;
    private float cursorAutoClickInterval = 10.0f; // Intervalo de clics automáticos para el Cursor
    private float cursorAutoClickTimer = 0.0f;

    // variables específicas de los edificios
    private int cursorCount = 0;
    /*private int grandmaCount = 0;
    private int farmCount = 0;
    private int mineCount = 0;*/

    // clase para representar las mejoras
    public class Upgrade
    {
        public int baseCost = 10; // Costo inicial
        public float cpsIncrease = 0.1f; // Aumento en el CPS por mejora
        public float costMultiplier = 1.2f; // Factor de aumento de costo
    }

    private Upgrade cursorUpgrade;


    void Start()
    {
        cursorUpgrade = new Upgrade();

        cookieButton.onClick.AddListener(IncrementScore);
        cursorButton.onClick.AddListener(BuyCursor);
        /*grandmaButton.onClick.AddListener(BuyGrandma);
        farmButton.onClick.AddListener(BuyFarm);
        mineButton.onClick.AddListener(BuyMine);*/
    }

    void Update()
    {
        // calcula las galletas por segundo (CPS)
        cookiesPerSecond = (cursorCount * 0.1f) /*+ (grandmaCount * 1.0f) + (farmCount * 5.0f) + (mineCount * 10.0f)*/;

        scoreText.text = "Cookies: " + score + "\nCookies per Second: " + cookiesPerSecond.ToString("F2");
        cursorButtonText.text = "Cursor\nCost: " + cursorUpgrade.baseCost + "\nOwned: " + cursorCount;

        cursorAutoClickTimer += Time.deltaTime;
        if (cursorAutoClickTimer >= cursorAutoClickInterval)
        {
            cursorAutoClickTimer = 0.0f;
            IncrementScore(); // Realiza un clic automático en la galleta
        }
    }

    void IncrementScore()
    {
        score++; // incrementa el contador
    }

    void BuyCursor()
    {
        if (score >= cursorUpgrade.baseCost)
        {
            score -= cursorUpgrade.baseCost;
            cursorCount++;
            cursorUpgrade.baseCost = Mathf.RoundToInt(cursorUpgrade.baseCost * cursorUpgrade.costMultiplier);
        }
    }

    void BuyCursorUpgrade()
    {
        BuyUpgrade(cursorUpgrade);
    }

    void BuyUpgrade(Upgrade upgrade)
    {
        if (score >= upgrade.baseCost)
        {
            score -= upgrade.baseCost;
            upgrade.baseCost = Mathf.RoundToInt(upgrade.baseCost * upgrade.costMultiplier);
            cookiesPerSecond += upgrade.cpsIncrease;
        }
    }
}
