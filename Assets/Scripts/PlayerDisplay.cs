using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public Image schoolEffect, placeEffect;

    public TMP_InputField avgText;
    public TMP_InputField homerunText;
    public TMP_InputField disciplineText;

    public TextMeshProUGUI costText;
    public TextMeshProUGUI nameText;

    //public TextMeshProUGUI numTextMesh, placeTextMesh;
    public Text numText, placeText;
    public Image colorImage;
    public Image[] stars;

    public void UpdateText(float avg, float homerun, float disc,int cost, string name)
    {
        avgText.text = avg.ToString("n3");
        homerunText.text = homerun.ToString("n3");
        disciplineText.text = disc.ToString("n3");

        costText.text = cost.ToString();
        nameText.text = name;

        
    }

    public void UpdateAttribute(TypeOfSchool school, int num, TypeOfPlace place)
    {
        colorImage.color = GetColor(school);
        numText.text = PlayerModel.GetStringForNum(num);
        placeText.text = PlayerModel.GetStringForPlace(place);
    }

    public void UpdateLevel(int level)
    {
        //Debug.Log(level);
        for (int i = 0; i < stars.Length; i++)
        {
            if (level > i)
            {
                stars[i].gameObject.SetActive(true);
            }
            else
            {
                stars[i].gameObject.SetActive(false);
            }
        }
    }

    Color GetColor(TypeOfSchool school)
    {
        switch (school)
        {
            case TypeOfSchool.none:
                return Color.black;
            case TypeOfSchool.red:
                return Color.red;
            case TypeOfSchool.blue:
                return Color.blue;
            case TypeOfSchool.green:
                return Color.green;
            default:
                return Color.clear;

        }
    }
    /*
    string GetStringForNum(int num)
    {
        switch (num)
        {
            case 0:
                return "";
            case 1:
                return "一";
            case 2:
                return "二";
            case 3:
                return "三";
            case 4:
                return "四";
            case 5:
                return "五";
            case 6:
                return "六";
            case 7:
                return "七";
            case 8:
                return "八";
            case 9:
                return "九";
            default:
                return "";
        }
    }

    string GetStringForPlace(TypeOfPlace place)
    {
        switch (place)
        {
            case TypeOfPlace.none:
                return "";
            case TypeOfPlace.east:
                return "東";
            case TypeOfPlace.west:
                return "西";
            case TypeOfPlace.south:
                return "南";
            case TypeOfPlace.north:
                return "北";
            case TypeOfPlace.foreign:
                return "外";
            default:
                return "";
        }
    }
    */
}
