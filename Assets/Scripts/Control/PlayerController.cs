using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        enum CursorType
        {
            Movement,
            Combat,
            None,
            UI
        }
        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        private void Awake()
        {
            health = GetComponent<Health>();
        }
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
            if (InterractWithUI()) { return; }
            if (health.IsDead()) { SetCursor(CursorType.None); return; }
            if (InterractWithCombat()) return;
            if (InterractWithMovement()) return;
            SetCursor(CursorType.None);          

        }

        private bool InterractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping cursorMapping = GetCursorMapping(type);
            Cursor.SetCursor(cursorMapping.texture, cursorMapping.hotspot, CursorMode.Auto);
        }
        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
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
                SetCursor(CursorType.Combat);
                return true;
            }
            return false;
        }

        private bool InterractWithMovement()
        {
            Ray lastRay = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(lastRay, out hit) ;
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point,1);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
