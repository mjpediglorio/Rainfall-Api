using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rainfall.Api
{
    public class Api : IApi
    {
        public async Task<ApiResultInfo> GetReadings(int stationId, int count)
        {
            string ResultString = "";
            HttpResponseMessage ResponseString = new HttpResponseMessage();
            var link = $"https://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings?_sorted&_limit={count}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var result = await client.GetAsync(link);
                    if (ResponseString.IsSuccessStatusCode)
                    {
                        ResultString = await result.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception err)
                {
                    throw;
                }
            }
            var ret = JsonConvert.DeserializeObject<ApiResultInfo>(ResultString);
            return ret;
        }
    }
}
