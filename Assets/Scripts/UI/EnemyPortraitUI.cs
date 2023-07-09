﻿using AutoFantasy.Scripts.Enemy;
using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace AutoFantasy.Scripts.UI
{
    public class EnemyPortraitUI : MonoBehaviour
    {
        [SerializeField]
        private Camera _heroCamera;
        [SerializeField]
        private RawImage _rawImage;
        [SerializeField]
        private Transform _enemyVisualTransform;
        [SerializeField]
        private EnemyController _enemyController;

        public void Initialize(EnemySO enemy)
        {
            var tex = new RenderTexture(512, 512, 16);
            _heroCamera.targetTexture = tex;
            _rawImage.texture = tex;

            GameObject enemyObject = _enemyController.Initialize(enemy.EnemyVisualPrefab);

            if (enemyObject.TryGetComponent(out IAnimationMovementController animationMovementController))
            {
                animationMovementController.Animate(enemy.Weapon.WeaponType.IdleAnimationClipName);
            }
            if (enemyObject.TryGetComponent(out IWeaponController weaponController))
            {
                weaponController.ShowWeapon(enemy.Weapon);
            }

            enemyObject.transform.SetParent(_enemyVisualTransform);
            enemyObject.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}