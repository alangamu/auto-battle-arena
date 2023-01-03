using AutoFantasy.Scripts.UI.Mission;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(menuName = "Database/MissionDatabase")]
    public class DatabaseMission : Database<Mission>
    {
        public Mission GetMission(int missionId)
        {
            return Items.Find(x => x.MissionId == missionId);
        }
    }
}