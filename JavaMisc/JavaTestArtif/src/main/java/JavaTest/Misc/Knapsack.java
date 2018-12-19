// Maximize sum of values you can put in a sack with a certain capacity
//
package JavaTest.Misc;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.List;

public class Knapsack {
    public void test() {
        Item[] items = new Item[] {
            new Item(7, 4),
            new Item(15, 7),
            new Item(11, 6),
            new Item(20, 10),
        };

        int knapsackCapacity = 59;
        Item[] optimumItems = fillKnapsack(items, knapsackCapacity);

        int totalValue = 0, totalWeight = 0;
        for (Item item: optimumItems){
            System.out.printf("(%d, %d) ", item.Value, item.Weight);
            totalValue += item.Value;
            totalWeight += item.Weight;
        }
        System.out.printf("\n\nTotal value = %d \nTotal weight = %d", totalValue, totalWeight);
    }

    private Item[] fillKnapsack(Item[] items, int knapsackCapacity) {
        int maxValue = 0;
        ArrayList<Item> itemsWithMaxValue = new ArrayList<Item>();
        ArrayList<Item> currentItems = new ArrayList<Item>();
        ArrayList<Integer> currentItemIndexes = new ArrayList<Integer>();
        int currentWeight = 0;
        int currentValue = 0;
        Arrays.sort(items, new ItemWeightComparator());
        
        System.out.println();
        int iterations = 0;

        for (int itemIndex = 0; itemIndex < items.length; ) {
            Item item = items[itemIndex];
            boolean canAddItem = currentWeight + item.Weight <= knapsackCapacity;
            iterations++;
            
            if (canAddItem){
                currentWeight += item.Weight;
                currentValue += item.Value;
                currentItems.add(item);
                currentItemIndexes.add(itemIndex);
                if (currentValue > maxValue){
                    maxValue = currentValue;
                    itemsWithMaxValue = new ArrayList<Item>(currentItems);
                }
                //printItems(currentItems);
            } else {
                // backtrack - remove the last item from the current list.
                Item latestAddedItem = null;
                if (currentItems.size() > 0) {
                    int latestAddedItemIndex = currentItemIndexes.get(currentItemIndexes.size() - 1);
                    do {
                        int last = currentItems.size() - 1;
                        latestAddedItemIndex = currentItemIndexes.get(last);
                        latestAddedItem = currentItems.get(last);
                        currentValue -= latestAddedItem.Value;
                        currentWeight -= latestAddedItem.Weight;
                        currentItems.remove(last);
                        currentItemIndexes.remove(last);
                        itemIndex = latestAddedItemIndex + 1;
                        //printItems(currentItems);
                    }
                    while (currentItems.size() > 0 && itemIndex == items.length);  // remove if we tried all values for current position
                } else
                    break;
            }
        }

        //System.out.printf("%d iterations \n\n", iterations);
        return itemsWithMaxValue.toArray(new Item[itemsWithMaxValue.size()]);
    }

    void printItems(List<Item> items) {
        int val = 0;
        int strLen = 0;
        
        for (Item printItem: items) {
            String s = String.format("(%d, %d) ", printItem.Value, printItem.Weight);
            System.out.print(s);
            val += printItem.Value;
            strLen += s.length();
        }

        for (int i = 50; i > strLen; i--)
            System.out.print(" ");

        System.out.printf("Val = %d ", val);
        System.out.println();
    }

    static class Item {
        int Value;
        int Weight;

        Item(int val, int weight) {
            Value = val;
            Weight = weight;
        }
    }

    static class ItemWeightComparator implements Comparator<Item> {

        @Override
        public int compare(Item o1, Item o2) {
            return o1.Weight - o2.Weight;
        }

    }
}