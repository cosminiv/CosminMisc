package JavaTest.Misc;

public class BitVectorTest {
    public void test() {
        BitVector bv = new BitVector(1000);
        
        System.out.printf("0: %b, expected false \n", bv.search(0));

        bv.insert(7);        
        System.out.printf("7: %b, expected true \n", bv.search(7));

        bv.insert(0);
        System.out.printf("0: %b, expected true \n", bv.search(0));
        
        bv.insert(20);
        System.out.printf("20: %b, expected true \n", bv.search(20));
        System.out.printf("8: %b, expected false \n", bv.search(8));
        
        bv.insert(200);
        System.out.printf("200: %b, expected true \n", bv.search(200));

        bv.insert(999);
        System.out.printf("999: %b, expected true \n", bv.search(999));

        bv.delete(20);
        System.out.printf("20: %b, expected false \n", bv.search(20));

        // delete again. this should not change the result
        bv.delete(20);
        System.out.printf("20: %b, expected false \n", bv.search(20));

        bv.delete(8);
    }

    static class BitVector {
        private long[] data;
        private final int WORD_SIZE = 64;

        BitVector(int size) {
            int wordCount = size / WORD_SIZE;
            if (size % WORD_SIZE != 0)
                wordCount++;
            data = new long[wordCount];
        }
        
        void insert(int n) {
            int wordIndex = n / WORD_SIZE;
            long bit = 1 << (n % WORD_SIZE);
            data[wordIndex] = data[wordIndex] | bit;
        }

        void delete(int n) {
            int wordIndex = n / WORD_SIZE;
            long bit = 1 << (n % WORD_SIZE);
            data[wordIndex] = data[wordIndex] & (~bit);
        }

        boolean search(int n) {
            int wordIndex = n / WORD_SIZE;
            long bit = 1 << (n % WORD_SIZE);
            long res = data[wordIndex] & bit;
            return res != 0;
        }
    }
}