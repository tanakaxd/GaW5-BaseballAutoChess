﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DrawnPlayerDisplay : MonoBehaviour
{

    public TextMeshProUGUI numText, placeText, costText;
    public Image colorImage;


    public void UpdateAttribute(TypeOfSchool school, int num, TypeOfPlace place,int cost)
    {
        colorImage.color = GetColor(school);
        numText.text = PlayerModel.GetStringForNum(num);
        placeText.text = PlayerModel.GetStringForPlace(place);
        costText.text = cost.ToString();
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
                return "I";
            case 2:
                return "II";
            case 3:
                return "III";
            case 4:
                return "IV";
            case 5:
                return "V";
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
                return "E";
            case TypeOfPlace.west:
                return "W";
            case TypeOfPlace.south:
                return "S";
            case TypeOfPlace.north:
                return "N";
            case TypeOfPlace.foreign:
                return "F";
            default:
                return "";
        }
    }
    */
}