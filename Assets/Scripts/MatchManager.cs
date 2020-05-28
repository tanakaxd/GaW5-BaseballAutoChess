using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchManager : MonoBehaviour
{
    public TextMeshProUGUI result;
    public OrderController orderController;

    [HideInInspector]public PlayerController[] order;
    private int currentBat;
    private int totalRuns;
    private int runs;
    private int outs;
    private int innings;
    private bool[] runnersOnBases;

    void InitGame()
    {
        currentBat = 0;
        totalRuns = 0;
        runs = 0;
        outs = 0;
        innings = 1;
        runnersOnBases = new bool[3] { false, false, false };
    }

    public void ConfirmOrder()
    {
        order = orderController.GetComponentsInChildren<PlayerController>();
        //Debug.Log(order.Length);
    }

    void BeginInning()
    {
        Debug.Log("inning: "+innings);

        while (outs < 3)
        {
            order[currentBat].Batting(ref outs,ref runs, ref runnersOnBases);
            CycleOrder();
        }

    }

    void CycleOrder()
    {
        currentBat++;
        if (currentBat == 9)
            currentBat = 0;
    }

    void EndInning()
    {
        Debug.Log(runs);

        //textへの反映
        result.text += runs+" ";

        totalRuns += runs;



        innings++;
        runs = 0;
        outs = 0;
        runnersOnBases = new bool[3] { false, false, false };
    }

    void EndMatch()
    {
        result.text += " : "+totalRuns + "\n";

        for (int i = 0; i < order.Length; i++)
        {
            order[i].stats.Refresh();
        }

    }

    public int ExcuteMatch()
    {
        InitGame();
        //ConfirmOrder();
        while (innings <= 9)
        {
            BeginInning();
            EndInning();
        }

        EndMatch();

        return totalRuns;
    }


}
