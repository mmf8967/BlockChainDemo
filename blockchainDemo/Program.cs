
using System.Text.Json;
using blockchainDemo;

bool flagExit = false;
string userInput = "";

List<AddBlocks> addBlocks = new List<AddBlocks>();

AddBlocks oAddBlock = new AddBlocks();

//Genius Block
addBlocks.Add(new AddBlocks(0, "Genius", 0, "0"));

do{

    Console.WriteLine("Please select an option:\n");
    Console.WriteLine("ls- Display all");
    Console.WriteLine("a- Add a blocks");
    Console.WriteLine("c- Check blocks");
    Console.WriteLine("m- Modify a block");
    Console.WriteLine("q- Quit"); 

    userInput = Console.ReadLine();


    switch(userInput){
        case "ls":

            var option = new JsonSerializerOptions { WriteIndented = true};
            foreach(AddBlocks block in addBlocks){
                Console.WriteLine(JsonSerializer.Serialize(block, option));
            }

            Console.WriteLine("\n");
            break;
        case "a":
            Console.WriteLine("Please enter the ID:");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please enter the amount:");
            int amount = Convert.ToInt32(Console.ReadLine());

            string previousBlockHash = addBlocks[addBlocks.Count - 1].blockHash;

            addBlocks.Add(new AddBlocks(id, firstName, amount, previousBlockHash));

            Console.WriteLine("\n");

            break;
        case "c":
            if(addBlocks.Count == 0){
                Console.WriteLine("There is no block\n");
                break;
            }
            else{
                for(int i = 1; i <= addBlocks.Count - 1; i++){
                    if(addBlocks[i - 1].blockHash == addBlocks[i].previousBlockHash){

                        string pre_hash = oAddBlock.createHash(addBlocks[i].pre_id, addBlocks[i].pre_firstName, addBlocks[i].pre_amount, 
                        addBlocks[i].previousBlockHash);

                        if(pre_hash == addBlocks[i].blockHash){
                            Console.WriteLine("Looks Good!");
                        }
                        else{
                            Console.WriteLine("Something went wrong!!!");
                            break;
                        }
                    }
                    else{
                        Console.WriteLine("Something went wrong!!!");
                        break;
                    }
                }
            }

            break;
        case "m":
            Console.WriteLine("Please select an Index of the block which you want to modify:");
            int blockID = Convert.ToInt32(Console.ReadLine());

            bool blockExist = false;

            foreach(var i in addBlocks){
                if(i.id == blockID){
                    blockExist = true;
                    var indexOF = addBlocks.IndexOf(addBlocks.Find(i => i.id == blockID));

                    Console.WriteLine("Please enter the new id:");
                    addBlocks[indexOF].id = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the new first name:");
                    addBlocks[indexOF].firstName = Console.ReadLine();

                    Console.WriteLine("Please enter the new amount:");
                    addBlocks[indexOF].amount = Convert.ToInt32(Console.ReadLine());

                    addBlocks[indexOF].previousBlockHash = addBlocks[indexOF - 1].blockHash;

                    addBlocks[indexOF].blockHash = oAddBlock.createHash(addBlocks[indexOF].id, addBlocks[indexOF].firstName, addBlocks[indexOF].amount,
                    addBlocks[indexOF].previousBlockHash);

                    break;
                }
            }

            if(!blockExist){
                Console.WriteLine("There is no such a thing! Please try another one!");
                break;
            }

            break;
        case "q":
            flagExit = true;
            break;
    }

}while(!flagExit);