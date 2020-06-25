using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName = "PlayerModel")]
public class PlayerModel : ScriptableObject
{
    public float average;
    public float homerun;
    public float discipline;

    public float GetAverage()
    {
        return average * GetGradeModifiler();
    }
    public float GetHomerun()
    {
        return homerun * GetGradeModifiler();
    }
    public float GetDisc()
    {
        return discipline * GetGradeModifiler();
    }

    public int grade;
    public int ID;

    //mahjong
    public int num;

    public TypeOfSchool school;
    public TypeOfPlace place;


    public float GetGradeModifiler()
    {
        switch (grade)
        {
            case 1:
                return 0.6f;

            case 2:
                return 0.7f;

            case 3:
                return 0.8f;

            case 4:
                return 0.9f;

            case 5:
                return 1.0f;

            default:
                return 1.0f;
        }
    }

    public Color GetColor()
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

    public static string GetStringForNum(int num)
    {
        switch (num)
        {
            case 0:
                return "";

            case 1:
                return "1";

            case 2:
                return "2";

            case 3:
                return "3";

            case 4:
                return "4";

            case 5:
                return "5";
            case 6:
                return "6";
            case 7:
                return "7";
            case 8:
                return "8";
            case 9:
                return "9";

            default:
                return "";
        }
    }

    public static string GetStringForPlace(TypeOfPlace place)
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
}

public enum TypeOfSchool
{
    none,
    red,
    blue,
    green
}

public enum TypeOfPlace
{
    none,
    east,
    west,
    south,
    north,
    foreign
}