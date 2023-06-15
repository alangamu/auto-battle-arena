using AutoFantasy.Scripts.ScriptableObjects.Items;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IWeaponController 
    {
        Transform GetWeaponTransform();
        WeaponSO GetWeapon();
        void ShowWeapon(WeaponSO weapon);
    }
}