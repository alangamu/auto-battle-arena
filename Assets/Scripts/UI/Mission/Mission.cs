using AutoFantasy.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.UI.Mission
{
    [Serializable]
    public class Mission
    {
        public int MissionId;
        public string MissionName;
        public MissionDifficultySO MissionDifficulty;
        public MissionTypeSO MissionType;
        public MissionRewardTypeSO GameCurrencyLevel;
        public MissionRewardTypeSO LootLevel;
        public MissionRewardTypeSO CrownFavorLevel;
        public int SellPrice;
        public bool Owned;
        //TODO: maybe imageId
        public Sprite MissionImage;
        public List<MissionStageEnemies> Rounds;
    }
}