namespace MyLedger.Models
{
    public enum Bank
    {
        RBC,
        CIBC
    }
    public class AddAmountViewModel
    {
        public Guid Id { get; set; }
        public string Bank { get; set; }  // Bank enum instead of string

        public decimal AmountValue { get; set; }  // Clear naming

        public DateTime CreatedAt { get; set; }
    }
}
