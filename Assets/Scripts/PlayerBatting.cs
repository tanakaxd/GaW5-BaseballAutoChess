using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBatting : MonoBehaviour
{
    public void Batting(MatchManager matchManager, PlayerController controller)
    {
        //Debug.Log(homerun);
        //Debug.Log(modifiedHomerun);
        if (Random.value < controller.modifiedDiscipline)
        {
            ManageRunner(matchManager,controller, 1);
            //Debug.Log(this.transform.name+": BB");
            controller.stats.ChangeBB(1);
        }
        else
        {
            if (Random.value < controller.modifiedAverage)
            {
                if (Random.value < controller.modifiedHomerun)
                {
                    ManageRunner(matchManager,controller, 4);
                    //Debug.Log(this.transform.name+": HR");
                    controller.stats.ChangeHR(1);


                }
                else
                {
                    ManageRunner(matchManager,controller, 1);
                    //Debug.Log(this.transform.name+": single");

                }
                controller.stats.ChangeH(1);
            }
            else
            {
                matchManager.outs++;
                //Debug.Log(this.transform.name+": out");

            }
        }
        controller.stats.ChangePA(1);
    }

    void ManageRunner(MatchManager matchManager, PlayerController controller, int typeOfHit)
    {
        for (int i = 0; i < typeOfHit; i++)
        {
            if (matchManager.runnersOnBases[2] == true)
            {
                matchManager.runs++;
                controller.stats.ChangeRBI(1);

            }
            matchManager.runnersOnBases[2] = matchManager.runnersOnBases[1];
            matchManager.runnersOnBases[1] = matchManager.runnersOnBases[0];
            matchManager.runnersOnBases[0] = i == 0 ? true : false;
        }
    }
}
