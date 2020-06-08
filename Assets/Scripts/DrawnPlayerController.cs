using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnPlayerController : MonoBehaviour
{
    public AbilityValueCharacterizer characterizer;
    public DrawnPlayerDisplay display;

    private float average;
    private float homerun;
    private float discipline;

    [HideInInspector] public TypeOfSchool school;
    [HideInInspector] public int num;
    [HideInInspector] public TypeOfPlace place;
    [HideInInspector] public int cost;

    [HideInInspector]public int ID;


    void LoadModel(PlayerModel model)
    {
        average = model.GetAverage();
        homerun = model.GetHomerun();
        discipline = model.GetDisc();
        num = model.num;
        school = model.school;
        place = model.place;
        cost = model.grade;

        ID = model.ID;
    }

    public void DisplayPlayer(PlayerModel model)
    {
        Debug.Log("displayplayer");
        LoadModel(model);
        characterizer.Refresh(average,homerun,discipline);
        display.UpdateAttribute(school, num, place,cost) ;
    }
}
