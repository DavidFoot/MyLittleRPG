using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    BaseStats stats;
    public void Awake()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
    }
    public void Update()
    {
        GetComponent<TextMeshProUGUI>().text = string.Format("{0:0}", stats.GetLevel());
    }
}
