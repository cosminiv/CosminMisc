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
            Children = new Dictionary<char, Node>()
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

        IEnumerable<NodeWithText> IterateBreadthFirst(NodeWithText root) {
            Queue<NodeWithText> wordsQueue = new Queue<NodeWithText>();
            wordsQueue.Enqueue(root);

            while (wordsQueue.Count > 0) {
                NodeWithText node = wordsQueue.Dequeue();
                yield return node;

                if (node.Node.Children != null) {
                    foreach (char ch in node.Node.Children.Keys) {
                        NodeWithText newNode = GenerateNodeWithText(node, ch);
                        wordsQueue.Enqueue(newNode);
                    }
                }
            }
        }

        IEnumerable<NodeWithText> IterateDepthFirst(NodeWithText root) {
            Stack<NodeWithText> wordsStack = new Stack<NodeWithText>();
            wordsStack.Push(root);

            while (wordsStack.Count > 0) {
                NodeWithText node = wordsStack.Pop();
                yield return node;

                if (node.Node.Children != null) {
                    foreach (char ch in node.Node.Children.Keys.OrderByDescending(k => k)) {
                        NodeWithText newNode = GenerateNodeWithText(node, ch);
                        wordsStack.Push(newNode);
                    }
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

                if (parent.Children == null)
                    parent.Children = new Dictionary<char, Node>();

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
            public Dictionary<char, Node> Children { get; set; }
            public bool IsWord { get; set; }
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
