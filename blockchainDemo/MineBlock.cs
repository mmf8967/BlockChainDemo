using System.Security.Cryptography;
using System.Text;


namespace blockchainDemo;
public class MineBlock
{
    public List<Transaction> transactions {get; set;}
    public string previousBlockHash { get; set; }
    public string blockHash { get; set; } 

    public MineBlock()
    {
        
    }
    public MineBlock(List<Transaction> transactions, string previousBlockHash)
    {
        this.transactions = transactions;
        this.previousBlockHash = previousBlockHash;
        this.blockHash = createHash(transactions, previousBlockHash); 
    }

    public string createHash(List<Transaction> transactions, string previousBlockHash){

        var sha = SHA256.Create();

        int nonce = 0;

        string info = transactions + previousBlockHash;
        string infoHash = "";

        bool flagCorrect = false;

        if(previousBlockHash != "0"){
            Task task = new Task(() => {
                

                    while(!flagCorrect){
                        infoHash = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(info + nonce.ToString())));

                        if(infoHash.Substring(0, 4) == "0000"){
                            flagCorrect = true;
                        }
                        else{
                            nonce++;
                        }
                    } 
            });

            task.Start();


            int timer = 0;
            Console.WriteLine("\nLoading...\n");
            while(!flagCorrect){

                Thread.Sleep(1000);

                Console.Write("."); 

                timer += 1;
            }
            
            Console.Write("{0} S", timer);
            Console.WriteLine("\n\n");
        }
        else{
            infoHash = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(info + nonce.ToString())));
            flagCorrect = true;
        } 

        return infoHash;
    }
}
