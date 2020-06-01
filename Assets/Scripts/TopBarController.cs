using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopBarController : MonoBehaviour
{
    public TextMeshProUGUI matchesText, averageRunText, fameText, moneyText;
    public IntVariable matches, money;
    public FloatVariable fame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshMoney()
    {
        moneyText.text = money.Value.ToString();
    }

    public void RefreshFame()
    {
        fameText.text = fame.Value.ToString();
    }
}
