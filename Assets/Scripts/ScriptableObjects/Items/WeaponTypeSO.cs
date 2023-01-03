using System.Collections.Generic;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "Items/WeaponTypeSO")]
    public class WeaponTypeSO : WeareableTypeSO
    {
        public string BoneParentName;
        public string RunAnimationClipName;
        public string DeathAnimationClipName;
        public string IdleAnimationClipName;
        public List<string> AttackAnimationClipsNames;
        public float Range;

        //TODO: review this again later when you have the left animation for all weapons
        public bool IsLeftHanded;
    }
}