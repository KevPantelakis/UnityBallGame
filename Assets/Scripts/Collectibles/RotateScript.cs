using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

    [SerializeField] private float rotationSpeed = 5f;

	void Update () {
	
		transform.Rotate (Vector3.up* rotationSpeed*Time.deltaTime);

	}
}
