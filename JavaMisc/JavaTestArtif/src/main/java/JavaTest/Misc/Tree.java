package JavaTest.Misc;

import java.util.Stack;

public class Tree {
    public void test() {
        BST tree = new BST(6, null, null);
        addNonRecursive(tree, 2);
        addNonRecursive(tree, 1);
        addNonRecursive(tree, 17);
        addNonRecursive(tree, 5);
        addNonRecursive(tree, 0);
        addNonRecursive(tree, 20);
        addNonRecursive(tree, 3);

        traverseInOrder(tree);
        System.out.printf("\n");

        traverseInOrderNonRecursive(tree);
        System.out.printf("\n\n");
        
        traverseIndented(tree, 0);
    }

    void addNonRecursive(BST root, int value) {
        BST node = root;
        while (true) {
            if (value < node.Value) {
                if (node.Left == null) {
                    node.Left = new BST(value, null, null);
                    break;
                } else
                    node = node.Left;
            } else {
                if (node.Right == null) {
                    node.Right = new BST(value, null, null);
                    break;
                } else
                    node = node.Right;
            }
        }
    }

    void add(BST root, int value) {
        if (value < root.Value) {
            if (root.Left == null)
                root.Left = new BST(value, null, null);
            else
                add(root.Left, value);
        } else {
            if (root.Right == null)
                root.Right = new BST(value, null, null);
            else
                add(root.Right, value);
        }
    }

    void traverseInOrder(BST root) {
        if (root.Left != null)
            traverseInOrder(root.Left);
        System.out.printf("%d ", root.Value);
        if (root.Right != null)
            traverseInOrder(root.Right);
    }

    void traverseInOrderNonRecursive(BST root) {
        Stack<BST> stack = new Stack<BST>();
        BST crtNode = root;

        while (true) {
            while (crtNode != null) {
                stack.push(crtNode);
                crtNode = crtNode.Left;
            }
            
            if (stack.empty()) break;

            BST node = stack.pop();
            System.out.printf("%d ", node.Value);
            crtNode = node.Right;
        }

        System.out.printf("\n");
    }

    void traverseIndented(BST root, int level) {
        for (int i = 0; i < level; i++)
            System.out.printf("   ");
        System.out.printf("%d\n", root.Value);
        if (root.Left != null)
            traverseIndented(root.Left, level + 1);
        if (root.Right != null)
            traverseIndented(root.Right, level + 1);
    }

    static class BST {
        int Value;
        BST Left, Right;

        public BST(int V, BST L, BST R) {
            Value = V;
            Left = L;
            Right = R;
        }
    }
}