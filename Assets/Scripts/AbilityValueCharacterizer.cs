using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AbilityValueCharacterizer : MonoBehaviour
{
    //public PlayerController controller;

    public TextMeshProUGUI avgCharacter;
    public TextMeshProUGUI homerunCharacter;
    public TextMeshProUGUI disciplineCharacter;

    //public float maxAVG,maxHR,maxDiscipline;
    public float aveAVG,aveHR,aveDiscipline;
    public FloatReference sigmaAVG,sigmaHR,sigmaDiscipline;

    public enum AbilityChar
    {
        S,
        A,
        B,
        C,
        D,
        E,
        F,
        G
    }
    /*
    int CentiNormalizer(float abilityValue, float max)
    {
        float normalized = abilityValue/max*100;
        return (int)normalized;
    }
    */

    int Deviatize(float abilityValue, float sigma, float average)
    {
        float deviation = (abilityValue - average) / sigma*10 + 50;
        return (int)deviation;
    }

    private void Awake()
    {
        //controller.onPlayerAbilityValueChange += Refresh;
    }

    public void Refresh(float average,float homerun,float disc)
    {
        //Debug.Log("refresh called");

        /*
        int avg = CentiNormalizer(average, maxAVG);
        Characterizer(avg,avgCharacter);
        Characterizer(CentiNormalizer(homerun, maxHR), homerunCharacter);
        Characterizer(CentiNormalizer(disc, maxDiscipline), disciplineCharacter);
        */

        Characterizer(Deviatize(average, sigmaAVG, aveAVG), avgCharacter);
        Characterizer(Deviatize(homerun, sigmaHR, aveHR), homerunCharacter);
        Characterizer(Deviatize(disc, sigmaDiscipline, aveDiscipline), disciplineCharacter);
    }


    void Characterizer(int normalizedValue, TextMeshProUGUI textUI)
    {
        if (normalizedValue >= 90)
        {
            textUI.text = "S"+ normalizedValue;
            textUI.color = new Color(0.95f, 0.73f, 0.95f);
            textUI.fontStyle = FontStyles.Bold;
        }
        else if (normalizedValue >= 80)
        {
            textUI.text = "A"+ normalizedValue;
            textUI.color = new Color(1.0f,0.25f,0.85f);
        }else if (normalizedValue >= 70)
        {
            textUI.text = "B"+ normalizedValue;
            textUI.color = Color.red;
        }else if (normalizedValue >= 60)
        {
            textUI.text = "C"+ normalizedValue;
            textUI.color = new Color(1.0f, 0.5f, 0f);
        }else if (normalizedValue >= 50)
        {
            textUI.text = "D"+ normalizedValue;
            textUI.color = Color.yellow;
        }else if (normalizedValue >= 40)
        {
            textUI.text = "E"+ normalizedValue;
            textUI.color = Color.green;
        }else if (normalizedValue >= 20)
        {
            textUI.text = "F"+ normalizedValue;
            textUI.color = Color.blue;
        }
        else
        {
            textUI.text = "G"+ normalizedValue;
            textUI.color = Color.grey;
        }
    }

}
