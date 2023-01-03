using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class RecipeListUI : MonoBehaviour
    {
        [SerializeField]
        private ItemTypeEvent itemTypeEvent;
        [SerializeField]
        private ItemTypeSO itemType;

        private void Start()
        {
            ItemTypeEventRaise();
        }

        private void ItemTypeEventRaise()
        {
            itemTypeEvent.Raise(itemType);
        }
    }
}