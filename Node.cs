using System.Collections.Generic;

namespace WordSuggestion
{
    public class Node
    {
        public string Letter { get; set; }
        public bool IsWord { get; set; }

        public Dictionary<string, Node> Children = new();

        public static Node CreateRoot()
        {
            return new Node("000");
        }
        public Node(string letter)
        {
            Letter = letter;
        }

        public bool ExistLetter(string letter)
        {
            return Children.ContainsKey(letter);
        }

        public bool IsRootNode()
        {
            return Letter.Equals("000");
        }
    }
}