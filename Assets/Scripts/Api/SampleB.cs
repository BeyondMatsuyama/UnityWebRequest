using System;
using System.Collections.Generic;

namespace Api
{
    /// <summary>
    /// API サンプルB
    /// </summary>
    public class SampleB : Web.ApiBase
    {
        public const string Name = "Test/SampleB";

        /// <summary>
        /// リクエストパラメータ
        /// </summary>
        [Serializable]
        public struct Request
        {
            public string userId;
            public List<int> values;
        }
        public Request request;

        /// <summary>
        /// レスポンスパラメータ
        /// </summary>
        [Serializable]
        public struct Response
        {
            public int count;
            public List<int> values;
        }
        public Response response;
    }
}
