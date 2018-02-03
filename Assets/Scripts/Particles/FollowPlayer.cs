using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    private GameObject player;

    // Use this for initialization
    private void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update ()
    {
        transform.position = player.transform.position;
	}

}
