using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IAnimationController 
    {
        void SetWeaponType(WeaponTypeSO weaponType);
        void SetAnimator(Animator animator);
        void Run();
        void Attack();
        void Idle();
        void GetHit();
        void Death();
    }
}