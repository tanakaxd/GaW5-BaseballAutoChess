using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnPlayerController : MonoBehaviour
{
    public AbilityValueCharacterizer characterizer;

    private float average;
    private float homerun;
    private float discipline;

    [HideInInspector]public int ID;


    void LoadModel(PlayerModel model)
    {
        average = model.average;
        homerun = model.homerun;
        discipline = model.discipline;
        ID = model.ID;
    }

    public void DisplayPlayer(PlayerModel model)
    {
        LoadModel(model);
        characterizer.Refresh(average,homerun,discipline);
    }
}
