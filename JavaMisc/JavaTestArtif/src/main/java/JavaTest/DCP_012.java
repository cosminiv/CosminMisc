// There exists a staircase with N steps, and you can climb up either 1 or 2 steps at a time. 
// Given N, write a function that returns the number of unique ways you can climb the staircase. The order of the steps matters.
// Also solve for an arbitrary list of steps you can climb
//
package JavaTest;
import java.util.HashMap;
import java.util.List;
import java.util.ArrayList;

class DCP_012{
    void test(){
        // int c = solve(4, new int[] {1, 2});
        // int c = solve(4, new int[] {3});
        int c = solve(20, new int[] {3, 14, 8});
        System.out.printf("\nResult = %d", c);
    }

    int solve(int N, int[] possibleSteps){
        HashMap<Integer, List<Solution>> solutions = new HashMap<Integer, List<Solution>>();
        
        for (int n = 1; n <= N; n++){
            for (int posSteps: possibleSteps){
                List<Solution> crtSolution = solutions.get(n);
                if (crtSolution == null) crtSolution = new ArrayList<Solution>();
                if (posSteps == n) crtSolution.add(new Solution(posSteps));
                List<Solution> smallerSol = solutions.get(n - posSteps);

                if (smallerSol != null)
                    for (Solution sol: smallerSol){
                        Solution newSol = new Solution((ArrayList<Integer>)sol.Steps.clone());
                        newSol.Steps.add(posSteps);
                        crtSolution.add(newSol);
                    }

                solutions.put(n, crtSolution);
            }
        }

        // Display solutions
        for (Solution s: solutions.get(N))
            System.out.println(s);

        return solutions.get(N).size();
    }

    class Solution {
        ArrayList<Integer> Steps = new ArrayList<Integer>();

        Solution(int s){
            Steps.add(s);
        }

        Solution(ArrayList<Integer> steps){
            Steps = steps;
        }
        
        @Override
        public String toString(){
            StringBuilder sb = new StringBuilder();
            
            for (Integer s: Steps){
                sb.append(s);
                sb.append(", ");
            }
            
            if (sb.length() > 0)
                sb = sb.delete(sb.length() - 2, sb.length());

            return sb.toString();
        }
    }
}

