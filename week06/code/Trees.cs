public static class Trees
{
    /// <summary>
    /// Creates a balanced BinarySearchTree from a sorted list of integers.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sorted)
    {
        BinarySearchTree tree = new();
        InsertMiddle(sorted, tree, 0, sorted.Length - 1);
        return tree;
    }

    /// <summary>
    /// Recursively inserts the middle element from a (sub)array into the tree,
    /// followed by the middle elements of left and right subarrays.
    /// </summary>
    private static void InsertMiddle(int[] sorted, BinarySearchTree tree, int first, int last)
    {
        // ✅ Problem 5: Balanced Tree Construction
        if (first > last)
            return;

        int mid = (first + last) / 2;
        tree.Insert(sorted[mid]);

        // Recursively insert left and right halves
        InsertMiddle(sorted, tree, first, mid - 1);
        InsertMiddle(sorted, tree, mid + 1, last);
    }
}
