namespace CryptoSavings.Model.DAL.HttpAPI
{
    public class CryptoCurrency : Currency
    {
        public string DetailsUrl { get; set; }
        public string ImageUrl { get; set; }

        public override bool IsCryptoCurrency => true;
    }
}
