package JavaTest.Misc;

import java.util.*;

public class StringSearch {
    public void test() {
        String[] userNames = new String[] { "abba", "adrian", "adrian1", "adrian123", "cosmin", "cosmin2" };
        String text = "Blah @abb blah @adrian, how you doin @cosmin@@@";
        List<String> result = findMentions(text, userNames);
        int a = 8;
    }

    List<String> findMentions(String text, String[] userNames) {
        ArrayList<String> result = new ArrayList<String>();

        for (int atIndex = text.indexOf("@", 0); 
            atIndex >= 0 && atIndex < text.length() - 1; 
            atIndex = text.indexOf("@", atIndex + 1)) {

            System.out.printf("\natIndex = %d\n", atIndex);
            List<Integer> matchingUsernamesIndices = new LinkedList<Integer>();
            int indexInUsername = 0;

            for (int letterIndex = atIndex + 1; letterIndex < text.length(); letterIndex++) {
                char letter = text.charAt(letterIndex);

                // Search the first letter in all usernames
                if (letterIndex == atIndex + 1) {
                    for (int i = 0; i < userNames.length; i++)
                        if (userNames[i].length() > 0 && userNames[i].charAt(0) == letter)
                            matchingUsernamesIndices.add(i);
                    System.out.printf("Matching indexes: %s\n", listToString(matchingUsernamesIndices));
                    if (matchingUsernamesIndices.isEmpty()) 
                        break;
                }

                // Search the subsequent letters starting from matchingUsernamesIndices
                else {
                    Iterator<Integer> iterator = matchingUsernamesIndices.iterator();
                    int countToRemove = 0;
                    String fullUsername = "";

                    // Check how many usernames do NOT match
                    while (iterator.hasNext()) {
                        int matchingUsernamesIndex = iterator.next();
                        String username = userNames[matchingUsernamesIndex];
                        if (username.length() < indexInUsername + 1 || username.charAt(indexInUsername) != letter){
                            countToRemove++;
                            if (username.length() == indexInUsername)
                                fullUsername = username;
                        }
                    }

                    // If nothing matches anymore, we've reached the end of the username
                    // Return the longest one we have
                    if (countToRemove == matchingUsernamesIndices.size()){
                        result.add(fullUsername);
                        break;
                    }
                    else {
                        // Only some don't match; delete them
                        iterator = matchingUsernamesIndices.listIterator();
                        while (iterator.hasNext()) {
                            int matchingUsernamesIndex = iterator.next();
                            String username = userNames[matchingUsernamesIndex];
                            if (username.length() < indexInUsername + 1 || username.charAt(indexInUsername) != letter){
                                iterator.remove();
                            }
                        }                        
                    }

                    System.out.printf("Matching indexes: %s\n", listToString(matchingUsernamesIndices));
                }

                // Filtered down to the actual username
                if (matchingUsernamesIndices.size() == 1) {
                    result.add(userNames[matchingUsernamesIndices.get(0)]);
                    break;
                }

                indexInUsername++;
            }
        }
        
        return result;
    }

    private String listToString(List<Integer> matchingUsernamesIndices) {
        String result = "";
        for (Integer i: matchingUsernamesIndices)
            result += i.toString() + ", ";
        return result;
    }
}