﻿using System;
using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        private float _maxHealth;
        private float _currentHealth;
        public event Action<float> OnHealthPctChanged = delegate { };

        private void Start()
        {
            _maxHealth = GetComponent<PlayerController>().PlayerSpecs.Health;
            _currentHealth = _maxHealth;
        }
        public void ModifyHealth(float amount)
        {
            _currentHealth += amount;
            GetComponent<PlayerController>().PlayerSpecs.CurrentHealth = _currentHealth;
            float currenthealthPct = _currentHealth / _maxHealth;
            OnHealthPctChanged(currenthealthPct);
            if (_currentHealth <= 0)
            {
                //TODO: Player Dead -> Raise UI panel
                Debug.Log(_currentHealth);
            }            
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))            
                ModifyHealth(-10);            
        }
    }
}