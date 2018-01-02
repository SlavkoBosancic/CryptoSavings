namespace CryptoSavings.Model.DAL.HttpAPI
{
    public class Currency
    {
        public string Id { get; set; }                      // BTC or USD
        public string Name { get; set; }                    // Bitcoin or US Dollar

        public string FullName => this.ToString();          // Bitcoin (BTC) or US Dollar (USD)
        public bool IsEmpty => string.IsNullOrEmpty(this.Id) || string.IsNullOrEmpty(this.Name);

        public virtual bool IsCryptoCurrency => false;
        public virtual bool IsFiatCurrency => false;

        #region [Overrides]

        public override bool Equals(object obj)
        {
            return obj is Currency ? this.Id == ((Currency)obj).Id : false;
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(this.Id) ? string.Empty.GetHashCode() : this.Id.GetHashCode();
        }

        public override string ToString()
        {
            var result = string.Empty;

            if(!string.IsNullOrEmpty(this.Id) && !string.IsNullOrEmpty(this.Name))
            {
                result = string.Format("{0} ({1})", this.Name, this.Id);
            }

            return result;
        }

        #endregion
    }
}
