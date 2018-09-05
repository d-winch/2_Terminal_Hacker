using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Game configuration data
    const string menuHint = "Type 'menu' to return";
    string[] level1Passwords = { "teach", "books", "student", "exams", "password" };
    string[] level2Passwords = { "prison", "arrest", "handcuffs", "investigate", "police" };

    //Game state
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    string password;

    // Use this for initialization
    void Start ()
    {
        level = 0;
        //Boot();
        ShowMainMenu();
    }

    IEnumerable Boot()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Booting.");
        yield return new WaitForSeconds(1);
        Terminal.ClearScreen();
        Terminal.WriteLine("Booting..");
        yield return new WaitForSeconds(1);
        Terminal.ClearScreen();
        Terminal.WriteLine("Booting...");
        yield return new WaitForSeconds(1);
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine(
            "Where would you like to hack into?\n\n" +
            "Enter 1 for the school\n" +
            "Enter 2 for the police station\n\n" +
            "Please enter your selection...");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            CheckLevelInput(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void CheckLevelInput(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2");

        if (isValidLevelNumber)
        {
            LoadLevel(int.Parse(input));
        }
        else if (input == "007") // Easter Egg
        {
            Terminal.WriteLine("Please enter a valid option, Mr Bond");
            Terminal.WriteLine(menuHint);
        }
        else
        {
            Terminal.WriteLine("Please enter a valid option");
            Terminal.WriteLine(menuHint);
        }
    }

    void LoadLevel(int value)
    {
        switch (value)
        {
            case 1:
                Terminal.ClearScreen();
                Terminal.WriteLine("Entering school network");
                level = 1;
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                Terminal.ClearScreen();
                Terminal.WriteLine("Entering police network");
                level = 2;
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
        currentScreen = Screen.Password;
        Terminal.WriteLine(
            "\nPlese enter your password...\n" +
            "Hint: " + password.Anagram());
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Incorrect, please try again");
            Terminal.WriteLine(menuHint);
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        Terminal.WriteLine("Logged in");
        ShowReward();
    }

    void ShowReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine(@"       _.-'`'-._
    .-'    _    '-.
     `-.__  `\_.-'
       |  `-``\|
       `-.....-A
               #
               #");
                break;
            case 2:
                Terminal.WriteLine(@"   ,   /\   ,
  / '-'  '-' \
 |   POLICE   |
 \    .--.    /
  |  ( 19 )  |
  \   '--'   /
   '--.  .--'
       \/");
                break;
        }
        Terminal.WriteLine(menuHint);
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
