
using System.Text.Json;
using blockchainDemo;

bool flagExit = false;
string userInput = "";

do{

    Console.WriteLine("Please select an option:\n");
    Console.WriteLine("ls- Display all");
    Console.WriteLine("a- Add a blocks");
    Console.WriteLine("c- Check blocks");
    Console.WriteLine("m- Modify a block");
    Console.WriteLine("q- Quit"); 

    userInput = Console.ReadLine();

    List<AddBlocks> addBlocks = new List<AddBlocks>();

    //Genius Block
    addBlocks.Add(new AddBlocks(0, "Genius", 0, "0"));


    switch(userInput){
        case "ls":

            var option = new JsonSerializerOptions { WriteIndented = true};
            foreach(AddBlocks block in addBlocks){
                Console.WriteLine(JsonSerializer.Serialize(block, option));
            }

            Console.WriteLine("\n");
            break;
        case "q":
            break;
    }

}while(!flagExit);