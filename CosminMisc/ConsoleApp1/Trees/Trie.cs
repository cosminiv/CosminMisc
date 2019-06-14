using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Trees
{
    /// <summary>
    /// Tree of word prefixes
    /// </summary>
    class Trie
    {
        Node Root { get; } = new Node {
            Value = '0',
        };

        public int NodeCount = 0;

        public bool FindWord(string word) {
            Node node = FindNodeWithPrefix(word);
            return node != null && node.IsWord;
        }

        public List<string> FindWordsWithPrefix(string prefix, int maxWords = int.MaxValue) {
            List<string> result = new List<string>();
            Node prefixNode = FindNodeWithPrefix(prefix);
            if (prefixNode == null)
                return result;

            NodeWithText root = new NodeWithText {
                Node = prefixNode,
                Text = new StringBuilder(prefix)
            };

            foreach (NodeWithText node in IterateDepthFirst(root)) { 
                if (node.Node.IsWord)
                    result.Add(node.Text.ToString());

                if (result.Count == maxWords)
                    break;
            }

            return result;
        }

        public string FindLongestWordWithAllPrefixesWords() {
            List<StringBuilder> longestWords = new List<StringBuilder> { new StringBuilder("")} ;
            int maxLenFound = 0;
            NodeWithText root = new NodeWithText { Node = Root, Text = new StringBuilder("") };
            Queue<NodeWithText> wordsQueue = new Queue<NodeWithText>();
            wordsQueue.Enqueue(root);

            while (wordsQueue.Count > 0) {
                NodeWithText node = wordsQueue.Dequeue();
                if (node.Text.Length > 0 && !node.Node.IsWord)
                    continue;

                if (node.Text.Length > maxLenFound) { 
                    maxLenFound = node.Text.Length;
                    longestWords.Clear();
                }

                if (node.Text.Length == maxLenFound) {
                    longestWords.Add(node.Text);
                }

                foreach (char ch in node.Node.Children.Keys) {
                    NodeWithText newNode = GenerateNodeWithText(node, ch);
                    wordsQueue.Enqueue(newNode);
                }
            }

            string result = longestWords.Select(w => w.ToString()).Min();
            return result;
        }

        IEnumerable<NodeWithText> IterateBreadthFirst(NodeWithText root) {
            Queue<NodeWithText> wordsQueue = new Queue<NodeWithText>();
            wordsQueue.Enqueue(root);

            while (wordsQueue.Count > 0) {
                NodeWithText node = wordsQueue.Dequeue();
                yield return node;

                foreach (char ch in node.Node.Children.Keys) {
                    NodeWithText newNode = GenerateNodeWithText(node, ch);
                    wordsQueue.Enqueue(newNode);
                }
            }
        }

        IEnumerable<NodeWithText> IterateDepthFirst(NodeWithText root) {
            Stack<NodeWithText> wordsStack = new Stack<NodeWithText>();
            wordsStack.Push(root);

            while (wordsStack.Count > 0) {
                NodeWithText node = wordsStack.Pop();
                yield return node;

                foreach (char ch in node.Node.Children.Keys) {
                    NodeWithText newNode = GenerateNodeWithText(node, ch);
                    wordsStack.Push(newNode);
                }
            }
        }

        NodeWithText GenerateNodeWithText(NodeWithText parent, char ch) {
            return new NodeWithText {
                Node = parent.Node.Children[ch],
                Text = new StringBuilder(parent.Text.ToString()).Append(ch)
            };
        }

        private Node FindNodeWithPrefix(string prefix) {
            Node node = Root;

            foreach (char ch in prefix) {
                if (!node.Children.ContainsKey(ch))
                    return null;
                node = node.Children[ch];
            }

            return node;
        }

        public void Insert(string word) {
            Node parent = Root;
            for (int i = 0; i < word.Length; i++) {
                char ch = word[i];

                if (!parent.Children.ContainsKey(ch)) {
                    parent.Children[ch] = new Node { };
                    NodeCount++;
                }

                parent = parent.Children[ch];
            }
            parent.IsWord = true;
        }

        class Node
        {
            public char Value { get; set; }
            public SortedDictionary<char, Node> Children { get; set; } = new SortedDictionary<char, Node>(CharComparer);
            public bool IsWord { get; set; }
            static ReverseCharComparer CharComparer = new ReverseCharComparer();

            class ReverseCharComparer : IComparer<char>
            {
                public int Compare(char x, char y) {
                    return y - x;
                }
            }
        }

        class NodeWithText
        {
            public Node Node;

            /// <summary>
            /// Text from root down to the current node
            /// </summary>
            public StringBuilder Text;
        }
    }
}
