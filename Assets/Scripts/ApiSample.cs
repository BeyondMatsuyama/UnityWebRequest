using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiSample : MonoBehaviour
{
    private Api.SampleA         apiA;
    private Api.SampleB         apiB;
    private Web.ApiBase.Result  result;

    private void Start()
    {
        // 通信用クラス生成
        apiA = new Api.SampleA();
        apiB = new Api.SampleB();

        // API 通信
        sendApiA();
        sendApiB();
    }

    /// <summary>
    /// API サンプルA 通信
    /// </summary>
    private void sendApiA()
    {
        // エンドポイントの設定
        apiA.EndPoint = Api.SampleA.Name;
        // リクエストパラメータを設定
        apiA.request.userId = "beyondA";
        apiA.request.password = "0123abcd89";

        // 通信
        apiA.Send<Api.SampleA.Request>(ref apiA.request, result => {

            // リザルト
            if(result.isSucceeded)  // 成功
            {
                // レスポンスを展開
                apiA.response = apiA.Response<Api.SampleA.Response>();

                // 内容確認
                Debug.Log("SampleA Succeed!!");
                Debug.Log("  name : " + apiA.response.name);
                Debug.Log("  age : " + apiA.response.age);
            }
            else                    // 失敗
            {
                Debug.Log("SampleA Failed : " + result.error);
            }
        });
    }

    /// <summary>
    /// API サンプルB 通信
    /// </summary>
    private void sendApiB()
    {
        // エンドポイントの設定
        apiB.EndPoint = Api.SampleB.Name;
        // リクエストパラメータを設定
        apiB.request.userId = "beyondB";
        apiB.request.values = new List<int>() { 1, 10, 100, 1000 };

        // 通信
        apiB.Send<Api.SampleB.Request>(ref apiB.request, result => {

            // リザルト
            if (result.isSucceeded)  // 成功
            {
                // レスポンスを展開
                apiB.response = apiB.Response<Api.SampleB.Response>();

                // 内容確認
                Debug.Log("SampleB Succeed!!");
                Debug.Log("  count : " + apiB.response.count);
                foreach(var v in apiB.response.values)
                {
                    Debug.Log("  val : " + v);
                }
            }
            else                    // 失敗
            {
                Debug.Log("SampleB Failed : " + result.error);
            }
        });
    }

}
