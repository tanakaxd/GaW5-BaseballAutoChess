using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModelDataBase", menuName = "PlayerModelDataBase")]
public class PlayerModelDataBase : ScriptableObject
{
    public List<PlayerModel> dataBase;

    public PlayerModel GetRandomModel()
    {
        return dataBase[Random.Range(0, dataBase.Count)];
    }

    public PlayerModel GetRandomModel(int highestGrade)
    {
        PlayerModel model=null;
        int count = 0;
        while (model == null)
        {
            if (count > 1000)
                break;
            model = dataBase[Random.Range(0, dataBase.Count)];
            if (model.grade > highestGrade)
            {
                model = null;
            }
            count++;
        }
        return model;
    }

    public int GetRandomID(int grade)
    {
        List<PlayerModel> ids = dataBase.Where(model => model.grade == grade).ToList();
        if (ids == null)
            Debug.LogError("invalid grade!!");
        return ids[Random.Range(0, ids.Count)].ID;
    }

    public PlayerModel GetModel(int ID)
    {
        PlayerModel playerModel= dataBase.Where(model => model.ID == ID).ToList()[0];
        return playerModel;
    }
}
