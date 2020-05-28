using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public MatchManager matchManager;
    public TextMeshProUGUI result;
    public TextMeshProUGUI avgScore;
    public int matches;
    public bool reset;

    private int totalScore;

    private void Awake()
    {
        Debug.unityLogger.logEnabled = false;

        result.text = "";
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
        result.text = "";
        totalScore = 0;

        matchManager.ConfirmOrder();

        if (reset)
        {
            for (int i = 0; i < matchManager.order.Length; i++)
            {
                matchManager.order[i].stats.Init();
            }
        }

        for (int i = 0; i < matches; i++)
        {
            totalScore += matchManager.ExcuteMatch();
            yield return null;
            //yield return new WaitForSeconds(0.3f);
        }

        avgScore.text = ((float)totalScore / matches).ToString();
    }

    public void SimulateWrapper()
    {
        StartCoroutine(Simulate());
    }
}
