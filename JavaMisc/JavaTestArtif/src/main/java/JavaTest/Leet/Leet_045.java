package JavaTest.Leet;

import java.io.BufferedReader;
import java.io.FileReader;
import java.util.*;

// Given an array of non-negative integers, you are initially positioned at the first index of the array.
// Each element in the array represents your maximum jump length at that position.
// Your goal is to reach the last index in the minimum number of jumps.
public class Leet_045 {
    public void test(){
        //int[] n = {2,3,1,1,4};
        int[] n = {8,2,4,4,4,9,5,2,5,8,8,0,8,6,9,1,1,6,3,5,1,2,6,6,0,4,8,6,0,3,2,8,7,6,5,1,7,0,3,4,8,3,5,9,0,4,0,1,0,5,9,2,0,7,0,2,1,0,8,2,5,1,2,3,9,7,4,7,0,0,1,8,5,6,7,5,1,9,9,3,5,0,7,5};
        //int[] n = readNumbersFromFile();
        System.out.printf("%d \n\n", n.length);

        int res = jump(n);
        System.out.printf("\n%d", res);
    }

    public int jump(int[] nums) {
        List<Node> q = new ArrayList<Node>();
        q.add(new Node(nums[0], 0, 0));
        int pass = 1;

        while (!q.isEmpty()){
            Node node = q.remove(0);
            if (node.Index == nums.length - 1)
                return node.DistFromStart;
            for (int i = Math.min(nums.length - 1, node.Index + node.Value); i > node.Index; i--){
                int dist = node.DistFromStart + 1;
                boolean foundNode = false;

                for (int qIndex = 0; qIndex < q.size(); qIndex++){
                    Node qNode = q.get(qIndex);
                    foundNode = foundNode || qNode.Index == i;
                    if (foundNode && dist < qNode.DistFromStart){
                        qNode.DistFromStart = dist;
                        break;
                    }
                }

                if (!foundNode)
                    q.add(new Node(nums[i], i, dist));
            }

            //if (pass++ < 120) printQueue(q);
            //else break;
        }

        return -1;
    }

    int[] readNumbersFromFile() {
        String fileName = "C:\\Users\\ivanc\\Desktop\\aaa\\1.txt";
        BufferedReader br = null; 
        String inputStr = "";
        try {
            br = new BufferedReader(new FileReader(fileName));
            inputStr = br.readLine();
        }
        catch (Exception ex){
            System.out.println(ex);
        }
        String[] numbers = inputStr.split(",");
        int[] n = new int[numbers.length];
        for (int i = 0; i < numbers.length; i++)
            n[i] = Integer.parseInt(numbers[i]);
        
        return n;
    }    

    private void printQueue(List<Node> q) {
        for (Node n: q)
            System.out.printf("%d ", n.Index);
        System.out.println();
    }

    static class Node {
        int Value;
        int Index;
        int DistFromStart;

        Node(int val, int index, int dist){
            Value = val;
            Index = index;
            DistFromStart = dist;
        }
    }
}