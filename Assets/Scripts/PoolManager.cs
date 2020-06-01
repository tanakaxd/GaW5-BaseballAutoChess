using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public PlayerModelDataBase database;
    public FloatVariable fame;

    private List<int> playerIDs = new List<int>();
    public int[] amounts; 

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < amounts[i]; j++)
            {
                playerIDs.Add(database.GetRandomID(i));

            }
        }

        //MyDebug.List<int>(playerIDs);
        
    }

    public PlayerModel GetRandomModel()
    {
        int grade = (int)Mathf.Clamp((float)MyRandom.RandomGaussianUnity(fame.Value, 1),0,5);
        return database.GetRandomModel(grade);

    }


}
