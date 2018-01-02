using CryptoSavings.Model.DAL.Repository;

namespace CryptoSavings.Model
{
    public class User
    {
        [PrimaryKey(AutoAssigned = false)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
