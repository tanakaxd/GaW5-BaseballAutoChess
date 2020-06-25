using DG.Tweening;
using TMPro;
using UnityEngine;

public class TopBarController : MonoBehaviour
{
    public TextMeshProUGUI matchesText, averageRunText, fameText, moneyText;
    private float currentMatches, currentFame, currentMoney;
    public IntVariable matches;
    public FloatVariable fame, money;

    // Start is called before the first frame update
    private void Start()
    {
        RefreshFame();
        RefreshMatch();
        RefreshMoney();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void RefreshMoney()
    {
        //currentMoney = money.Value;
        //moneyText.text = money.Value.ToString("n0");
        //Sequence seq = DOTween.Sequence();
        ////seq.OnStart();

        DOTween.To(
            () => currentMoney,          // 何を対象にするのか
            num =>
            {
                currentMoney = num;
                moneyText.text = num.ToString("n0");
            },   // 値の更新
            money.Value,                  // 最終的な値
            1.0f                  // アニメーション時間
        );

        Debug.Log("money");
    }

    public void RefreshFame()
    {
        DOTween.To(
            () => currentFame,          // 何を対象にするのか
            num =>
            {
                currentFame = num;
                fameText.text = num.ToString("n0");
            },   // 値の更新
            fame.Value,                  // 最終的な値
            1.0f                  // アニメーション時間
        );
        Debug.Log("fame");
    }

    public void RefreshMatch()
    {
        currentMatches = matches.Value;
        matchesText.text = matches.Value.ToString();
        Debug.Log("match");
    }
}