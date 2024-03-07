using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buff : MonoBehaviour
{
    public Button buffButton; // botón que representa el buff
    public TextMeshProUGUI buffButtonText; // texto de la UI donde se mostrará el costo del buff
    public int buffCost = 10; // el costo inicial del buff
    public float buffMultiplier = 2.0f; // el multiplicador del buff
    public float costMultiplier = 1.5f; // el multiplicador del costo del buff
    public string buildingType; // el tipo de edificio que este buff afecta

    void Start()
    {
        buffButton.onClick.AddListener(ApplyBuff);
        buffButton.gameObject.SetActive(false); // Desactiva el botón al inicio
        UpdateButtonText();
    }

    void ApplyBuff()
    {
        if (CookieClicker.score >= buffCost)
        {
            CookieClicker.score -= buffCost;
            buffCost = Mathf.RoundToInt(buffCost * costMultiplier);

            switch (buildingType)
            {
                case "Cursor":
                    CookieClicker.cursorAutoClickInterval /= buffMultiplier;
                    break;
                case "Grandma":
                    CookieClicker.grandmaAutoClickInterval /= buffMultiplier;
                    break;
                case "Farm":
                    CookieClicker.farmAutoClickInterval /= buffMultiplier;
                    break;
                case "Mine":
                    CookieClicker.mineAutoClickInterval /= buffMultiplier;
                    break;
            }

            UpdateButtonText();
        }
    }

    void UpdateButtonText()
    {
        buffButtonText.text = buildingType + " Buff\nCost: " + buffCost;
    }

    public void CheckBuildingCount()
    {
        switch (buildingType)
        {
            case "Cursor":
                if (CookieClicker.cursorCount > 0)
                    buffButton.gameObject.SetActive(true);
                break;
            case "Grandma":
                if (CookieClicker.grandmaCount > 0)
                    buffButton.gameObject.SetActive(true);
                break;
            case "Farm":
                if (CookieClicker.farmCount > 0)
                    buffButton.gameObject.SetActive(true);
                break;
            case "Mine":
                if (CookieClicker.mineCount > 0)
                    buffButton.gameObject.SetActive(true);
                break;
        }
    }

}
