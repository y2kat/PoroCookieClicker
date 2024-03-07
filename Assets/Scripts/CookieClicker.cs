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

    //CURSOR
    public Button cursorButton;
    public TextMeshProUGUI cursorButtonText;
    private float cursorAutoClickInterval = 10.0f; // Intervalo de clics automáticos para el Cursor
    private float cursorAutoClickTimer = 0.0f;


    //GRANDMA
    public Button grandmaButton;
    public TextMeshProUGUI grandmaButtonText;
    private float grandmaAutoClickInterval = 8.0f; // Intervalo de clics automáticos para el Cursor
    private float grandmaAutoClickTimer = 0.0f;


    //FARM
    public Button farmButton;
    public TextMeshProUGUI farmButtonText;
    private float farmAutoClickInterval = 7.0f; // Intervalo de clics automáticos para el Cursor
    private float farmAutoClickTimer = 0.0f;

    //MINE
    public Button mineButton;
    public TextMeshProUGUI mineButtonText;
    private float mineAutoClickInterval = 5.0f; // Intervalo de clics automáticos para el Cursor
    private float mineAutoClickTimer = 0.0f;

    // variables específicas de los edificios
    private int cursorCount = 0;
    private int grandmaCount = 0;
    private int farmCount = 0;
    private int mineCount = 0;

    // clase para representar las mejoras
    public class Upgrade
    {
        public int baseCost = 10; // Costo inicial
        public float cpsIncrease = 0.1f; // Aumento en el CPS por mejora
        public float costMultiplier = 1.2f; // Factor de aumento de costo
    }

    private Upgrade cursorUpgrade;
    private Upgrade grandmaUpgrade;
    private Upgrade farmUpgrade;
    private Upgrade mineUpgrade;


    void Start()
    {
        cursorUpgrade = new Upgrade();
        grandmaUpgrade = new Upgrade { baseCost = 25 };
        farmUpgrade = new Upgrade { baseCost = 45 };
        mineUpgrade = new Upgrade { baseCost = 100 };

        cookieButton.onClick.AddListener(IncrementScore);
        cursorButton.onClick.AddListener(BuyCursor);
        grandmaButton.onClick.AddListener(BuyGrandma);
        farmButton.onClick.AddListener(BuyFarm);
        mineButton.onClick.AddListener(BuyMine);
    }

    void Update()
    {
        // calcula las galletas por segundo (CPS)
        cookiesPerSecond = (cursorCount * 0.1f) + (grandmaCount * 1.0f) + (farmCount * 5.0f) + (mineCount * 10.0f);

        scoreText.text = "Cookies: " + score + "\nCookies per Second: " + cookiesPerSecond.ToString("F2");
        cursorButtonText.text = "Cursor\nCost: " + cursorUpgrade.baseCost + "\nOwned: " + cursorCount;
        grandmaButtonText.text = "Grandma\nCost: " + grandmaUpgrade.baseCost + "\nOwned: " + grandmaCount;
        farmButtonText.text = "Farm\nCost: " + farmUpgrade.baseCost + "\nOwned: " + farmCount;
        mineButtonText.text = "Mine\nCost: " + mineUpgrade.baseCost + "\nOwned: " + mineCount;

        cursorAutoClickTimer += Time.deltaTime;
        if (cursorAutoClickTimer >= cursorAutoClickInterval && cursorCount > 0)
        {
            cursorAutoClickTimer = 0.0f;
            IncrementScore(); // Realiza un clic automático en la galleta
            Debug.Log("Cursor +1");
        }

        grandmaAutoClickTimer += Time.deltaTime;
        if (grandmaAutoClickTimer >= grandmaAutoClickInterval && grandmaCount > 0)
        {
            grandmaAutoClickTimer = 0.0f;
            IncrementScore();
            Debug.Log("Grandma +1");
        }

        farmAutoClickTimer += Time.deltaTime;
        if (farmAutoClickTimer >= farmAutoClickInterval && farmCount > 0)
        {
            farmAutoClickTimer = 0.0f;
            IncrementScore();
            Debug.Log("Farm +1");
        }

        mineAutoClickTimer += Time.deltaTime;
        if (mineAutoClickTimer >= mineAutoClickInterval && mineCount > 0)
        {
            mineAutoClickTimer = 0.0f;
            IncrementScore();
            Debug.Log("Mina +1");
        }
    }

    void IncrementScore()
    {
        score++; // incrementa el contador
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

    //CURSOR
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

    //GRANDMA
    void BuyGrandma()
    {
        if (score >= grandmaUpgrade.baseCost)
        {
            score -= grandmaUpgrade.baseCost;
            grandmaCount++;
            grandmaUpgrade.baseCost = Mathf.RoundToInt(grandmaUpgrade.baseCost * grandmaUpgrade.costMultiplier);
        }
    }

    void BuyGrandmaUpgrade()
    {
        BuyUpgrade(grandmaUpgrade);
    }

    //FARM
    void BuyFarm()
    {
        if (score >= farmUpgrade.baseCost)
        {
            score -= farmUpgrade.baseCost;
            farmCount++;
            farmUpgrade.baseCost = Mathf.RoundToInt(farmUpgrade.baseCost * farmUpgrade.costMultiplier);
        }
    }

    void BuyFarmUpgrade()
    {
        BuyUpgrade(farmUpgrade);
    }

    //MINE
    void BuyMine()
    {
        if (score >= mineUpgrade.baseCost)
        {
            score -= mineUpgrade.baseCost;
            mineCount++;
            mineUpgrade.baseCost = Mathf.RoundToInt(mineUpgrade.baseCost * mineUpgrade.costMultiplier);
        }
    }

    void BuyMineUpgrade()
    {
        BuyUpgrade(mineUpgrade);
    }
}
