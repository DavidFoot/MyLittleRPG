using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<PlayableDirector>().played += OnBegin;
        GetComponent<PlayableDirector>().stopped += OnEnd;
    }
    private void OnBegin(PlayableDirector x)
    {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }
    private void OnEnd(PlayableDirector x)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
