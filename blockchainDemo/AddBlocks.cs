using System.Security.Cryptography;
using System.Text;


namespace blockchainDemo;
public class AddBlocks
{
    public int id { get; set; }
    public string firstName { get; set; }
    public int amount { get; set; }
    public string previousBlockHash { get; set; }
    public string blockHash { get; set; }


    //previous values
    public int pre_id { get; set; }
    public string pre_firstName { get; set; }
    public int pre_amount { get; set; }

    public AddBlocks()
    {
        
    }
    public AddBlocks(int id, string firstName, int amount, string previousBlockHash)
    {
        this.id = id;
        this.firstName = firstName;
        this.amount = amount;
        this.previousBlockHash = previousBlockHash;
        this.blockHash = createHash(id, firstName, amount, previousBlockHash);

        this.pre_id = id;
        this.pre_firstName = firstName;
        this.pre_amount = amount;
    }

    public string createHash(int id, string firstName, int amount, string previousBlockHash){

        var sha = SHA256.Create();

        int nonce = 0;

        string info = id.ToString() + firstName + amount.ToString() + previousBlockHash;
        string infoHash = "";

        bool flagCorrect = false;

        if(id == 0){
            infoHash = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(info + nonce.ToString())));
        }
        else{

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

        return infoHash;
    }
}
