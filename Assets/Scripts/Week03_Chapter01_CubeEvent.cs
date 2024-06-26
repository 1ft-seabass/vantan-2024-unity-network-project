using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;       // IEnumerator のための参照
using UnityEngine.Networking;   // UnityWebRequest のための参照
using System;                   // Serializable のための参照
using System.Text;              // Encoding のための参照

public class Week03_Chapter01_CubeEvent : MonoBehaviour, IPointerClickHandler
{
    // 受信した JSON データを Unity で扱うデータにする ResultResponseData ベースクラス
    [Serializable]
    public class ResultResponseData
    {
        // result というプロパティ名で string 型で変換
        public string result;
    }

    // 送信する Unity データを JSON データ化する PointRequestData ベースクラス
    [Serializable]
    public class PointRequestData
    {
        // point というプロパティ名で int 型で変換
        public int point;
    }

    // アクセスする URL
    // サーバーURL + /api/post/result でアクセス
    string urlGitHub = "ここにサーバーURLを入れる";

    public void OnPointerClick(PointerEventData eventData)
    {
        // マウスクリックイベント
        // Debug.Log($"オブジェクト {this.name} がクリックされたよ！");

        // HTTP リクエストを非同期処理を待つためコルーチンとして呼び出す
        StartCoroutine("PostPointData");
    }

    // POST リクエストする本体
    IEnumerator PostPointData()
    {
        // HTTP リクエストする(POST メソッド) UnityWebRequest を呼び出し
        // アクセスする先は変数 urlGitHub で設定
        UnityWebRequest request = new UnityWebRequest(urlGitHub, "POST");


        // PointRequestData ベースクラスを器として呼び出す
        PointRequestData pointRequestData = new PointRequestData();
        // データを設定
        pointRequestData.point = 1000; // ダミーで 1000 pt のゲーム結果を送る

        // 送信データを JsonUtility.ToJson で JSON 文字列を作成
        // pointRequestData の構造に基づいて変換してくれる
        string strJSON = JsonUtility.ToJson(pointRequestData);
        Debug.Log($"strJSON : {strJSON}");
        // 送信データを Encoding.UTF8.GetBytes で byte データ化
        byte[] bodyRaw = Encoding.UTF8.GetBytes(strJSON);

        // アップロード（Unity→サーバ）のハンドラを作成
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        // ダウンロード（サーバ→Unity）のハンドラを作成
        request.downloadHandler = new DownloadHandlerBuffer();

        // JSON で送ると HTTP ヘッダーで宣言する
        request.SetRequestHeader("Content-Type", "application/json");

        // リクエスト開始
        yield return request.SendWebRequest();

        // 結果によって分岐
        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("リクエスト成功");

                // コンソールに表示
                Debug.Log($"responseData: {request.downloadHandler.text}");

                // ResultResponseData クラスで Unity で扱えるデータ化
                ResultResponseData resultResponse = JsonUtility.FromJson<ResultResponseData>(request.downloadHandler.text);

                // MessageText に結果テキスト割り当て
                GameObject.Find("MessageText").GetComponent<TextMesh>().text = resultResponse.result;

                break;
        }

    }
}