namespace blockchainDemo;
public class Transaction
{
    public string fromAddress { get; set; }
    public string toAddress { get; set;}
    public int amount { get; set;}


    public Transaction(string fromAddress, string toAddress, int amount)
    {
        this.fromAddress = fromAddress;
        this.toAddress = toAddress;
        this.amount = amount;
    }
}
