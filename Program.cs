using System;
using System.Data;
using NUnit.Framework;

class Program
{
    static void Main()
    {
        
        Console.Write("Input: ");
        string userInput = Console.ReadLine()!;
        Boolean CheckInputLoop = true;
        // The program will run when the correct values are entered.
        while (CheckInputLoop)
        {
            if (CheckInput(userInput))
            {
                Oldphonekeypad inputOld = new Oldphonekeypad(userInput);
                
                // Console.WriteLine($"Output: {inputOld.DisplayOldphonekeypad()}");
                Console.WriteLine("Output: " + inputOld.DisplayOldphonekeypad());
                CheckInputLoop = false;
                Thread.Sleep(3000);
            }
            else
            {
                Console.Write("The entered value is invalid. Please enter a new value: ");
                userInput = Console.ReadLine()!;
            }
        }


        // test
        // PhoneTest t = new PhoneTest();
        // t.DisplayOldphonekeypad("33#", "E");
        // t.DisplayOldphonekeypad("227*#", "B");
        // t.DisplayOldphonekeypad("4433555 555666#", "HELLO");
        // Console.WriteLine("All ok!!!..");
        
    }
    // The function checks each value to see if there are any invalid values in the string. 
    static Boolean CheckInput(string input)
    {
        foreach (char c in input)
        {
            if (char.IsDigit(c) || c == '\b')
            {
                return true;
            }
        }
        return false;
    }

    
}   

// class Oldphonekeypad
public class Oldphonekeypad
{
    // Properties
    private string Input { get; set; } 

    // Constructor
    public Oldphonekeypad(string input)
    {
        Input = input;
        
    }

    // Method
    public string DisplayOldphonekeypad()
    {
        // Delete the last character that is *
        Input = Input.Substring(0, Input.Length-1);
        // text is output
        string text = "";
        // recount is counting the number of repeated characters.
        int recount = 0;
        // prechar is prechar is the previous character. 
        // When new characters are found, the previous characters are added to the text.
        char prechar = ' ';
        // When lengthinput is equal to lengthinputcheck The current character is added to the text.
        int lengthinput = Input.Length;
        int lengthinputcheck = 0;
        
        // The dictionary uses [prechar] as the key, and [recount] as the position in the list.
        Dictionary<char, List<char>> numData = new Dictionary<char, List<char>>();

        numData['0'] = new List<char> { ' ' };
        numData['1'] = new List<char> { '&', '\'', '(' };
        numData['2'] = new List<char> { 'A', 'B', 'C' };
        numData['3'] = new List<char> { 'D', 'E', 'F' };
        numData['4'] = new List<char> { 'G', 'H', 'I' };
        numData['5'] = new List<char> { 'J', 'K', 'L' };
        numData['6'] = new List<char> { 'M', 'N', 'O' };
        numData['7'] = new List<char> { 'P', 'Q', 'R', 'S' };
        numData['8'] = new List<char> { 'T', 'U', 'V' };
        numData['9'] = new List<char> { 'W', 'X', 'Y', 'Z' };

        foreach (char c in Input)
        {   
            lengthinputcheck += 1;
            // When starting or encountering a new char, recount is 0.
            if (recount == 0 )
            {   
                // protect start with * --> *33#
                if (c != '*' && c != ' ')
                {
                    // preIf prechar is equal to 'c', when a new value is encountered, 
                    // add prechar to the text and update prechar to the new value encountered.
                    prechar = c;
                    recount += 1;
                }
                
                
            }
            else if ( c == '*' )
            {   
                //  2222#
                // Add prechar to text and recount to 0.
                text = Loopnum(recount, numData, prechar, text);
                //  Delete the last character from text.
                text = text.Substring(0, text.Length-1);
                recount = 0;   
            }
            else if ( c == ' ')
            {
                //  2222#
                // Add prechar to text and recount to 0.
                text = Loopnum(recount, numData, prechar, text);
                recount = 0;   
                
            }

            else
            {
                // Encountering repeated characters.
                if ( prechar == c )
                {
                    recount += 1;
                }
                // Encountering a new character.
                else
                {   
                    //  2222#
                    // Add prechar to text and recount to 0.
                    text = Loopnum(recount, numData, prechar, text);
                    // recount = 1  The encountered character is the first one.
                    recount = 1;
                    prechar = c;
                    
                }
            }
            // Add the character encountered at the end to the text. --> 333 #, 3#,  33  33   33   #
            if ( lengthinput == lengthinputcheck && c != '*' && c != ' ')
            {   
                //  2222#
                text = Loopnum(recount, numData, prechar, text);
                
            }
        }
        
        //  return output
        return text;
        // Console.WriteLine($"Output: {text}");
    }

    // The function Loopnum checks, when encountering a duplicate character greater than what is in the dictionary, 
    // it performs a mod operation to find the value.
    private string Loopnum(int recount, Dictionary<char, List<char>> numData, char prechar, string text) 
    {
        //  check if 33333#, 1111122222#
        if (recount-1 >= numData[prechar].Count)
        {
            text = text + numData[prechar][(recount-1)%numData[prechar].Count];
        }
        else
        {
            text = text + numData[prechar][recount-1];
        }
        return text;
    }
}
// Test
[TestFixture]
public class PhoneTest
{
    //      output : input
    // [TestCase("33#", "E")]
    // [TestCase("227*#", "B")]
    // [TestCase("555", "L")]
    public void DisplayOldphonekeypad(string input, string expectedResult)
    {
        // Arrange
        Oldphonekeypad oldphonekeypad = new Oldphonekeypad(input);

        // Act
        string result = oldphonekeypad.DisplayOldphonekeypad();

        // Assert
        Assert.AreEqual(expectedResult, result);
        Console.WriteLine("All ok!!!");
    }

}

