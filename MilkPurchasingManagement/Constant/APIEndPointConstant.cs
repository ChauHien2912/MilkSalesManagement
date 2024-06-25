namespace WareHouseManagement.API.Constant
{
    public static class APIEndPointConstant
    {
        static APIEndPointConstant() { }

        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;


        public static class Product
        {
            public const string ProductEndpoint = ApiEndpoint + "/products";
            
        }
        public static class Order
        {
            public const string OrderEndpoint = ApiEndpoint + "/orders";

        }

        public static class User
        {
            public const string UserEndpoint = ApiEndpoint + "/users";

        }

    }
}
