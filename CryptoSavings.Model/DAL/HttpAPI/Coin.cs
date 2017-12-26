namespace CryptoSavings.Model.DAL.HttpAPI
{
    public class Coin
    {
        public string Id { get; set; }                                      // BTC
        public string Name { get; set; }                                    // Bitcoin
        public string FullName => string.Format("{0} ({1})", Name, Id);    // Bitcoin (BTC)

        public string DetailsUrl { get; set; }
        public string ImageUrl { get; set; }

        #region Overrides

        public override bool Equals(object obj)
        {
            return obj is Coin ? this.Id == ((Coin)obj).Id : false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.FullName;
        }

        #endregion
    }
}
