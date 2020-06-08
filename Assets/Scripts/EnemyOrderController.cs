using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrderController : MonoBehaviour
{
    private EnemyPlayerController[] enemyOrder;

    private void Awake()
    {
        enemyOrder = GetComponentsInChildren<EnemyPlayerController>();
    }

    public void RenewEnemy()
    {
        foreach(EnemyPlayerController enemy in enemyOrder)
        {
            enemy.GeneratePlayer();
        }
    }

}
