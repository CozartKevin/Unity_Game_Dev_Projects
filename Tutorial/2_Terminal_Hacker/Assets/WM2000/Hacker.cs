
using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game configuration data
    const string menuHint = "You may type menu at any time";
    string[] level1Passwords = { "Books", "aisle", "shelf", "password" , "font", "borrow" };
    string[] level2Passwords = { "mitochondria", "advocate", "unprecedented", "Poignant","nebulous", "clandestine" };
    string[] level3Passwords = { "apple", "pear", "space", "shuttle", "cisco", "ferengi" };

    //Game state
    int level;
    string password;
    enum Screen { MainMenu, Password, Win};
    Screen currentScreen;
	// Use this for initialization
	void Start () {
        
        ShowMainMenu();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnUserInput(string input)
    {

        if (input.ToLower() == "menu")
        {
            ShowMainMenu();
        }
        else if (input.ToLower() == "quit" || input.ToLower() == "close" || input.ToLower() == "exit")
        {
            Terminal.WriteLine("Please close browser");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            RunPasswordCheck(input);
        }

    }

    void StartGame()
    {
        currentScreen = Screen.Password;
        SetPassword();
        NewMethod();
    }

    private void NewMethod()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Enter Your password, hint:" + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        MainMenuText();
    }

    static void MainMenuText()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for local library");
        Terminal.WriteLine("Press 2 for police station");
        Terminal.WriteLine("Press 3 for NSA");
        Terminal.WriteLine("Enter your selection: ");
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            StartGame();
        }
        else if (input.ToLower() == "007")
        {
            Terminal.WriteLine("Choose a level Mr. Bond.");
        }
        else if (input.ToLower() == "egg")
        {
            Terminal.WriteLine("Easter Egg Found");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
            Terminal.WriteLine(menuHint);
        }
    }

    void SetPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0,level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0,level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0,level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid Level Number");
                break;
        }
        
    }

    void RunPasswordCheck(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Please try again.");
            Terminal.WriteLine(menuHint);
        }
    }

   void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    | \\V// |
    | |~~~| |
    | |===| |
    | |j  | |
    | | g | |
     \|===|/
      '---'
                                 ");
                break;
            case 2:
                Terminal.WriteLine("You got the prison Key!");
                Terminal.WriteLine(@"
8 8 8 8                   ,ooo.
8a8 8a8                  oP   ?b
d888a888zzzzzzzzzzzzzzzz8     8b
`""^""'                  ?o___oP'

");
                break;

            case 3:
                Terminal.WriteLine("Take a load off!");
                Terminal.WriteLine(@"   
 (.-'  - '`-.         
 /            :       
|   ,    \   \'      
 \   \    @   @       
  \   \__.'__.'       
");
                break;
            default:
                break;
        }
        Terminal.WriteLine(menuHint);
    }
}
