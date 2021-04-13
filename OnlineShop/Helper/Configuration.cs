//using PayPal.Api;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace OnlineShop.Helper
//{
//    public static class Configuration
//    {
//        public readonly static string ClientId;
//        public readonly static string ClientSecret;

//        static Configuration()
//        {
//            var config = GetConfig();
//            //ClientId = config["clientId"];
//            //ClientSecret = config["clientSecret"];
//            ClientId = "AXayTyXU7Z0NAlB4MfdwYY_ebJ91qNar1OWc6XlR1e1YPhFk8SmwreIsi4faQh5B0HxOznkHAlLXG6SH";
//            ClientSecret = "EB9a_ekdXlOjVX02kx7NrvjswuWKASHhVEpDTDKsk6hUcyaRayxakf4npFMYYi_X3N91vqwuBzIOAFk-";
//        }
//        public static Dictionary<string, string> GetConfig()
//        {
//            return PayPal.Api.ConfigManager.Instance.GetProperties();
//        }
//        private static string GetAccessToken()
//        {
//            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret,
//            GetConfig()).GetAccessToken();
//            return accessToken;
//        }
//        public static APIContext GetAPIContext()
//        {
//            APIContext apiContext = new APIContext(GetAccessToken());
//            apiContext.Config = GetConfig();
//            return apiContext;
//        }
//    }
//}