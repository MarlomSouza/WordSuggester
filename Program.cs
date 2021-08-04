using System;
using System.Collections.Generic;

namespace WordSuggestion
{
    public class Program
    {
        public static string typedWord = string.Empty;
        public static void Main(string[] args)
        {
            var wordSuggester = new WordSuggester();
            var words = InsertWords();
            foreach (var word in words)
            {
                wordSuggester.Insert(word);
            }

            var letter = string.Empty;
            var currentNode = wordSuggester.Node;
            while (letter != "2")
            {
                letter = ShowOptions();
                Console.Clear();

                switch (letter)
                {
                    case "1":
                        wordSuggester.GetSuggestion(currentNode);
                        break;

                    case "2":
                        Environment.Exit(0);
                        break;
                    default:
                        currentNode = WordSuggestion(wordSuggester, letter, currentNode);
                        continue;
                }
            }
        }

        private static IList<string> InsertWords()
        {
            Console.WriteLine("Enter station: (Split by ',')");
            return Console.ReadLine().Trim().Split(',');
        }

        private static Node WordSuggestion(WordSuggester wordSuggester, string letter, Node currentNode)
        {
            if (currentNode.ExistLetter(letter))
            {
                typedWord += letter;
                currentNode = wordSuggester.GetNextNode(currentNode, letter);

                if (currentNode.IsWord)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Found: " + typedWord);

                    if (currentNode.Children.Count == 0)
                    {
                        currentNode = wordSuggester.Node;
                        typedWord = string.Empty;
                    }
                }
                wordSuggester.GetSuggestion(currentNode);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Letter not found: " + letter);

                wordSuggester.GetSuggestion(currentNode);
            }

            return currentNode;
        }

        private static string ShowOptions()
        {
            Console.ForegroundColor = ConsoleColor.White;
            var options = "1 - Show Stations/Letters \n" +
                          "2 - Exit \n" +
                          "Type a option or a letter: ";

            Console.WriteLine(options);
            return Console.ReadLine();
        }
    }
}