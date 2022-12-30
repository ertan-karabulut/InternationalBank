namespace WebBlazor.Models.Account
{
    public class AccountUpdateDto
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public string AccountName { get; set; }
        public int? CustomerId { get; set; }
        public int? TypeId { get; set; }
        public int? CurrencyUnitId { get; set; }
        public string Iban { get; set; }
        public string AccountNumber { get; set; }
        public bool? IsActive { get; set; }
    }
}
