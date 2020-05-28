using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsController : MonoBehaviour
{
    public TextMeshProUGUI AVGText, HRText, RBIText, OBPText, OPSText;
    private int PA,AB, H, HR, RBI, BB;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        PA = 0;
        AB = 0;
        H =0;
        HR = 0;
        RBI = 0;
        BB = 0;
    }

    public void Refresh()
    {
        AB = PA - BB;

        AVGText.text = ((float)H/AB).ToString("n3")+"("+AB+"-"+H+")";
        HRText.text = HR.ToString();
        RBIText.text = RBI.ToString();
        float OBP = (float)(H + BB) / PA;
        OBPText.text = OBP.ToString("n3");
        OPSText.text = (CalculateSLG()+OBP).ToString("n3");
    }

    public void ChangePA(int num)
    {
        PA += num;
    }
    public void ChangeAB(int num)
    {

    }

    public void ChangeH(int num)
    {
        H += num;
    }

    public void ChangeHR(int num)
    {
        HR += num;
    }

    public void ChangeRBI(int num)
    {
        RBI += num;
    }

    public void ChangeBB(int num)
    {
        BB += num;

    }

    public void Change()
    {

    }

    private float CalculateSLG()
    {
        int single = H - HR;
        float singleRate = (float)single / AB;
        float exH = 1 * singleRate;

        float HRRate = (float)HR / AB;
        float exHR = 4 * HRRate;

        float SLG = exH + exHR;
        //Debug.Log(SLG);
        return SLG;
    }

}
