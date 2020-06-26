using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyPlayerController : MonoBehaviour
{
    public PlayerModel defaultModel;
    public PlayerModel baseModel;
    public PlayerModelDataBase dataBase;
    //public PlayerStatsController stats;
    public AbilityValueCharacterizer characterizer;

    public FloatReference sigmaAVG, sigmaHR, sigmaDiscipline;
    public FloatVariable fame;

    public bool useRandomModel;
    public bool useRandomlyGenerated;

    private float average;
    private float homerun;
    private float discipline;

    #region event
    public delegate void OnPlayerAbilityValueChange(float average, float homerun, float discipline);
    public event OnPlayerAbilityValueChange onPlayerAbilityValueChange;
    #endregion

    void Start()
    {
        onPlayerAbilityValueChange += characterizer.Refresh;
        GeneratePlayer();
    }

    public void GeneratePlayer()
    {
        if (useRandomlyGenerated)
        {
            GenerateRandom(dataBase.GetRandomModelLower((int)Mathf.Clamp(fame.Value/8,1f,5f)));
        }
        else if (useRandomModel)
        {
            LoadModel(dataBase.GetRandomModel());
        }
        else if (baseModel != null)
        {
            LoadModel(baseModel);
        }
        else
        {
            LoadModel(defaultModel);
        }

        //ApplyModelToText();
    }
    /*
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
    */

    void LoadModel(PlayerModel model)
    {
        average = model.GetAverage();
        homerun = model.GetHomerun();
        discipline = model.GetDisc();

        onPlayerAbilityValueChange?.Invoke(average, homerun, discipline);
    }

    void GenerateRandom(PlayerModel model)
    {
        average = (float)MyRandom.RandomGaussianUnity(model.GetAverage(),sigmaAVG.Value);
        homerun = (float)MyRandom.RandomGaussianUnity(model.GetHomerun(), sigmaHR);
        discipline = (float)MyRandom.RandomGaussianUnity(model.GetDisc(), sigmaDiscipline);

        onPlayerAbilityValueChange?.Invoke(average, homerun, discipline);
    }

    public void Batting(ref int outs, ref int runs, ref bool[] runners)
    {
        if (Random.value < discipline)
        {
            ManageRunner(ref runners, ref runs, 1);
            //Debug.Log(this.transform.name + ": BB");
            //stats.ChangeBB(1);
        }
        else
        {
            if (Random.value < average)
            {
                if (Random.value < homerun)
                {
                    ManageRunner(ref runners, ref runs, 4);
                    //Debug.Log(this.transform.name + ": HR");
                    //stats.ChangeHR(1);


                }
                else
                {
                    ManageRunner(ref runners, ref runs, 1);
                    //Debug.Log(this.transform.name + ": single");

                }
                //stats.ChangeH(1);
            }
            else
            {
                outs++;
                //Debug.Log(this.transform.name + ": out");

            }
        }
        //stats.ChangePA(1);
    }

    void ManageRunner(ref bool[] runners, ref int runs, int typeOfHit)
    {
        for (int i = 0; i < typeOfHit; i++)
        {
            if (runners[2] == true)
            {
                runs++;
                //stats.ChangeRBI(1);

            }
            runners[2] = runners[1];
            runners[1] = runners[0];
            runners[0] = i == 0 ? true : false;
        }
    }

}
