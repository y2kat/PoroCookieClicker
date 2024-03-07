using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CookieClicker : MonoBehaviour
{
    public Button cookieButton; // bot�n que representa la galleta
    public TextMeshProUGUI scoreText; // texto de la UI donde se mostrar� el contador
    public static int score = 0; // el contador
    private float cookiesPerSecond = 0.0f; // contador de galletas por segundo (CPS)

    //CURSOR
    public Button cursorButton;
    public TextMeshProUGUI cursorButtonText;
    public static float cursorAutoClickInterval = 10.0f; // Intervalo de clics autom�ticos para el Cursor
    private float cursorAutoClickTimer = 0.0f;
    public TextMeshProUGUI cursorCountText;

    //GRANDMA
    public Button grandmaButton;
    public TextMeshProUGUI grandmaButtonText;
    public static float grandmaAutoClickInterval = 8.0f;
    private float grandmaAutoClickTimer = 0.0f;
    public TextMeshProUGUI grandmaCountText;


    //FARM
    public Button farmButton;
    public TextMeshProUGUI farmButtonText;
    public static float farmAutoClickInterval = 7.0f;
    private float farmAutoClickTimer = 0.0f;
    public TextMeshProUGUI farmCountText;

    //MINE
    public Button mineButton;
    public TextMeshProUGUI mineButtonText;
    public static float mineAutoClickInterval = 5.0f;
    private float mineAutoClickTimer = 0.0f;
    public TextMeshProUGUI mineCountText;

    // variables espec�ficas de los edificios
    public static int cursorCount = 0;
    public static int grandmaCount = 0;
    public static int farmCount = 0;
    public static int mineCount = 0;

    //variables espec�ficas para los buff
    public Buff cursorBuff;
    public Buff grandmaBuff; 
    public Buff farmBuff; 
    public Buff mineBuff;

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
            IncrementScore(); // Realiza un clic autom�tico en la galleta
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
            cursorBuff.CheckBuildingCount();
            cursorCountText.text = "You have " + cursorCount + " Cursor";
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
            grandmaBuff.CheckBuildingCount();
            grandmaCountText.text = "You have " + grandmaCount + " Grandmas";
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
            farmBuff.CheckBuildingCount();
            farmCountText.text = "You have " + farmCount + " Farms";
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
            mineBuff.CheckBuildingCount();
            mineCountText.text = "You have " + mineCount + " Mines";
        }
    }

    void BuyMineUpgrade()
    {
        BuyUpgrade(mineUpgrade);
    }
}
