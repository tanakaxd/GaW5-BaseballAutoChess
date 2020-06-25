using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CsvImporter))]
public class CSVImpoterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var csvImpoter = target as CsvImporter;
        DrawDefaultInspector();

        if (GUILayout.Button("プレイヤーモデルデータの作成"))
        {
            // Debug.Log("敵データの作成ボタンが押された");
            SetCsvDataToScriptableObject(csvImpoter);
        }
    }

    private void SetCsvDataToScriptableObject(CsvImporter csvImporter)
    {
        // ボタンを押されたらパース実行
        if (csvImporter.csvFile == null)
        {
            Debug.LogWarning(csvImporter.name + " : 読み込むCSVファイルがセットされていません。");
            return;
        }

        // csvファイルをstring形式に変換
        string csvText = csvImporter.csvFile.text;

        // 改行ごとにパース
        string[] afterParse = csvText.Split('\n');

        // ヘッダー行を除いてインポート i=1
        for (int i = 1; i < afterParse.Length; i++)
        {
            string[] parseByComma = afterParse[i].Split(',');

            //0はname
            int column = 0;

            // 先頭の列が空であればその行は読み込まない
            if (parseByComma[column] == "")
            {
                continue;
            }

            // 行数をIDとしてファイルを作成
            string fileName = parseByComma[column] + ".asset";
            string path = "Assets/ImportedData/" + fileName;

            // PlayerModelのインスタンスをメモリ上に作成
            var playerModel = CreateInstance<PlayerModel>();

            column++;
            playerModel.average = float.Parse(parseByComma[column]);

            column++;
            playerModel.homerun = float.Parse(parseByComma[column]);

            column++;
            playerModel.discipline = float.Parse(parseByComma[column]);

            column++;
            playerModel.grade = int.Parse(parseByComma[column]);

            column++;
            playerModel.ID = int.Parse(parseByComma[column]);

            column++;
            playerModel.num = int.Parse(parseByComma[column]);

            column++;
            //Debug.Log(parseByComma[column]);
            switch (parseByComma[column])
            {
                case "青":
                    playerModel.school = TypeOfSchool.blue;
                    break;

                case "赤":
                    playerModel.school = TypeOfSchool.red;
                    break;

                case "緑":
                    playerModel.school = TypeOfSchool.green;
                    break;

                default:
                    playerModel.school = TypeOfSchool.none;
                    break;
            }

            column++;
            //Debug.Log(parseByComma[column]);
            string place = parseByComma[column];
            //Debug.Log(place);

            //switch (parseByComma[column])
            //{
            //    case "east":
            //        playerModel.place = TypeOfPlace.east;
            //        break;
            //    case "west":
            //        playerModel.place = TypeOfPlace.west;
            //        break;
            //    case "south":
            //        playerModel.place = TypeOfPlace.south;
            //        break;
            //    case "north":
            //        playerModel.place = TypeOfPlace.north;
            //        break;
            //    case "foreign":
            //        playerModel.place = TypeOfPlace.foreign;
            //        break;
            //    default:
            //        Debug.Log("invalid string");
            //        break;

            //}

            //原因不明のエラーに悩まされた
            //最後に空白の列を入れて、行の最後が,になるようにしたらできた
            //原因はよくわからないが、例えば"south"!="south"として扱われる
            //最後の行のみ問題ないことから\nが何かしらの原因だと思う

            if (place == "east")
            {
                playerModel.place = TypeOfPlace.east;
            }
            else if (place == "west")
            {
                playerModel.place = TypeOfPlace.west;
            }
            else if (place == "south")
            {
                playerModel.place = TypeOfPlace.south;
            }
            else if (place == "north")
            {
                playerModel.place = TypeOfPlace.north;
            }
            else
            {
                playerModel.place = TypeOfPlace.foreign;
            }

            //Debug.Log(playerModel.place);

            // インスタンス化したものをアセットとして保存
            var asset = (PlayerModel)AssetDatabase.LoadAssetAtPath(path, typeof(PlayerModel));
            if (asset == null)
            {
                // 指定のパスにファイルが存在しない場合は新規作成
                AssetDatabase.CreateAsset(playerModel, path);
            }
            else
            {
                // 指定のパスに既に同名のファイルが存在する場合は更新
                EditorUtility.CopySerialized(playerModel, asset);
                AssetDatabase.SaveAssets();
            }
            AssetDatabase.Refresh();
        }
        Debug.Log(csvImporter.name + " : プレイヤーモデルデータの作成が完了しました。");
    }
}