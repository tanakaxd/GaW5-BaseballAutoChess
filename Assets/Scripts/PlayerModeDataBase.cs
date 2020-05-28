using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModelDataBase", menuName = "PlayerModelDataBase")]
public class PlayerModelDataBase : ScriptableObject
{
    public List<PlayerModel> dataBase;

    public PlayerModel GetRandomModel()
    {
        return dataBase[Random.Range(0, dataBase.Count)];
    }
}
