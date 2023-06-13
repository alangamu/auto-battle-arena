using AutoFantasy.Scripts.ScriptableObjects.Items;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IAnimationController 
    {
        void SetWeaponType(WeaponTypeSO weaponType);
        void Run();
        void Attack();
        void Idle();
        void Hit();
        void Shoot();
        void GetHit();
        void Death();
    }
}