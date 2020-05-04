using System;

namespace Api
{
    /// <summary>
    /// API サンプルA
    /// </summary>
    public class SampleA : Web.ApiBase
    {
        public const string Name = "Test/SampleA";

        /// <summary>
        /// リクエストパラメータ
        /// </summary>
        [Serializable]
        public struct Request
        {
            public string userId;
            public string password;
        }
        public Request request;

        /// <summary>
        /// レスポンスパラメータ
        /// </summary>
        [Serializable]
        public struct Response
        {
            public string name;
            public int age;
        }
        public Response response;
    }
}