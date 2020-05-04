using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Web
{
    /// <summary>
    /// HTTP 通信基底クラス
    /// </summary>
    public class ApiBase
    {
        /// <summary>
        /// HTTP 通信の結果
        /// </summary>
        public struct Result
        {
            public bool     isSucceeded;    // true で成功
            public string   error;          // 失敗時のエラー内容

            /// <summary>
            /// 成功
            /// </summary>
            public void Suceeded()
            {
                isSucceeded = true;
                error = "";
            }

            /// <summary>
            /// 失敗
            /// </summary>
            /// <param name="error"></param>
            public void Failed(string error)
            {
                isSucceeded = false;
                this.error = error;
            }
        }

        // ベースURL
        private const string BaseUrl = "https://localhost:5001/api/";  // 適切なパスを設定してください

        // レスポンス（JSON）
        private string resJson;
        // エンドポイント
        public string EndPoint { get; set; }

        /// <summary>
        /// リクエスト（オブジェクト）を JSON に変換して HTTP（POST）通信を行う
        /// </summary>
        /// <typeparam name="T">リクエスト型</typeparam>
        /// <param name="request">リクエストのオブジェクト</param>
        /// <param name="cb">コールバック</param>    
        public void Send<T>(ref T request, Action<Result> cb)
        {
            // リクエストオブジェクトを JSON に変換（byte配列）
            string reqJson = JsonUtility.ToJson(request);
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(reqJson);

            // HTTP（POST）通信
            var url = BaseUrl + EndPoint;            
            CoroutineHandler.StartStaticCoroutine(onSend(url, postData, cb));
        }

        /// <summary>
        /// HTTP（POST）通信の実行
        /// </summary>
        /// <param name="url">接続する URL</param>
        /// <param name="postData">POST するデータ</param>
        /// <param name="cb">コールバック</param>
        /// <returns>コルーチン</returns>
        private IEnumerator onSend(string url, byte[] postData, Action<Result> cb)
        {
            // HTTP（POST）の情報を設定
            var req = new UnityWebRequest(url, "POST");
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            // API 通信（完了待ち）
            yield return req.SendWebRequest();

            // 通信結果
            Result result = new Result();
            if (req.isNetworkError ||
                 req.isHttpError)  // 失敗
            {
                // Debug.Log("Network error:" + req.error);
                result.Failed(req.error);
            }
            else                    // 成功
            {
                // Debug.Log("Succeeded:" + req.downloadHandler.text);
                resJson = req.downloadHandler.text;
                result.Suceeded();
            }
            cb(result);
        }

        /// <summary>
        /// レスポンス（JSON）からオブジェクトを生成して返す
        /// </summary>
        /// <typeparam name="T">レスポンス型</typeparam>
        /// <returns>レスポンスのオブジェクト</returns>
        public T Response<T>()
        {
            return JsonUtility.FromJson<T>(resJson);
        }

    }
}