// Serialize and deserialize binary tree
//
package JavaTest.DCP;

class DCP_003 {

    public void test(){
//      1
//    2   3
//  4    5  6
        Node n1 = new Node(1,
                    new Node(2,
                        new Node(4), null
                    ),
                    new Node(3,
                        new Node(5),
                        new Node(6)
                    )
                );

        String ser = serialize(n1);
        
        //Node node1 = deserialize("{}");
        //Node node2 = deserialize("{\"value\":\"7\",\"left\":{},\"right\":{}}");
        Node node3 = deserialize(ser);

        System.out.println(ser);
        System.out.println(node3);
    }

    private String serialize(Node treeRoot) {
        StringBuilder sb = new StringBuilder();
        serialize(treeRoot, sb);
        return sb.toString();
    }

    private void serialize(Node node, StringBuilder sb) {
        sb.append("{");

        if (node != null) {
            sb.append("\"value\":\"");
            sb.append(node.value);
            sb.append("\",");

            sb.append("\"left\":");
            serialize(node.left, sb);
            sb.append(",");

            sb.append("\"right\":");
            serialize(node.right, sb);
        }

        sb.append("}");
    }

    private Node deserialize(String str){
        DeserResult result = deserialize(str, 1);
        return result.node;
    }

    private DeserResult deserialize(String str, int index){
        if (str.charAt(index) == '}')
            return new DeserResult(null, index);
        
        index += "\"value\":\"".length();
        int index2 = str.indexOf("\"", index + 1);
        String valueStr = str.substring(index, index2);

        int value = Integer.parseInt(valueStr, 0, valueStr.length(), 10);
        Node node = new Node(value);

        index = index2 + ",\"left\":{".length() + 1;        
        DeserResult leftResult = deserialize(str, index);
        index = leftResult.index;

        index += ",\"right\":{".length() + 1;
        DeserResult rightResult = deserialize(str, index);
        index = rightResult.index + 1;

        node.left = leftResult.node;
        node.right = rightResult.node;

        DeserResult result = new DeserResult(node, index);
        return result;
    }

    private static class DeserResult {
        Node node;
        int index;

        public DeserResult(Node n, int i) {
            node = n;
            index = i;
        }
    }

    private static class Node {
        int value;
        Node left;
        Node right;

        public Node(int val){
            value = val;
        }

        public Node(int val, Node L, Node R){
            value = val;
            left = L;
            right = R;
        }
    }
}


