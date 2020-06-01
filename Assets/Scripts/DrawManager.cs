using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public PoolManager pool;
    public List<DrawnPlayerController> players;
    public PlayerModelDataBase dataBase;
    public FloatVariable scountingLevel;
    public Transform drawPanel;

    //private List<int> drawnIDs = new List<int>();



    public void Draw()
    {
        //
        drawPanel.gameObject.SetActive(true);

        //idを取得
        for (int i = 0; i < players.Count; i++)
        {
            //drawnIDs.Add(pool.GetRandomModel());
            //反映
            players[i].gameObject.SetActive(true);
            players[i].transform.GetComponent<CanvasGroup>().blocksRaycasts=true;
            players[i].DisplayPlayer(pool.GetRandomModel());
        }


    }



}
