namespace Slovko.NL.Api.Models
{
    using Slovko.NL.Api.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FilterGenerator
    {



        public static (string,  HashSet<string>) GenerateFilter(LetterGroup[] lettersStates)
        {
            string[] RegexPattern = new string[] {
                "[абвгґдеєжзиіїйклмнопрстуфхцчшщьюя]",
                "[абвгґдеєжзиіїйклмнопрстуфхцчшщьюя]",
                "[абвгґдеєжзиіїйклмнопрстуфхцчшщьюя]",
                "[абвгґдеєжзиіїйклмнопрстуфхцчшщьюя]",
                "[абвгґдеєжзиіїйклмнопрстуфхцчшщьюя]"
            };

            
            // var lettersMustContain = new List<string>(); but for unique letters
            var lettersMustContain = new HashSet<string>();

            
            for (int i = 0; i < lettersStates.Length; i++)
            {
                for (int j = 0; j < lettersStates[i].Letters.Length; j++)
                {
                    var letter = lettersStates[i].Letters[j];

                    if (letter.State == (int)LetterState.Wrong)
                    {
                        if (!lettersMustContain.Contains(letter.Text))
                        {
                            for (int k = 0; k < RegexPattern.Length; k++)
                            {
                                RegexPattern[k] = RegexPattern[k].Replace(letter.Text, "");
                            }
                        }
                        else
                        {
                            RegexPattern[j] = RegexPattern[j].Replace(letter.Text, "");
                        }
                    }
                    else if (letter.State == (int)LetterState.PartialMatch)
                    {
                        lettersMustContain.Add(letter.Text);
                        RegexPattern[j] = RegexPattern[j].Replace(letter.Text, "");
                    }
                    else if (letter.State == (int)LetterState.FullMatch)
                    {
                        lettersMustContain.Add(letter.Text);
                        RegexPattern[j] = letter.Text;
                    }
                }
            }

            return (string.Join("", RegexPattern), lettersMustContain);
        }
    }

}
