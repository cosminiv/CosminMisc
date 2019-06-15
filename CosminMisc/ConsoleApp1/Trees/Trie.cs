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

        public string FindLongestWord() {
            string result = "";
            int maxLen = 0;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);

            while (queue.Count > 0) {
                Node node = queue.Dequeue();
                if (node.Level > maxLen && node.IsWord) {
                    maxLen = node.Level;
                    result = MakeWordFromNode(node);
                    Console.WriteLine(result);
                }
                
                foreach (var ch in node.Children.Keys) {
                    queue.Enqueue(node.Children[ch]);
                }
            }

            return result;
        }

        public Dictionary<int, int> FindNodeCountPerLevel() {
            Dictionary<int, int> result = new Dictionary<int, int>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);

            while (queue.Count > 0) {
                Node node = queue.Dequeue();
                if (!result.ContainsKey(node.Level))
                    result[node.Level] = 1;
                else
                    result[node.Level]++;

                if (node.Level == 1)
                    Console.Write(node.Value);

                foreach (var ch in node.Children.Keys)
                    queue.Enqueue(node.Children[ch]);
            }

            return result;
        }

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
            List<Node> longestWords = new List<Node>();
            int maxLenFound = 0;
            
            Queue<Node> wordsQueue = new Queue<Node>();
            wordsQueue.Enqueue(Root);

            while (wordsQueue.Count > 0) {
                Node node = wordsQueue.Dequeue();
                if (node.Level > 0 && !node.IsWord)
                    continue;

                if (node.Level > maxLenFound) { 
                    maxLenFound = node.Level;
                    longestWords.Clear();
                }

                if (node.Level == maxLenFound) {
                    longestWords.Add(node);
                }

                foreach (char ch in node.Children.Keys) {
                    wordsQueue.Enqueue(node.Children[ch]);
                }
            }

            string result = longestWords.Select(n => MakeWordFromNode(n)).Min();
            return result;
        }

        string MakeWordFromNode(Node node) {
            StringBuilder sb = new StringBuilder(node.Level);
            for (Node crtNode = node; crtNode.Level > 0; crtNode = crtNode.Parent)
                sb.Append(crtNode.Value);
            
            return new string(sb.ToString().Reverse().ToArray());
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
                    NodeCount++;
                    parent.Children[ch] = new Node {
                        Parent = parent,
                        Value = ch,
                        Level = i + 1
                    };
                }

                parent = parent.Children[ch];
            }
            parent.IsWord = true;
        }

        class Node
        {
            public char Value { get; set; }
            //public int Id { get; set; }
            public SortedDictionary<char, Node> Children { get; set; } = new SortedDictionary<char, Node>(CharComparer);
            public bool IsWord { get; set; }
            public Node Parent { get; set; }
            public int Level { get; set; }
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
