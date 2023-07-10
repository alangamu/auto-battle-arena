using AutoFantasy.Scripts.Heroes;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class PreFightUIController : MonoBehaviour
    {
        [SerializeField]
        private HeroRuntimeSet _heroes;
        [SerializeField]
        private HeroRuntimeSet _fightHeroes;
        [SerializeField]
        private GameObject _preFightPopup;
        [SerializeField]
        private TileRuntimeSet _tiles;
        [SerializeField]
        private GameObject _enemyPortraitUIPrefab; 
        [SerializeField]
        private GameObject _heroPortraitUIPrefab; 
        [SerializeField]
        private Transform _enemiesTransform;
        [SerializeField]
        private Transform _fightHeroesTransform;
        [SerializeField]
        private GameObjectGameEvent _tileShowEnemyEvent;
        [SerializeField]
        private IntVariable _maxCombatHeroes;
        [SerializeField]
        private TileTypeSO _heroTileType;
        [SerializeField]
        private Button _cancelButton;
        [SerializeField]
        private Button _fightButton;
        [SerializeField]
        private MapEnemyStageSO _activeEnemyStage;

        private MapEnemyStageSO[] _enemyStages;

        private void OnEnable()
        {
            _preFightPopup.SetActive(false);
            _enemyStages = Resources.LoadAll<MapEnemyStageSO>("EnemyStages");
            _tileShowEnemyEvent.OnRaise += ShowPopup;
            _cancelButton.onClick.AddListener(HidePopup);
        }

        private void OnDisable()
        {
            _tileShowEnemyEvent.OnRaise -= ShowPopup;
            _cancelButton.onClick.RemoveAllListeners();
        }

        private void HidePopup()
        {
            _preFightPopup.SetActive(false);
        }

        private void ShowPopup(GameObject tile)
        {
            if (tile.TryGetComponent(out ITile enemyTile))
            {
                _preFightPopup.SetActive(true);

                FillEnemiesPanel(enemyTile);

                List<ITile> neighborTiles = _tiles.GetNeighboursTiles(enemyTile.GetHex(), 2);

                if (neighborTiles != null && neighborTiles.Count > 0)
                {
                    List<ITile> heroesTiles = neighborTiles.FindAll(x => x.TileType == _heroTileType);

                    if (heroesTiles != null && heroesTiles.Count > 0)
                    {
                        print($"{heroesTiles.Count} Heroes found!");
                        if (heroesTiles.Count > _maxCombatHeroes.Value)
                        {
                            _fightButton.enabled = false;
                            FillNearbyHeroesPanel();
                            return;
                        }

                        FillFightHeroesPanel(heroesTiles);
                        _fightButton.enabled = true;
                    }
                }
            }
        }

        private void FillEnemiesPanel(ITile enemyTile)
        {
            _activeEnemyStage.SetEnemies(_enemyStages.ToList().Find(x => x.Q == enemyTile.GetHex().Q && x.R == enemyTile.GetHex().R).Enemies);

            foreach (Transform item in _enemiesTransform)
            {
                Destroy(item.gameObject);
            }

            foreach (var enemy in _activeEnemyStage.Enemies)
            {
                GameObject enemyGO = Instantiate(_enemyPortraitUIPrefab, _enemiesTransform);

                if (enemyGO.TryGetComponent(out EnemyPortraitUI enemyPortrait))
                {
                    enemyPortrait.Initialize(enemy);
                }
            }
        }

        private void FillFightHeroesPanel(List<ITile> heroesTiles)
        {
            _fightHeroes.Items.Clear();
            foreach (Transform hero in _fightHeroesTransform)
            {
                Destroy(hero.gameObject);
            }

            foreach (var item in heroesTiles)
            {
                GameObject fightHeroGO = Instantiate(_heroPortraitUIPrefab, _fightHeroesTransform);
                Hero hero = _heroes.Items.Find(x => x.MapPositionQ == item.GetHex().Q && x.MapPositionR == item.GetHex().R);
                _fightHeroes.Add(hero);
                if (fightHeroGO.TryGetComponent(out IHeroController heroController))
                {
                    heroController.SetHero(hero);
                }
            }
        }

        private void FillNearbyHeroesPanel()
        {

        }
    }
}