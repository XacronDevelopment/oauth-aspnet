
namespace Constants
{
    public static class Paths
    {
        #region non-Public Members        

        /// <summary>
        /// OAuth Callback URL for ASP.NET Core Implicit Grant Client
        /// </summary>
        private const string ImplicitGrantClient2CallBackPath = "http://localhost:9581/Home/SignIn";

        /// <summary>
        /// OAuth Callback URL for ASP.NET Core Implicit Grant Client
        /// </summary>
        private const string ImplicitGrantClient1CallBackPath = "http://localhost:9500/Home/SignIn";

        /// <summary>
        /// ASP.NET 4.x Resource Server
        /// </summary>
        private const string ResourceServer1BaseAddress = "http://localhost:31701";

        /// <summary>
        /// ASP.NET Core Resource Server
        /// </summary>
        private const string ResourceServer2BaseAddress = "http://localhost:62619";

        #endregion

        #region Public Members

        /// <summary>
        /// AuthorizationServer project should run on this URL
        /// </summary>
        public const string AuthorizationServerBaseAddress = "http://localhost:11625";

        /// <summary>
        /// AuthorizationCodeGrant project should be running on this URL.
        /// </summary>
        public const string AuthorizeCodeCallBackPath = "http://localhost:38500/";

        /// <summary>
        /// ImplicitGrant project should be running on this URL
        /// </summary>
        public const string ImplicitGrantCallBackPath = ImplicitGrantClient2CallBackPath;

        /// <summary>
        /// ResourceServer project should run on this URL
        /// </summary>
        public const string ResourceServerBaseAddress = ResourceServer2BaseAddress;

        public const string AuthorizePath = "/OAuth/Authorize";
        public const string TokenPath = "/OAuth/Token";
        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";
        public const string MePath = "/api/Me";

        #endregion
    }
}