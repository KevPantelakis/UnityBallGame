using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    private GameObject player; // Reference to the player object



	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position;
	}


    // Mates the particule system rotate so that the trail looks nice
    public void ParticulesFollow(Vector3 direction)
    {
        direction = new Vector3(-direction.x,direction.y,direction.z);
        float angle = Vector3.SignedAngle(new Vector3(0, 0, -1), direction, new Vector3(0, 1, 0));
        Quaternion quat = Quaternion.Euler(0, angle, 0);
        quat = Quaternion.Inverse(quat);
        transform.rotation = Quaternion.Lerp(transform.rotation, quat, 2f*Time.deltaTime);
    }
}
