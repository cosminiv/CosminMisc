using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    // Given a binary tree, check whether it is a mirror of itself(ie, symmetric around its center).
    class Leet_101
    {
        public void Test() {
            TreeNode tree1 =
                new TreeNode(1,
                    L: new TreeNode(2,
                        new TreeNode(2), null),
                    R: new TreeNode(2,
                        new TreeNode(2), null)
                );

            TreeNode tree2 =
                new TreeNode(1,
                    L: new TreeNode(2,
                        null, new TreeNode(3)),
                    R: new TreeNode(2,
                        null, new TreeNode(3))
                );

            TreeNode tree3 =
                new TreeNode(1,
                    L: new TreeNode(2,
                        null, new TreeNode(3)),
                    R: new TreeNode(2,
                        new TreeNode(3), null)
                );

            foreach (var tree in new[] { tree1, tree2, tree3}) {
                bool a = IsSymmetric(tree);
                Console.WriteLine(a);
            }
        }

        public bool IsSymmetric(TreeNode root) {
            if (root == null)
                return true;
            if (root.left == null && root.right == null)
                return true;
            if (root.left == null || root.right == null)
                return false;
            return 
                AreSymmetric(root.left, root.right);
        }

        bool AreSymmetric(TreeNode left, TreeNode right) {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;
            if (left.val != right.val)
                return false;

            return AreSymmetric(left.right, right.left)
                && AreSymmetric(left.left, right.right);
        }

        public bool IsSymmetricV2(TreeNode root) {
            if (root == null || (root.left == null && root.right == null))
                return true;

            var nodesLeftToRight = new List<string>();
            TraverseLeftToRight(root.left, "", nodesLeftToRight);

            var nodesRightToLeft = new List<string>();
            TraverseRightToLeft(root.right, "", nodesRightToLeft);

            if (nodesLeftToRight.Count != nodesRightToLeft.Count)
                return false;

            for (int i = 0; i < nodesRightToLeft.Count; i++) {
                Console.WriteLine($"{nodesLeftToRight[i]} {nodesRightToLeft[i]}");

                if (nodesLeftToRight[i] != nodesRightToLeft[i])
                    return false;
            }

            return true;
        }

        string NodeAsText(TreeNode node) {
            if (node == null)
                return "null";
            return node.val.ToString();
        }

        void TraverseLeftToRight(TreeNode root, string suffix, List<string> list) {
            if (root != null)
                TraverseLeftToRight(root.left, "L", list);

            string valText = root == null ? "null" : root.val.ToString();
            list.Add(valText + suffix);

            if (root != null)
                TraverseLeftToRight(root.right, "R", list);

        }

        void TraverseRightToLeft(TreeNode root, string suffix, List<string> list) {
            if (root != null)
                TraverseRightToLeft(root.right, "L", list);

            string valText = root == null ? "null" : root.val.ToString();
            list.Add(valText + suffix);

            if (root != null)
                TraverseRightToLeft(root.left, "R", list);
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
            public TreeNode(int x, TreeNode L, TreeNode R) { val = x; left = L; right = R; }

            public override string ToString() {
                return val.ToString();
            }
        }

    }
}
