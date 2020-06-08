using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopBarController : MonoBehaviour
{
    public TextMeshProUGUI matchesText, averageRunText, fameText, moneyText;
    public IntVariable matches;
    public FloatVariable fame, money;
    // Start is called before the first frame update
    void Start()
    {
        RefreshFame();
        RefreshMatch();
        RefreshMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshMoney()
    {
        moneyText.text = money.Value.ToString("n0");
        Debug.Log("money");
    }

    public void RefreshFame()
    {
        fameText.text = fame.Value.ToString("n0");
        Debug.Log("fame");

    }

    public void RefreshMatch()
    {
        matchesText.text = matches.Value.ToString();
        Debug.Log("match");


    }
}
