using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchManager : MonoBehaviour
{
    public TextMeshProUGUI result;
    public Transform orderTransform;
    public Transform enemyOrderTransform;
    public List<TextMeshProUGUI> topScores;
    public TextMeshProUGUI topTotalScore;
    public List<TextMeshProUGUI> bottomScores;
    public TextMeshProUGUI bottomTotalScore;
    public IntVariable money;


    [HideInInspector]public PlayerController[] order;
    [HideInInspector]public EnemyPlayerController[] enemyOrder;

    private int currentBat;
    private int currentEnemyBat;
    [HideInInspector] public int totalRuns;
    private int totalEnemyRuns;

    private int runs;
    private int outs;
    private int innings;
    private bool[] runnersOnBases;

    private bool isTop;
    private bool isPlayerTop = false;

    void InitGame()
    {
        currentBat = 0;
        currentEnemyBat = 0;
        totalRuns = 0;
        totalEnemyRuns = 0;
        runs = 0;
        outs = 0;
        innings = 1;
        isTop = true;
        runnersOnBases = new bool[3] { false, false, false };

        for (int i = 0; i < 9; i++)
        {
            topScores[i].text = "";
            bottomScores[i].text = "";
        }
        topTotalScore.text = "0";
        bottomTotalScore.text = "0";
    }

    public void ConfirmOrder()
    {
        order = orderTransform.GetComponentsInChildren<PlayerController>();
        //Debug.Log(order.Length);
        enemyOrder= enemyOrderTransform.GetComponentsInChildren<EnemyPlayerController>();
    }

    void BeginInning()
    {
        string topBottom = isTop ? "top" : "bottom";
        Debug.Log("inning: "+innings+"_"+ topBottom);

        while (outs < 3)
        {
            if (isPlayerTop==isTop)
            {
                order[currentBat].Batting(ref outs,ref runs, ref runnersOnBases);
                CycleOrder(true);
            }
            else
            {
                enemyOrder[currentEnemyBat].Batting(ref outs, ref runs, ref runnersOnBases);
                CycleOrder(false);
            }
        }

    }

    void CycleOrder(bool isPlayer)
    {
        if (isPlayer)
        {
            currentBat++;
            if (currentBat == 9)
                currentBat = 0;
        }
        else
        {
            currentEnemyBat++;
            if (currentEnemyBat == 9)
                currentEnemyBat = 0;
        }
    }

    void EndInning(bool isPlayer)
    {
        Debug.Log(runs);

        //textへの反映
        //result.text += runs+" ";

        if (isPlayer)
        {
            totalRuns += runs;
        }
        else
        {
            totalEnemyRuns += runs;
        }

        DisplayScore(runs, innings, isTop);

        runs = 0;
        outs = 0;
        runnersOnBases = new bool[3] { false, false, false };

        isTop = !isTop;
    }

    void DisplayScore(int runs, int innings, bool isTop)
    {
        if (isTop)
        {
            topScores[innings - 1].text = runs.ToString();
            topTotalScore.text = (isPlayerTop ? totalRuns : totalEnemyRuns).ToString();

        }
        else
        {
            bottomScores[innings - 1].text = runs.ToString();
            bottomTotalScore.text = (isPlayerTop ? totalEnemyRuns : totalRuns).ToString();
        }
    }


    void EndMatch()
    {
        result.text = totalRuns > totalEnemyRuns? "WIN":"LOST";

        for (int i = 0; i < order.Length; i++)
        {
            order[i].stats.Refresh();
        }

        money.ApplyChange(1);
    }

    public IEnumerator ExcuteMatch()
    {
        InitGame();
        //ConfirmOrder();

        while (innings <= 9)
        {
            for (int i = 0; i < 2; i++)
            {
                BeginInning();
                EndInning(isPlayerTop == isTop);
                yield return new WaitForSeconds(0.3f);
            }
            innings++;
        }

        EndMatch();

    }


}
