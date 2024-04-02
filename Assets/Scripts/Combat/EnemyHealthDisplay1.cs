using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RPG.Attributes;
namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        // Start is called before the first frame update
        Health EnemyHealth;
        Fighter player;
        public void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Fighter>();            
        }
        public void Update()
        {
            if(EnemyHealth = player.GetCombatTargetHealth())
            {

                GetComponent<TextMeshProUGUI>().text = string.Format("{0:0}%", EnemyHealth.GetHealthPercentage());
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = "--";
            }

        }
    }
}

