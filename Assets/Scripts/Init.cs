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
        [SerializeField]
        private HeroRuntimeSet roster;
        [SerializeField]
        private HeroRuntimeSet tavernRoster;

        [SerializeField]
        private TeamsSO teams;
        [SerializeField]
        private IntVariable maxTeamSize;
        [SerializeField]
        private StringVariable teamsJson;

        [SerializeField]
        private StringVariable tavernJson;
        [SerializeField]
        private HeroRuntimeSet tavernHeroes;
        [SerializeField]
        private StringVariable rosterJson;
        [SerializeField]
        private HeroRuntimeSet rosterHeroes;
        [SerializeField]
        private StringVariable inventoryJson;
        [SerializeField]
        private Inventory inventory;

        private void OnEnable()
        {
            loadInitialData.OnRaise += LoadInitialData_OnRaise;
        }

        private void Start()
        {
            SetTeams();
            SetActiveHeroes();
            CheckForActiveHero();
            tavernRoster.OnChange += CheckForActiveHero;
        }

        private void LoadInitialData_OnRaise()
        {
            print("loading tavern");
            JsonUtility.FromJsonOverwrite(tavernJson.Value, tavernHeroes);
            print("loading roster");
            JsonUtility.FromJsonOverwrite(rosterJson.Value, rosterHeroes);
            print("loading inventory");
            JsonUtility.FromJsonOverwrite(inventoryJson.Value, inventory);
            print("loading teams");
            JsonUtility.FromJsonOverwrite(teamsJson.Value, teams);
        }

        private void OnDisable()
        {
            tavernRoster.OnChange -= CheckForActiveHero;
            loadInitialData.OnRaise -= LoadInitialData_OnRaise;
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
            if (roster.Items[0] != null)
            {
                rosterActiveHero.SetHero(roster.Items[0]);
            }

            if (tavernRoster.Items[0] != null)
            {
                tavernActiveHero.SetHero(tavernRoster.Items[0]);
            }
        }

        private void SetTeams()
        {
            if (teams.Teams.Count == 0)
            {
                for (int i = 0; i < maxTeamSize.Value; i++)
                {
                    Team team = new Team();
                    teams.Teams.Add(team);
                    if (i == 0)
                    {
                        teams.SetActiveTeam(i);
                    }
                }
            }
        }
    
        private void CheckForActiveHero()
        {
            if (tavernRoster.Items.Find(x => x.GetHeroId() == tavernActiveHero.ActiveHero.GetHeroId()) == null)
            {
                if (tavernRoster.Items[0] != null)
                {
                    tavernActiveHero.SetHero(tavernRoster.Items[0]);
                }
            }
        }
    }
}