public interface IItemContainer {
    bool ContainsItem(Item item);
    int ItemCount(Item item);
    bool RemoveItem(Item item);
    bool CanAddItem(Item item, int amount = 1);
    bool AddItem(Item item);
}
