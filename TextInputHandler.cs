using System;
using System.Linq;
using System.Text;

namespace algorithmscSharp
{
    class TextInputHandler
    {
        public static string formatInput(string input)
        {
            string[] bannedUpper = { "Dec." };
            string[] bannedSpace = { "$15." };
            char[] punctuation = ".!".ToCharArray();
            StringBuilder sb = new StringBuilder();
            string[] words = input.Split(" ");
            for(var i = 0;i<words.Length;i++)
            {
                string word = words[i];
                if(string.IsNullOrEmpty(word))
                    continue;
                string priorWord = "";
                string followingWord = "";
               
                int j = i;
                while(j > 0 && string.IsNullOrEmpty(priorWord))
                {
                    priorWord = words[j-1];
                    j--;
                }
                j = i;
                while(j+1 < words.Length && string.IsNullOrEmpty(followingWord))
                {
                    followingWord = words[j+1];
                    j++;
                }
                
                if(i == 0 || punctuation.Contains(priorWord[priorWord.Length - 1]))
                {
                    //is first word
                    if(!bannedUpper.Contains(priorWord))
                    {
                        word = word.Substring(0,1).ToUpper() + word.Substring(1).ToLower();
                    }
                }
                else
                {
                    if(char.IsUpper(word[0]) && word.Substring(1).ToLower() == word.Substring(1))
                    {
                        word = word.Substring(0,1).ToUpper() + word.Substring(1).ToLower();
                    }
                    else
                    {
                        word = word.ToLower();
                    }
                }
                if(bannedSpace.Contains(word))
                {
                    sb.Append(word);
                }
                else
                {
                    sb.Append(word + " ");
                }
            }
            return sb.ToString();
        }
    }
}
