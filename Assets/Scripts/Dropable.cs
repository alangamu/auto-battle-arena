using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoFantasy.Scripts
{
    public class Dropable : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected HeroEvent dropHeroEvent;

        public virtual void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                IHeroUI rosterHeroUI = eventData.pointerDrag.GetComponent<IHeroUI>();

                if (rosterHeroUI != null)
                {
                    dropHeroEvent.Raise(rosterHeroUI.Hero);
                }
            }
        }
    }
}