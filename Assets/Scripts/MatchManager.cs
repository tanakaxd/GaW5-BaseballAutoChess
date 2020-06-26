using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchManager : MonoBehaviour
{
    public TextMeshProUGUI m_result;
    
    public List<TextMeshProUGUI> m_topScores;
    public TextMeshProUGUI m_topTotalScore;
    public List<TextMeshProUGUI> m_bottomScores;
    public TextMeshProUGUI m_bottomTotalScore;
    public FloatVariable m_money;
    public FloatVariable m_fame;
    public FloatVariable m_totalScores;

    public OrderManager m_orderManager;




    [HideInInspector] public int currentBat;
    [HideInInspector]public  int currentEnemyBat;
    [HideInInspector]public  int totalRuns;
    [HideInInspector]public  int totalEnemyRuns;

    [HideInInspector]public  int runs;
    [HideInInspector]public  int outs;
    [HideInInspector]public  int innings;
    [HideInInspector]public  bool[] runnersOnBases;

    [HideInInspector]public  bool isTop;
    [HideInInspector]public  bool isPlayerTop = false;

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
        m_result.text = "";

        for (int i = 0; i < 9; i++)
        {
            m_topScores[i].text = "";
            m_bottomScores[i].text = "";
        }
        m_topTotalScore.text = "0";
        m_bottomTotalScore.text = "0";
    }



    void BeginInning()
    {
        string topBottom = isTop ? "top" : "bottom";
        Debug.Log("inning: "+innings+"_"+ topBottom);

        while (outs < 3)
        {
            if (isPlayerTop==isTop)
            {
                m_orderManager.order[currentBat].Batting(this);
                CycleOrder(true);
            }
            else
            {
                m_orderManager.enemyOrder[currentEnemyBat].Batting(ref outs, ref runs, ref runnersOnBases);
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
            m_topScores[innings - 1].text = runs.ToString();
            m_topTotalScore.text = (isPlayerTop ? totalRuns : totalEnemyRuns).ToString();

        }
        else
        {
            m_bottomScores[innings - 1].text = runs.ToString();
            m_bottomTotalScore.text = (isPlayerTop ? totalEnemyRuns : totalRuns).ToString();
        }
    }


    void EndMatch()
    {
        
        if(totalRuns > totalEnemyRuns)
        {
            m_result.text = "WIN!"; 
            m_money.ApplyChange(3);
            m_fame.ApplyChange(3);

        }else if(totalRuns == totalEnemyRuns)
        {
            m_result.text = "DRAW";
            //m_money.ApplyChange(m_fame.Value);
        }
        else
        {
            m_result.text ="LOST";
            //m_money.ApplyChange(m_fame.Value);
            //m_fame.ApplyChange(-1);
        }


        for (int i = 0; i < m_orderManager.order.Length; i++)
        {
            m_orderManager.order[i].stats.Refresh();
        }

        m_totalScores.ApplyChange(totalRuns);

        
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
