using DG.Tweening;
using System;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public Transform orderTransform;
    public Transform enemyOrderTransform;

    public BoolVariable isEastBuffOn, isWestBuffOn, isSouthBuffOn, isNorthBuffOn;

    [HideInInspector] public PlayerController[] order;
    [HideInInspector] public EnemyPlayerController[] enemyOrder;

    private void Start()
    {
        CheckOrder();
    }

    public void ConfirmOrder()
    {
        order = orderTransform.GetComponentsInChildren<PlayerController>();
        //Debug.Log(order.Length);
        enemyOrder = enemyOrderTransform.GetComponentsInChildren<EnemyPlayerController>();
    }

    public void CheckOrder()
    {
        order = orderTransform.GetComponentsInChildren<PlayerController>();

        Debug.Log("CheckOrder called");

        //まずリセット
        SetActiveAllPlayerAura(false);
        SetActiveAllPlayerFlagForSchoolAndPlace(false);

        //シナジーの検証
        //結果を格納するリスト
        //List<Dictionary<TypeOfColor, List<int[]>>> result = new List<Dictionary<TypeOfColor, List<int[]>>>();
        //e.g. [{red:[[2,3,4],[6,7]]},{blue:[[]]},{green:[[9,1]]}]

        //赤青緑について3つ以上の連なる並びがあるか
        //色ごとにやる方法
        //色と数が連続している要素のindexの配列を送れればよい => red, [7,8,0]
        foreach (TypeOfSchool school in Enum.GetValues(typeof(TypeOfSchool)))
        {
            if (school == TypeOfSchool.none)
                continue;

            Debug.Log(school.ToString());

            //色ごとに数字を格納する仮の配列を作る
            int[] nums = new int[9];

            //まず赤いエレメントを見つける
            for (int i = 0; i < order.Length; i++)
            {
                if (order[i].school == school)
                {
                    nums[i] = order[i].num;
                }
                else
                {
                    nums[i] = -100;
                }
            }

            //次の項との数字に連続性があるかどうかのbool配列を作る
            bool[] isNextContinuous = new bool[9];
            for (int i = 0; i < order.Length; i++)
            {
                isNextContinuous[i] = nums[i] + 1 == nums[CycleOrder(i)];
            }

            //送り出す配列を作って、引数として渡す
            int round = 1; //連続している限りチェックするので一周を少しオーバーする可能性がある
            for (int i = 0; i < order.Length; i++)
            {
                //if (!isNextContinuous[i])
                //    continue;

                //連続性がどれだけつながっているかチェックする
                //int startNum = nums[i];
                int startIndex = i;
                int row = 1;
                while (isNextContinuous[i])
                {
                    row++;
                    i++;

                    if (i >= 9) //9番と1番が連続している場合の無限ループを回避
                    {
                        round++;
                        i = 0;
                    }
                }

                //連続が3以上かチェック
                if (row >= 3)
                {
                    //送り出す配列を作る
                    int[] continuousIndex = new int[row]; //[3,4,5]or[8,0,1,2]
                    for (int j = 0; j < continuousIndex.Length; j++)
                    {
                        int index = startIndex + j;
                        if (index >= 9)
                        {
                            index = index - 9;
                        }
                        continuousIndex[j] = index;
                    }

                    //UIに反映。Auraの処理はSetActive(true)にすれば終わり
                    UpdateSchoolAura(school, continuousIndex);
                    foreach (int num in continuousIndex)
                    {
                        Debug.Log(num);
                    }

                    //効果を出すためにplayerControllerのフラグを立てる
                    //Auraの時と違って、フラグを立てた後さらに再計算を促す必要がある
                    SetSchoolFlag(continuousIndex);
                }

                if (round >= 2)
                {
                    Debug.Log("school infinite loop avoided!" + (round >= 2 ? round : row));
                    break;
                }
            }
        }

        //内部計算に反映
        //playercontrollerがschoolとフラグはすでに持っている
        UpdateSchoolBuff();

        //placeが重複しているか
        foreach (TypeOfPlace place in Enum.GetValues(typeof(TypeOfPlace)))
        {
            if (place == TypeOfPlace.none)
                continue;

            Debug.Log(place.ToString());

            //placeごとにboolを格納する仮の配列を作る
            bool[] bools = new bool[9];

            //まずeastを見つける
            for (int i = 0; i < order.Length; i++)
            {
                bools[i] = order[i].place == place;
            }

            //次の項との連続性があるかどうかのbool配列を作る
            bool[] isNextContinuous = new bool[9];
            for (int i = 0; i < order.Length; i++)
            {
                isNextContinuous[i] = bools[i] && bools[CycleOrder(i)];
            }

            //送り出す配列を作って、引数として渡す
            int round = 1; //連続している限りチェックするので一周を少しオーバーする可能性がある
            for (int i = 0; i < order.Length; i++)
            {
                //if (!isNextContinuous[i])
                //    continue;

                //連続性がどれだけつながっているかチェックする
                int startIndex = i;
                int row = 1;
                while (isNextContinuous[i])
                {
                    row++;
                    i++;

                    if (i >= 9)
                    {
                        round++;//9番と1番が連続している場合の無限ループを回避
                        i = 0;
                    }

                    if (row >= 9)
                    {
                        break;//ひたすら連続している場合の無限ループを回避
                    }
                }

                //連続が3以上かチェック
                if (row >= 3)
                {
                    //送り出す配列を作る
                    int[] continuousIndex = new int[row]; //[3,4,5]or[8,0,1,2]
                    for (int j = 0; j < continuousIndex.Length; j++)
                    {
                        int index = startIndex + j;
                        if (index >= 9)
                        {
                            index = index - 9;
                        }
                        continuousIndex[j] = index;
                    }

                    //UIに反映。Auraの処理はSetActive(true)にすれば終わり
                    UpdatePlaceAura(place, continuousIndex);
                    foreach (int num in continuousIndex)
                    {
                        Debug.Log(num);
                    }

                    SetActivePlaceBuff(place, true);
                }

                if (round >= 2 || row >= 9)
                {
                    Debug.Log("place infinite loop avoided!" + (round >= 2 ? round : row));
                    break;
                }
            }
        }
    }

    private void SetActiveAllPlayerAura(bool active)
    {
        for (int i = 0; i < order.Length; i++)
        {
            order[i].SetAuraForSchool(active ? Color.black : Color.white);
            order[i].SetAuraForPlace(active ? Color.black : Color.white);
        }
    }

    private void UpdateSchoolAura(TypeOfSchool school, int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            RectTransform rect = order[indexes[i]].schoolRect;
            Sequence seq = DOTween.Sequence().Append(rect.DOScale(
                 new Vector2(1.2f, 1.2f),  //終了時点のScale
                 0.5f       //時間
             )).Append(rect.DOScale(
                 new Vector2(1, 1),  //終了時点のScale
                 0.5f       //時間
             ));

            //バラバラに書く方法
            //Sequence seq = DOTween.Sequence();

            //seq.Append(rect.DOScale(
            //    new Vector2(1.2f, 1.2f),　　//終了時点のScale
            //    0.5f 　　　　　　//時間
            //));
            //seq.Append(rect.DOScale(
            //    new Vector2(1,1),　　//終了時点のScale
            //    0.5f 　　　　　　//時間
            //));
            switch (school)
            {
                case TypeOfSchool.red:
                    order[indexes[i]].SetAuraForSchool(Color.black);
                    seq.Play();
                    break;

                case TypeOfSchool.blue:

                    order[indexes[i]].SetAuraForSchool(Color.black);
                    seq.Play();

                    break;

                case TypeOfSchool.green:
                    order[indexes[i]].SetAuraForSchool(Color.black);
                    seq.Play();

                    break;
                defalut:
                    break;
            }
        }
    }

    private void UpdatePlaceAura(TypeOfPlace place, int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            RectTransform rect = order[indexes[i]].placeRect;
            Sequence seq = DOTween.Sequence().Append(rect.DOScale(
                 new Vector2(1.2f, 1.2f),  //終了時点のScale
                 0.5f       //時間
             )).Append(rect.DOScale(
                 new Vector2(1, 1),  //終了時点のScale
                 0.5f       //時間
             ));
            switch (place)
            {
                case TypeOfPlace.east:
                    order[indexes[i]].SetAuraForPlace(Color.black);
                    seq.Play();
                    break;

                case TypeOfPlace.west:
                    order[indexes[i]].SetAuraForPlace(Color.black);
                    seq.Play();
                    break;
                case TypeOfPlace.south:
                    order[indexes[i]].SetAuraForPlace(Color.black);
                    seq.Play();
                    break;

                case TypeOfPlace.north:
                    order[indexes[i]].SetAuraForPlace(Color.black);
                    seq.Play();
                    break;
                defalut:
                    break;
            }
        }
    }

    private void SetActiveAllPlayerFlagForSchoolAndPlace(bool active)
    {
        for (int i = 0; i < order.Length; i++)
        {
            order[i].SetSchoolActive(active);
            order[i].SetPlaceActive(active);
        }
    }

    private void SetSchoolFlag(int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            order[indexes[i]].SetSchoolActive(true);
        }
    }

    //個々のplyaercontrollerがplaceのbuff情報を持っている必要が今のところない
    //onの場合、auraだけ外からいじればよい
    private void SetPlaceFlag(int[] indexes)
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            order[indexes[i]].SetPlaceActive(true);
        }
    }

    private void UpdateSchoolBuff()
    {
        for (int i = 0; i < order.Length; i++)
        {
            order[i].UpdateSchoolModifier();
        }
    }

    private void SetActivePlaceBuff(TypeOfPlace place, bool isActive)
    {
        switch (place)
        {
            case TypeOfPlace.east:
                isEastBuffOn.SetValue(isActive);
                break;

            case TypeOfPlace.west:
                isWestBuffOn.SetValue(isActive);
                break;

            case TypeOfPlace.south:
                isSouthBuffOn.SetValue(isActive);
                break;

            case TypeOfPlace.north:
                isNorthBuffOn.SetValue(isActive);
                break;

            default:
                break;
        }
    }

    private bool CheckContinuity(int index, int[] array)
    {
        return array[index] + 1 == array[CycleOrder(index)];
    }

    private int CycleOrder(int num)
    {
        num++;
        if (num >= 9)
            num = 0;
        return num;
    }
}

/*
//任意の打順のやつと次のやつとの関連を調べる関数で再帰処理
//没案。抜ける処理がまだ
for (int i = 0; i < order.Length; )
{
    if (nums[i] <= 0)
    {
        i++;
        continue;
    }

    int startNum = nums[i];
    int startIndex = i;

    int row = 0;
    bool isContinuous;
    do
    {
        isContinuous = CheckContinuity(i, nums);
        i++;
        row++;
    }
    while (isContinuous);

    if (row <= 2)
        continue;

    int[] rows = new int[row];
    for (int j = 0; j < rows.Length; j++)
    {
        rows[j] = nums[startIndex + j];
    }
}
*/