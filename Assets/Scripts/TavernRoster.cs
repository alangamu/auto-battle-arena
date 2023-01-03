using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class TavernRoster : MonoBehaviour
    {
        [SerializeField] 
        private HeroRuntimeSet tavernRoster;
        [SerializeField] 
        private Transform contentTransform;
        [SerializeField]
        private ActiveHeroSO activeHero;
        [SerializeField] 
        private GameObject tavernHeroUIPrefab;

        private void OnEnable()
        {
            tavernRoster.OnChange += FillTavern;
        }

        private void OnDisable()
        {
            tavernRoster.OnChange -= FillTavern;
        }

        private void Start()
        {
            FillTavern();
        }

        private void FillTavern()
        {
            foreach (Transform item in contentTransform)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in tavernRoster.Items)
            {
                AddHeroUI(item);
            }
        }

        private void AddHeroUI(Hero hero)
        {
            GameObject rosterHeroUI = Instantiate(tavernHeroUIPrefab, contentTransform);
            if (rosterHeroUI.TryGetComponent(out IHeroController _heroController))
            {
                _heroController.SetHero(hero);
            }
        }
    }
}