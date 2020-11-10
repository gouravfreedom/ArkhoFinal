using System;
namespace DFS.Models
{
    public class PaymentResponse
    {
        public TransactionStatus TransactionStatus { get; set; }
    }

    public class TransactionStatus
    {
        public string payeeId { get; set; }
        public string payerId { get; set; }
        public string amount { get; set; }
        public string paypalStatus { get; set; }
        public string appStatus { get; set; }
        public string processURL { get; set; }
        public string eventId { get; set; }
    }
}
