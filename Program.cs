using System;
using System.Data;
class Program
{
    static void Main()
    {
        
        Console.Write("Input: ");
        string userInput = Console.ReadLine()!;

        string pad = OldPhonePad(userInput);
        Console.WriteLine($"Output: {pad}");
        

    }

    static string OldPhonePad(String input) 
    {
        input = input.Substring(0, input.Length-1);
        string text = "";

        int recount = 0;
        char prechar = ' ';
        int lengthinput = input.Length;
        int lengthinputcheck = 0;
        
        
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

        foreach (char c in input)
        {   
            lengthinputcheck += 1;
            if (recount == 0 )
            {   
                // protect start with * --> *33#
                if (c != '*' && c != ' ')
                {
                    
                    prechar = c;
                    recount += 1;
                }
                
                
            }
            else if ( c == '*' )
            {   
                //  2222#
                text = Loopnum(recount, numData, prechar, text);
                text = text.Substring(0, text.Length-1);
                recount = 0;   
            }
            else if ( c == ' ')
            {
                //  2222#
                text = Loopnum(recount, numData, prechar, text);
                recount = 0;   
                
            }
            else
            {
                // Repeat the previous letter
                if ( prechar == c )
                {
                    recount += 1;
                }
                // New number
                else
                {   
                    //  2222#
                    text = Loopnum(recount, numData, prechar, text);
                    recount = 1;
                    prechar = c;
                    
                }
            }
            // put last char --> 333 #, 3#,  33  33   33   #
            if ( lengthinput == lengthinputcheck && c != '*' && c != ' ')
            {   
                //  2222#
                text = Loopnum(recount, numData, prechar, text);
                
            }
        }
        


        return text;
    }

    static string Loopnum(int recount, Dictionary<char, List<char>> numData, char prechar, string text) 
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