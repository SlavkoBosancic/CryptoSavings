namespace CryptoSavings.Model.DAL.HttpClient
{
    public class HttpParameter
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public HttpParameterType Type { get; set; }
    }
}
