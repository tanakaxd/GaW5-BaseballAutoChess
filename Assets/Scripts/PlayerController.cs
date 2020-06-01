using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public PlayerModel defaultModel;
    public PlayerModel baseModel;
    public PlayerModelDataBase dataBase;
    public PlayerStatsController stats;
    public AbilityValueCharacterizer characterizer;

    public TMP_InputField avgText;
    public TMP_InputField homerunText;
    public TMP_InputField disciplineText;

    public bool useRandomModel;

    private float average;
    private float homerun;
    private float discipline;

    #region event
    public delegate void OnPlayerAbilityValueChange(float average, float homerun,float discipline);
    public event OnPlayerAbilityValueChange onPlayerAbilityValueChange;

    //public delegate void OnPlayerModelLoaded();
    //public event OnPlayerModelLoaded onPlayerModelLoaded;
    #endregion

    public UnityEvent onPlayerModelLoaded;


    void Start()
    {
        onPlayerAbilityValueChange += characterizer.Refresh;
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

    public void ApplyTextToModel()
    {
        average = float.Parse(avgText.text);
        homerun = float.Parse(homerunText.text);
        discipline = float.Parse(disciplineText.text);

        onPlayerAbilityValueChange?.Invoke(average, homerun, discipline);
    }

    void ApplyModelToText()
    {
        avgText.text = average.ToString("n3");
        homerunText.text = homerun.ToString("n3");
        disciplineText.text = discipline.ToString("n3");
    }

    public void LoadModel(PlayerModel model)
    {
        average = model.average;
        homerun = model.homerun;
        discipline = model.discipline;
        ApplyModelToText();

        onPlayerAbilityValueChange?.Invoke(average, homerun, discipline);

        onPlayerModelLoaded?.Invoke();
    }

    public void Batting(ref int outs, ref int runs, ref bool[] runners)
    {
        if (Random.value < discipline)
        {
            ManageRunner(ref runners,ref runs, 1);
            Debug.Log(this.transform.name+": BB");
            stats.ChangeBB(1);
        }
        else
        {
            if (Random.value < average)
            {
                if (Random.value < homerun)
                {
                    ManageRunner(ref runners, ref runs, 4);
                    Debug.Log(this.transform.name+": HR");
                    stats.ChangeHR(1);


                }
                else
                {
                    ManageRunner(ref runners, ref runs, 1);
                    Debug.Log(this.transform.name+": single");

                }
                stats.ChangeH(1);
            }
            else
            {
                outs++;
                Debug.Log(this.transform.name+": out");

            }
        }
        stats.ChangePA(1);
    }

    void ManageRunner(ref bool[] runners, ref int runs, int typeOfHit)
    {
        for (int i = 0; i < typeOfHit; i++)
        {
            if (runners[2] == true)
            {
                runs++;
                stats.ChangeRBI(1);

            }
            runners[2] = runners[1];
            runners[1] = runners[0];
            runners[0] = i == 0 ? true : false;
        }
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
