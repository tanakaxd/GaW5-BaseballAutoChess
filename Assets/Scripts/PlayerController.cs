using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerModel defaultModel;
    public PlayerModel baseModel;
    public PlayerModelDataBase dataBase;
    public PlayerStatsController stats;
    public AbilityValueCharacterizer characterizer;
    public PlayerDisplay display;
    public PlayerBatting batting;

    public bool useRandomModel;

    private float average;
    private float homerun;
    private float discipline;

    [HideInInspector] public float modifiedAverage;
    [HideInInspector] public float modifiedHomerun;
    [HideInInspector] public float modifiedDiscipline;

    [HideInInspector]public TypeOfSchool school;
    [HideInInspector]public int num;
    [HideInInspector]public TypeOfPlace place;
    [HideInInspector] public int ID;
    [HideInInspector] public int grade;
    [HideInInspector] public string name;

    [HideInInspector] public int level;

    private bool isSchoolActive;
    //private bool isSchoolSuperActive;
    private bool isPlaceActive;


    #region event
    public delegate void OnPlayerAbilityValueChange(float average, float homerun,float discipline);
    public event OnPlayerAbilityValueChange onPlayerAbilityValueChange;

    //public delegate void OnPlayerModelLoaded();
    //public event OnPlayerModelLoaded onPlayerModelLoaded;
    #endregion

    public UnityEvent onPlayerModelLoaded;
    public RectTransform schoolRect;
    public RectTransform placeRect;

    void Start()
    {
        onPlayerAbilityValueChange += characterizer.Refresh;
        //onPlayerAbilityValueChange += display.UpdateText;
        GeneratePlayer();
    }

    void GeneratePlayer()
    {
        if (useRandomModel)
        {
            LoadModel(dataBase.GetRandomModel());
        }
        else if(baseModel!=null)
        {
            LoadModel(baseModel);
        }
        else
        {
            LoadModel(defaultModel);
        }

    }

    /*
    public void ApplyTextToModel()
    {
        average = float.Parse(avgText.text);
        homerun = float.Parse(homerunText.text);
        discipline = float.Parse(disciplineText.text);

        onPlayerAbilityValueChange?.Invoke(average, homerun, discipline);
    }
    */



    public void LoadModel(PlayerModel model)
    {
        average = model.GetAverage();
        homerun = model.GetHomerun();
        discipline = model.GetDisc();
        num = model.num;
        school = model.school;
        place = model.place;
        ID = model.ID;
        grade = model.grade;
        name = model.name;

        level = 1;

        modifiedAverage = average;
        modifiedHomerun = homerun;
        modifiedDiscipline = discipline;

        display.UpdateText(average,homerun,discipline,grade,name);
        display.UpdateAttribute(school, num, place);
        display.UpdateLevel(level);

        onPlayerAbilityValueChange?.Invoke(average, homerun, discipline);
        onPlayerModelLoaded?.Invoke();
    }

    private void SetEntityAbility(string variableName, float value)
    {
        switch (variableName)
        {
            case "average":
                average = value;
                break;
            case "homerun":
                homerun = value;
                break;
            case "discipline":
                discipline = value;
                break;
            default:
                break;
        }
        display.UpdateText(average, homerun, discipline, grade, name);
        onPlayerAbilityValueChange?.Invoke(average, homerun, discipline);
    }

    public void SetAuraForSchool(Color color)
    {
        display.schoolEffect.color = color;
    }

    public void SetAuraForPlace(Color color)
    {
        display.placeEffect.color = color;
    }

    public void SetSchoolActive(bool active)
    {
        isSchoolActive = active;
    }
    public void SetPlaceActive(bool active)
    {
        isPlaceActive = active;
    }

    public void UpdateSchoolModifier()
    {
        //Debug.Log("UpdateSchoolModifier called");
        switch (school)
        {
            case TypeOfSchool.red:
                modifiedHomerun = isSchoolActive ? homerun * 1.1f : homerun;
                break;
            case TypeOfSchool.blue:
                modifiedAverage = isSchoolActive ? average * 1.1f : average;
                break;
            case TypeOfSchool.green:
                modifiedDiscipline = isSchoolActive ? discipline * 1.1f : discipline;
                break;
            default:
                break;


        }
    }

    public void LevelUp()
    {
        if (level >= 3)
            return;

        level++;
        UpdateLevelModifier();
        display.UpdateLevel(level);
        UpdateSchoolModifier();
        Debug.Log(level);

        onPlayerAbilityValueChange?.Invoke(average, homerun, discipline);
    }
    private void UpdateLevelModifier()
    {
        average *= GetLevelModifier();
        homerun *= GetLevelModifier();
        discipline *= GetLevelModifier();
    }

    private float GetLevelModifier()
    {
        switch (level)
        {
            case 1:
                return 1;
            case 2:
                return 1.5f;
            case 3:
                return 2;
            default:
                return 1;
        }
    }

public void Batting(MatchManager matchManager)
    {
        batting.Batting(matchManager, this);
    }

    /*
    void ManageRunner(ref bool[] runners, int typeOfHit)
    {
        switch (typeOfHit)
        {
            case 4:
                PushRunner(ref runners, 4);
                goto case 1;
            case 1:
                PushRunner(ref runners, 1);
                break;
            default:
                break;
        }
    }

    int PushRunner(ref bool[] runners, int typeOfHit)
    {

    }
    */
}
