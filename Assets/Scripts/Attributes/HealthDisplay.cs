using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        // Start is called before the first frame update
        Health health;
        public void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }
        public void Update()
        {
            GetComponent<TextMeshProUGUI>().text = string.Format("{2}/{1}({0:0}%)", health.GetPercentage(), health.GetMaxHealth(),health.GetHealth());
        }
    }
}

