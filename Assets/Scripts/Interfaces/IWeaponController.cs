using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IWeaponController 
    {
        event Action<WeaponSO> OnSetWeapon;
    }
}