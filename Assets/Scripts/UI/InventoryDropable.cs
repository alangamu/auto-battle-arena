using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoFantasy.Scripts.UI
{
    public class InventoryDropable : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private ItemEvent addItemToInventory;
        [SerializeField]
        private ActiveHeroSO activeHero;
        [SerializeField]
        private HeroRuntimeSet roster;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                IItemUI itemType = eventData.pointerDrag.GetComponent<IItemUI>();

                if (itemType.IsInventory)
                {
                    return;
                }

                if (itemType != null)
                {
                    addItemToInventory.Raise(itemType.ItemRef);

                    //TODO: maybe get rid of taking the hero out of the roster and access directly to the active hero
                    Hero hero = roster.GetHeroById(activeHero.ActiveHero.GetHeroId());
                    if (hero != null)
                    {
                        hero.RemoveItem(itemType.ItemRef);
                        //if (!string.IsNullOrEmpty(hero.SkillId))
                        //{
                        //    hero.SetSkillId(string.Empty);
                        //}
                    }
                }
            }
        }
    }
}