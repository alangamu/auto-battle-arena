using AutoFantasy.Scripts.ScriptableObjects.Items;
using System;
using UnityEngine;

namespace AutoFantasy.Scripts.Interfaces
{
    public interface IWeaponController 
    {
        event Action<WeaponSO> OnSetWeapon;
        Transform GetWeaponTransform();
    }
}