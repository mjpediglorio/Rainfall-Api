namespace Rainfall.Api
{
    public interface IApi
    {
        Task<ApiResultInfo> GetReadings(int stationId, int count);
    }
}
