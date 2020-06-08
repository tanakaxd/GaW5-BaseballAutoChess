using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public MatchManager matchManager;
    public EnemyOrderController enemyOrderController;

    //public Transform enemyOrder;
    //public TextMeshProUGUI result;
    public TextMeshProUGUI avgScore;
    public GameObject matchPanel;


    public IntVariable matches;
    public FloatVariable money;
    public FloatVariable startingMoney;
    public FloatVariable fame;
    public FloatVariable startingFame;

    public int consecutiveMatches;
    public bool reset;

    private int totalScore;
    private List<int> scoreHistory;
    //private int totalEnemyScore;

    private void Awake()
    {
        //Debug.unityLogger.logEnabled = false;

        //result.text = "";
        scoreHistory = new List<int>();

        //awakeだとdelegate登録より先にinvokeされてしまう
        //awakeではなくstartでやればとりあえずできるが、今度はその変数を必要とする初期化処理がさらに遅れることになる
        money.SetValue(startingMoney);
        fame.SetValue(startingFame);
        matches.SetValue(0);
        totalScore = 0;
    }

    public void SetActiveDrawPanel()
    {

    }

    public void Simulate()
    {
        matchPanel.gameObject.SetActive(true);

        enemyOrderController.RenewEnemy();

        //result.text = "";
        //totalScore = 0;

        matchManager.m_orderManager.ConfirmOrder();

        if (reset)
        {
            for (int i = 0; i < matchManager.m_orderManager.order.Length; i++)
            {
                matchManager.m_orderManager.order[i].stats.Init();
            }
        }

        for (int i = 0; i < consecutiveMatches; i++)
        {
            //innning by innning version
            StartCoroutine(matchManager.ExcuteMatch());
            //scoreHistory.Add(matchManager.totalRuns);
            totalScore += matchManager.totalRuns;


            //instant version
            //scoreHistory.Add(matchManager.ExcuteMatch());
        }

        matches.ApplyChange(consecutiveMatches);

        avgScore.text = ((float)totalScore / matches.Value).ToString("n2");
    }

}
