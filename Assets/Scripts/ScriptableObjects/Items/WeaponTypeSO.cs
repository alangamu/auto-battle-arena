using AutoFantasy.Scripts.ScriptableObjects.MovementTypes;
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
        public MovementTypeSO AttackMovementType;

        //TODO: review this again later when you have the left animation for all weapons
        public bool IsLeftHanded;
    }
}