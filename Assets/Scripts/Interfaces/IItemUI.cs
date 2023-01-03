using AutoFantasy.Scripts.ScriptableObjects.Items;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IItemUI
    {
        Item ItemRef { get; }
        bool IsInventory { get; set; }
        void Setup(Item item);
        void Clear();
    }
}