using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class HeroHeadVisuals : MonoBehaviour
    {
        [SerializeField] 
        private DatabaseColor skinColor;
        [SerializeField] 
        private DatabaseColor hairColor;
        [SerializeField] 
        private DatabaseColor eyeColor;
        [SerializeField] 
        private DatabaseColor scarColor;
        [SerializeField] 
        private DatabaseColor bodyArtColor;

        [SerializeField] 
        private GameObject[] maleHeads;
        [SerializeField] 
        private GameObject[] femaleHeads;

        [SerializeField] 
        private GameObject[] maleEyebrows;
        [SerializeField] 
        private GameObject[] femaleEyebrows;

        [SerializeField]
        private GameObject[] maleHelmet;
        [SerializeField]
        private GameObject[] femaleHelmet;

        [SerializeField] 
        private GameObject[] hair;

        [SerializeField]
        private GameObject _parentGameObject;

        [SerializeField]
        private ArmorTypeSO _helmetType;
        [SerializeField]
        private ItemTypeSO _armorType;

        [SerializeField]
        private DatabaseItem _databaseItem;

        private List<GameObject> _armedBody;
        private IHeroController _heroController;
        private Hero _hero;

        public void Generate()
        {
            ClearLists();

            var item = _hero.ThisHeroData.HeroInventory.Find(x => x.ItemWeareableTypeId == _helmetType.WeareableTypeId && x.ItemTypeId == _armorType.ItemTypeId);

            if (item != null)
            {
                ArmorSO armorItem = _databaseItem.GetItem(item) as ArmorSO;
                int prefabIndex = armorItem.PrefabIndexOnHero - 1;
                _armedBody.Add(_hero.IsMale ? maleHelmet[prefabIndex] : femaleHelmet[prefabIndex]);

                ActivateBody(); 
                return;
            }

            _armedBody.Add(_hero.IsMale ? maleHeads[_hero.HeadIndex] : femaleHeads[_hero.HeadIndex]) ;
            _armedBody.Add(hair[_hero.HairIndex]);
            _armedBody.Add(_hero.IsMale ? maleEyebrows[_hero.EyebrowsIndex] : femaleEyebrows[_hero.EyebrowsIndex]);

            ActivateBody();
        }

        public void SetHero(Hero hero)
        {
            _hero = hero;
            _hero.OnInventoryChanged += Generate;
            Generate();
        }

        private void ClearLists()
        {
            _armedBody = new List<GameObject>();

            foreach (var item in hair)
            {
                item.SetActive(false);
            }

            foreach (var item in maleHeads)
            {
                item.SetActive(false);
            }

            foreach (var item in maleEyebrows)
            {
                item.SetActive(false);
            }

            foreach (var item in femaleHeads)
            {
                item.SetActive(false);
            }

            foreach (var item in femaleEyebrows)
            {
                item.SetActive(false);
            }

            foreach (var item in maleHelmet)
            {
                item.SetActive(false);
            }

            foreach (var item in femaleHelmet)
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
                itemMaterial.SetColor("_Color_Eyes", eyeColor.Items[_hero.EyeColorIndex]);
                itemMaterial.SetColor("_Color_Scar", scarColor.Items[_hero.ScarColorIndex]);
                itemMaterial.SetColor("_Color_BodyArt", bodyArtColor.Items[_hero.BodyArtColorIndex]);
                itemMaterial.SetColor("_Color_Hair", hairColor.Items[_hero.HairColorIndex]);
            }
        }

        private void OnEnable()
        {
            if (_parentGameObject.TryGetComponent(out _heroController))
            {
                _heroController.OnSetHero += SetHero;
            }

        }

        private void OnDisable()
        {
            if (_heroController != null)
            {
                _heroController.OnSetHero -= SetHero;
            }

            if (_hero != null)
            {
                _hero.OnInventoryChanged -= Generate;
            }
        }
    }
}