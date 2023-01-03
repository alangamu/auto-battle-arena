using System;
using UnityEditor;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "Items/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public string ItemId;
        public string ItemName;
        public string ItemDescription;
        public Sprite ItemSprite;
        public ItemTypeSO ItemType;
        public bool IsStackable;

        private void OnEnable()
        {
            SetId();
        }

        public void SetId()
        {
            if (string.IsNullOrEmpty(ItemId))
            {
                Guid guid = Guid.NewGuid();
                ItemId = guid.ToString();
                EditorUtility.SetDirty(this);
                Debug.Log($"saving item {ItemName}");
            }
        }
    }
}