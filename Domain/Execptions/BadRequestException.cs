
namespace Services
{
    [Serializable]
    public sealed  class BadRequestException(List<string> erros):Exception("Validation Failed! ") 
    {

        public List<string> Errors { get; }=erros;

    }
}