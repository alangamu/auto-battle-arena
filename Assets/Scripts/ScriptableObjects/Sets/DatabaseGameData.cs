using System.Collections;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu]
    public class DatabaseGameData : Database<GameData>
    {
        public void AddGameData(GameData gameData) => Items.Add(gameData);

        public void RemoveGameData(GameData gameData) => Items.Remove(gameData);
    }
}