﻿using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        private void Start()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return; ;
            
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits )
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;

            }
            return false;
        }

        private bool InteractWithMovement()
        {

            bool hasHit = Physics.Raycast(GetMouseRay(), out RaycastHit hit);
            if (hasHit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
