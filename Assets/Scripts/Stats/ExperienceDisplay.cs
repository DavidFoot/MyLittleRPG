using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        // Start is called before the first frame update
        Experience experience;
        public void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }
        public void Update()
        {
            GetComponent<TextMeshProUGUI>().text = string.Format("{0:0}", experience.GetPoints());
        }
    }
}

