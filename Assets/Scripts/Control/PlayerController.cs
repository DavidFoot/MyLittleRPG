using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        private static RaycastHit[] GetAllRayCast(Ray myRay)
        {
            RaycastHit[] hitList = Physics.RaycastAll(GetMouseRay());
            return hitList;
        }
        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Health>().IsDead()) { return; }
            if (InterractWithCombat()) return;
            if (InterractWithMovement()) return;
            //Debug.Log("Not There, end of the World");           

        }

        private bool InterractWithCombat()
        {
            RaycastHit[] hitList = GetAllRayCast(GetMouseRay());
            foreach (RaycastHit hit in hitList)
            {

                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;


                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InterractWithMovement()
        {
            Ray lastRay = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(lastRay, out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point,1);
                    return true;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
