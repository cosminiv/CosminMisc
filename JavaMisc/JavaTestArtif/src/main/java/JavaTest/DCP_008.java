package JavaTest;

import java.util.HashSet;

// Count how many "unival subtrees" there are in a tree
// (they have only one repeated value)
//
class DCP_008 {
    public void test() {
        TreeNode tree = new TreeNode(0, new TreeNode(1, null, null),
                new TreeNode(0, new TreeNode(1, new TreeNode(1, null, null), new TreeNode(1, null, null)),
                        new TreeNode(0, null, null)));

        int c = getCountUnival(tree);
        System.out.printf("Count unival = %d", c);
    }

    static int getCountUnival(TreeNode root) {
        Count count = new Count();
        getCountUnival(root, count);
        return count.Value;
    }

    static HashSet<Integer> getCountUnival(TreeNode node, Count count) {
        HashSet<Integer> leafResult = handleLeafCase(node, count);
        if (leafResult != null)
            return leafResult;

        HashSet<Integer> leftValues = (node.Left != null) ? getCountUnival(node.Left, count) : new HashSet<Integer>();
        HashSet<Integer> rightValues = (node.Right != null) ? getCountUnival(node.Right, count)
                : new HashSet<Integer>();

        HashSet<Integer> allValues = new HashSet<Integer>(leftValues);
        allValues.add(node.Value);
        for (int n : rightValues)
            allValues.add(n);

        if (allValues.size() == 1)
            count.Value++;

        return allValues;
    }

    static HashSet<Integer> handleLeafCase(TreeNode node, Count count) {
        if (node.Left == null && node.Right == null) {
            count.Value++;
            HashSet<Integer> valueSet = new HashSet<Integer>();
            valueSet.add(node.Value);
            return valueSet;
        }
        return null;
    }

    static class Count {
        int Value;
    }

    static class TreeNode {
        int Value;
        TreeNode Left;
        TreeNode Right;

        TreeNode(int v, TreeNode l, TreeNode r) {
            Value = v;
            Left = l;
            Right = r;
        }
    }
}