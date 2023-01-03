using AutoFantasy.Scripts;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class DropableTransportSlot : Dropable
    {
        [SerializeField] 
        private IntVariable activeTransportSlot;
        [SerializeField] 
        private int slotIndex;

        public override void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                IHeroUI heroUI = eventData.pointerDrag.GetComponent<IHeroUI>();

                if (heroUI != null)
                {
                    activeTransportSlot.Value = slotIndex;
                    dropHeroEvent.Raise(heroUI.Hero);
                }
            }
        }
    }
}