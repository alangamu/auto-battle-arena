using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.UI
{
    public class WinStageRewardUI : MonoBehaviour
    {
        [SerializeField]
        private ItemDatabase _itemDatabase;
        [SerializeField]
        private Transform parentTransform;
        [SerializeField]
        private GameObject itemUIPrefab;
        [SerializeField]
        private ItemEvent addItemToInventory;
        [SerializeField]
        private ItemTypeSO goldType;
        [SerializeField]
        private IntVariable gold;
        [SerializeField]
        private ItemTypeSO gemType;
        [SerializeField]
        private IntVariable gems;
        [SerializeField]
        private ItemTypeSO armorType;
        [SerializeField]
        private ItemTypeSO weaponType;

        private List<Item> _rewards;

        public void SetRewards(List<Reward> rewards)
        {
            _rewards = new List<Item>();

            foreach (var reward in rewards)
            {
                float randomNumber = Random.Range(0f, 1f);
                if (randomNumber <= reward.Pct)
                {
                    CollectReward(reward);
                }
            }

            ShowRewards();
        }

        private void ShowRewards()
        {
            foreach (var item in _rewards)
            {
                GameObject itemUIObject = Instantiate(itemUIPrefab, parentTransform);
                if (itemUIObject.TryGetComponent(out IItemUI itemUI))
                {
                    itemUI.Setup(item);
                }
            }
        }

        private void CollectReward(Reward reward)
        {
            Item item = _itemDatabase.CreateNewItem(reward.RewardItem, reward.ItemRarity);
            ItemTypeSO itemType = reward.RewardItem.ItemType;

            if (itemType == armorType || itemType == weaponType)
            {
                addItemToInventory.Raise(item);
                _rewards.Add(item);
                return;
            }

            if (itemType == goldType)
            {
                HandleResource(reward, item, gold);
                return;
            }

            if (itemType == gemType)
            {
                HandleResource(reward, item, gems);
            }
        }

        private void HandleResource(Reward reward, Item item, IntVariable resource)
        {
            int resourceAmount = Random.Range(reward.MinQty, reward.MaxQty);
            resource.ApplyChange(resourceAmount);

            var rewardItem = _rewards.Find(x => x.ItemRefId.Equals(reward.RewardItem.ItemId));
            if (rewardItem != null)
            {
                rewardItem.AddAmount(resourceAmount);
                return;
            }
            item.SetAmount(resourceAmount);
            _rewards.Add(item);
            return;
        }
    }
}