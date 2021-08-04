using System;

namespace WordSuggestion
{
    public class WordSuggester
    {
        public Node Node = Node.CreateRoot();
        public string prefix = string.Empty;
        public WordSuggester()
        {

        }
        public void Insert(string word)
        {
            var currentNode = Node;
            foreach (var letter in word)
            {
                if (currentNode.ExistLetter(letter.ToString()))
                {
                    currentNode = currentNode.Children[letter.ToString()];
                    continue;
                }

                var tempNode = new Node(letter.ToString());
                currentNode.Children.Add(letter.ToString(), tempNode);
                currentNode = tempNode;
            }
            currentNode.IsWord = true;
        }

        public void AvailableWords(Node node)
        {
            AvailableWords(node, prefix);
        }

        private void AvailableWords(Node node, string word)
        {
            if (node.IsRootNode() == false)
                word += node.Letter;

            if (node.IsWord)
            {
                Console.WriteLine(prefix + ((prefix.Length == 0) ? word : word[(prefix.Length + 1)..]));
            }

            foreach (var child_node in node.Children.Values)
            {
                AvailableWords(child_node, word);
            }
        }

        public void GetSuggestion(Node node)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAvailable Words: ");
            ResetPrefix(node);
            AvailableWords(node);
            AvailableLetters(node);
        }

        public Node GetNextNode(Node node, string letter)
        {
            prefix += letter;
            return node.Children[letter];
        }

        private void ResetPrefix(Node node)
        {
            if (node.IsRootNode() || (node.IsWord && node.Children.Count == 0))
                prefix = string.Empty;
        }

        public void AvailableLetters(Node node)
        {
            Console.WriteLine("\nAvailable Letters: ");
            foreach (var letter in node.Children.Keys)
            {
                Console.Write(letter == " " ? "[Empty Space]" : $"{letter} ");
            }
            Console.WriteLine("\n");
        }
    }
}