using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieClicker : MonoBehaviour
{
    public int cookieCount = 0;

    void OnMouseDown()
    {
        cookieCount++;
    }
}
