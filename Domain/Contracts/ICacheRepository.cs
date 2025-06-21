namespace Services
{
    public  interface ICacheRepository
    {

        //Get 
        Task<string?> GetAsync(string cacheKey);


        //set 
        Task SetAsync(string cacheKey, string value, TimeSpan timeSpan);



    }
}