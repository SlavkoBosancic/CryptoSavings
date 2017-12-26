namespace CryptoSavings.Model.DAL.HttpAPI
{
    public class Currency
    {
        public string Id { get; set; }          // USD
        public string Name { get; set; }        // US Dollar
        public string FullName { get; set; }    // US Dollar (USD)

        #region Overrides

        public override bool Equals(object obj)
        {
            return obj is Currency ? this.Id == ((Currency)obj).Id : false;
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
