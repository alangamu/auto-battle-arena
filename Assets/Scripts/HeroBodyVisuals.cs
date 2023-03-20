using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class HeroBodyVisuals : MonoBehaviour
    {
        [SerializeField] 
        private DatabaseColor skinColor;

        [SerializeField] 
        private GameObject[] maleTorsos;
        [SerializeField] 
        private GameObject[] femaleTorsos;

        [SerializeField] 
        private GameObject[] maleArmUpperRight;
        [SerializeField] 
        private GameObject[] femaleArmUpperRight;

        [SerializeField] 
        private GameObject[] maleArmUpperLeft;
        [SerializeField] 
        private GameObject[] femaleArmUpperLeft;

        [SerializeField] 
        private GameObject[] maleArmLowerRight;
        [SerializeField] 
        private GameObject[] femaleArmLowerRight;

        [SerializeField] 
        private GameObject[] maleArmLowerLeft;
        [SerializeField] 
        private GameObject[] femaleArmLowerLeft;

        [SerializeField] 
        private GameObject[] maleHandLeft;
        [SerializeField] 
        private GameObject[] femaleHandLeft;

        [SerializeField] 
        private GameObject[] maleHandRight;
        [SerializeField] 
        private GameObject[] femaleHandRight;

        [SerializeField] 
        private GameObject[] maleHips;
        [SerializeField] 
        private GameObject[] femaleHips;

        [SerializeField] 
        private GameObject[] maleLegRight;
        [SerializeField] 
        private GameObject[] femaleLegRight;

        [SerializeField] 
        private GameObject[] maleLegLeft;
        [SerializeField] 
        private GameObject[] femaleLegLeft;

        [SerializeField]
        private GameEvent refreshUI;

        [SerializeField]
        private ArmorTypeSO pantsType;
        [SerializeField]
        private ArmorTypeSO chestType;
        [SerializeField]
        private ArmorTypeSO bootsType;
        [SerializeField]
        private ArmorTypeSO glovesType;
        [SerializeField]
        private ArmorTypeSO shouldersType;
        [SerializeField]
        private ArmorTypeSO bracersType;

        [SerializeField]
        private ItemDatabase databaseItem;

        [SerializeField]
        private ItemTypeSO armorType;

        private List<GameObject> _armedBody;
        private Hero _hero;
        private IHeroController _heroController;

        private void Generate()
        {
            ClearLists();

            //TODO: Check this for possible nullRef
            ActivateBodyPartByTypeId(_hero.IsMale ? maleTorsos : femaleTorsos, chestType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleHips : femaleHips, pantsType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleLegLeft : femaleLegLeft, bootsType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleLegRight : femaleLegRight, bootsType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleHandLeft : femaleHandLeft, glovesType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleHandRight : femaleHandRight, glovesType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleArmUpperRight : femaleArmUpperRight, shouldersType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleArmUpperLeft : femaleArmUpperLeft, shouldersType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleArmLowerRight : femaleArmLowerRight, bracersType.WeareableTypeId);
            ActivateBodyPartByTypeId(_hero.IsMale ? maleArmLowerLeft : femaleArmLowerLeft, bracersType.WeareableTypeId);

            ActivateBody();
        }

        private void ActivateBodyPartByTypeId(GameObject[] partsList, int weareableTypeId)
        {
            var item = _hero.ThisHeroData.HeroInventory.Find(x => x.ItemWeareableTypeId == weareableTypeId && x.ItemTypeId == armorType.ItemTypeId);

            if (item != null)
            {
                ArmorSO armorItem = databaseItem.GetItem(item) as ArmorSO;
                _armedBody.Add(partsList[armorItem.PrefabIndexOnHero]);
                return;
            }
            _armedBody.Add(partsList[0]);
        }

        private void SetHero(Hero hero)
        {
            _hero = hero;
            //TODO: make a real event to generate
            Generate();
        }

        private void ClearLists()
        {
            _armedBody = new List<GameObject>();

            ClearList(maleTorsos);
            ClearList(maleArmUpperRight);
            ClearList(maleArmUpperLeft);
            ClearList(maleArmLowerRight);
            ClearList(maleArmLowerLeft);
            ClearList(maleHandRight);
            ClearList(maleHandLeft);
            ClearList(maleHips);
            ClearList(maleLegRight);
            ClearList(maleLegLeft);

            ClearList(femaleTorsos);
            ClearList(femaleArmUpperRight);
            ClearList(femaleArmUpperLeft);
            ClearList(femaleArmLowerRight);
            ClearList(femaleArmLowerLeft);
            ClearList(femaleHandRight);
            ClearList(femaleHandLeft);
            ClearList(femaleHips);
            ClearList(femaleLegRight);
            ClearList(femaleLegLeft);
        }

        private void ClearList(GameObject[] list)
        {
            foreach (var item in list)
            {
                item.SetActive(false);
            }
        }

        private void ActivateBody()
        {
            foreach (var item in _armedBody)
            {
                item.SetActive(true);
                Material itemMaterial = item.GetComponent<SkinnedMeshRenderer>().material;
                itemMaterial.SetColor("_Color_Skin", skinColor.Items[_hero.SkinColorIndex]);
            }
        }

        private void OnEnable()
        {
            if (TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += SetHero;
            }
            refreshUI.OnRaise += Generate;
        }

        private void OnDisable()
        {
            if (_heroController != null)
            {
                _heroController.OnSetHero -= SetHero;
            }
            refreshUI.OnRaise -= Generate;
        }
    }
}