using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public MatchManager matchManager;
    //public TextMeshProUGUI result;
    public TextMeshProUGUI avgScore;


    public IntVariable money;
    public IntVariable startingMoney;
    public IntVariable matches;
    public FloatVariable fame;

    public int consecutiveMatches;
    public bool reset;

    private int totalScore;
    private List<int> scoreHistory;
    //private int totalEnemyScore;

    private void Awake()
    {
        //Debug.unityLogger.logEnabled = false;

        //result.text = "";
        scoreHistory = new List<int>();
        money.SetValue(startingMoney);
        matches.SetValue(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Simulate()
    {
        //result.text = "";
        totalScore = 0;

        matchManager.ConfirmOrder();

        if (reset)
        {
            for (int i = 0; i < matchManager.order.Length; i++)
            {
                matchManager.order[i].stats.Init();
            }
        }

        for (int i = 0; i < consecutiveMatches; i++)
        {
            //innning by innning version
            StartCoroutine(matchManager.ExcuteMatch());
            scoreHistory.Add(matchManager.totalRuns);


            //instant version
            //scoreHistory.Add(matchManager.ExcuteMatch());
            //yield return null;
            yield return new WaitForSeconds(0.3f);

        }

        //avgScore.text = ((float)totalScore / matches).ToString();
    }

    public void SimulateWrapper()
    {
        StartCoroutine(Simulate());
    }
}
