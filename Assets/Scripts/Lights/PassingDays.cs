using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingDays : MonoBehaviour {

    [SerializeField] private float rotationSpeed = 0.06f;
    
    private Light sun;
    private Quaternion defaultRotation;
    private Quaternion dayRotation = Quaternion.Inverse(Quaternion.Euler(new Vector3(360, -90, 0)));
    private Quaternion nightRotation = Quaternion.Euler(new Vector3(0, -90, 0));

	// Use this for initialization
	void Start () {
        if (!gameObject.CompareTag("Sun"))
        {
            sun = GetComponent<Light>();
            defaultRotation = sun.transform.rotation;
        }
        else
        {
            defaultRotation = gameObject.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update() {
        
        if (sun)
        {
            ///Rotate sun...
        }
        else
        {
           // gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, dayRotation, Time.deltaTime * rotationSpeed);

            gameObject.transform.Rotate(new Vector3((float)0.06, 0, 0));

        }
	}
}
