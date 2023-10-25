using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts
{
    public class Init : MonoBehaviour
    {
        [SerializeField]
        private GameEvent loadInitialData;

        [SerializeField]
        private ActiveHeroSO rosterActiveHero;
        [SerializeField]
        private ActiveHeroSO tavernActiveHero;
        //[SerializeField]
        //private HeroRuntimeSet roster;

        [SerializeField]
        private CitySO _initialCity;

        [SerializeField]
        private StringVariable rosterJson;
        [SerializeField]
        private HeroRuntimeSet _heroes;
        [SerializeField]
        private StringVariable inventoryJson;
        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private BoolVariable _isNewGameVariable;

        [SerializeField]
        protected GameEvent _generateMap;
        [SerializeField]
        protected GameEvent _loadMap;
        [SerializeField]
        private GameEvent _spawnMapHeroesEvent;
        [SerializeField]
        private GameEvent _spawnMapEnemiesEvent;
        [SerializeField]
        private GameEvent _spawnMapCitiesEvent;
        [SerializeField]
        private GameEvent _showFogOfWarEvent;
        [SerializeField]
        private GameEvent _nextTurnEvent;
        [SerializeField]
        private IntVariable _gold;
        [SerializeField]
        private IntVariable _gems;
        [SerializeField]
        private IntVariable _activeHeroIndex;

        private void LoadInitialData()
        {
            if (_isNewGameVariable.Value)
            {
                //TODO: Make the default heroes

                ResetHeroesPosition();
                ResetEnemiesDefeated();
                ClearInventory();
                InitialResources();
                ClearHeroesInventory();
                _generateMap.Raise();
                _activeHeroIndex.SetValue(_heroes.Items.Count - 1);
            }
            else
                _loadMap.Raise();

            //_isNewGameVariable.Value ? _generateMap.Raise() : _showFogOfWarEvent.Raise();

            SetActiveHeroes();
            _showFogOfWarEvent.Raise();
            _spawnMapCitiesEvent.Raise();
            _spawnMapEnemiesEvent.Raise();
            _spawnMapHeroesEvent.Raise();
            
            _nextTurnEvent.Raise();

            //CheckForActiveHero();
            //tavernRoster.OnChange += CheckForActiveHero;
            //print("loading tavern");
            //JsonUtility.FromJsonOverwrite(tavernJson.Value, tavernHeroes);
            //print("loading roster");
            //JsonUtility.FromJsonOverwrite(rosterJson.Value, rosterHeroes);
            //print("loading inventory");
            //JsonUtility.FromJsonOverwrite(inventoryJson.Value, inventory);
        }

        private void ResetEnemiesDefeated()
        {
            MapEnemyStageSO[] enemyStages = Resources.LoadAll<MapEnemyStageSO>("EnemyStages");

            foreach (var item in enemyStages)
            {
                item.SetIsDefeated(false);
            }
        }

        private void ClearHeroesInventory()
        {
            foreach (var item in _heroes.Items)
            {
                item.HeroInventory.Clear();
            }
        }

        private void InitialResources()
        {
            _gold.SetValue(0);
            _gems.SetValue(0);
        }

        private void ClearInventory()
        {
            inventory.Items.Clear();
        }

        private void ResetHeroesPosition()
        {
            foreach (var item in _heroes.Items)
            {
                item.MapPositionQ = _initialCity.Q;
                item.MapPositionR = _initialCity.R;
            }
        }

        private void OnDestroy()
        {
            //print("saving tavern");
            //tavernJson.Value = JsonUtility.ToJson(tavernHeroes);
            //print("saving roster");
            //rosterJson.Value = JsonUtility.ToJson(rosterHeroes);
            //print("saving inventory");
            //inventoryJson.Value = JsonUtility.ToJson(inventory);
            //print("saving teams");
            //teamsJson.Value = JsonUtility.ToJson(teams);
        }

        private void SetActiveHeroes()
        {
            if (_heroes.Items[0] != null)
            {
                rosterActiveHero.SetHero(_heroes.Items[0]);
            }
        }

        private void OnEnable()
        {
            loadInitialData.OnRaise += LoadInitialData;
        }

        private void OnDisable()
        {
            //tavernRoster.OnChange -= CheckForActiveHero;
            loadInitialData.OnRaise -= LoadInitialData;
        }

        private void Start()
        {
            //TODO: remove form here;
            loadInitialData.Raise();
        }
    }
}