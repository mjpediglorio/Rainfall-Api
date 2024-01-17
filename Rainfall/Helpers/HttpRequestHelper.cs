namespace Rainfall_Api.Helpers
{
    public static class HttpRequestHelper
    {
        public static bool ValidateQueries(string[] requiredQuery, string[] queries)
        {
            bool ret = true;
            foreach (var item in requiredQuery)
            {
                ret = queries.Contains(item);
                if (!ret)
                {
                    break;
                }
            }
            return ret;
        }
    }
}