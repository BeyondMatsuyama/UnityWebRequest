using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour を継承せずにコルーチンを使うためのクラス
/// 参考：https://qiita.com/Teach/items/2fa2b4fa4334a0a3e34d
/// </summary>
public class CoroutineHandler : MonoBehaviour
{
    // インスタンス
    static protected CoroutineHandler Instance;

    // インスタンス取得（DontDestroy な Object）
    static public CoroutineHandler instance
    {
        get
        {
            if (Instance == null)
            {
                GameObject obj = new GameObject("CoroutineHandler");
                DontDestroyOnLoad(obj);
                Instance = obj.AddComponent<CoroutineHandler>();
            }
            return Instance;
        }
    }

    /// <summary>
    /// オブジェクトが非アクティブまたは削除される際の処理（解放）
    /// </summary>
    public void OnDisable()
    {
        if(Instance)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }

    /// <summary>
    /// コルーチンの起動
    /// </summary>
    /// <param name="coroutine">起動させるコルーチン</param>
    /// <returns>コルーチン</returns>
    static public Coroutine StartStaticCoroutine(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }

}
