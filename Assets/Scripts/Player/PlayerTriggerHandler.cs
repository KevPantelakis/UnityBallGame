using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour {

    private BallChanger ballChanger;

	// Use this for initialization
	void Start () {
        ballChanger = GetComponent<BallChanger>();
	}
	

    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "Pickup":
                Destroy(other.gameObject);
                CollectibleManager.score++;
                break;
            case "NewBall":
                if (other.gameObject.name.Contains("Grass"))
                {
                    GameObject go = FindGameObjetWithName(other.gameObject, "GrassBallTrail");
                    GameObject newTrail = (go!=null) ? GameObject.Instantiate(go): null;

                    SimpleBall newBall = new SimpleBall
                    {
                        name = "Grass",
                        ballTexture = other.gameObject.GetComponent<MeshRenderer>().material,
                        physicMaterial = other.gameObject.GetComponent<Collider>().material,
                        mesh = other.gameObject.GetComponent<MeshFilter>().mesh,
                        trail = newTrail,
                        jumpPower = 1.5f,
                        mass = 1.0f                        
                    };

                    ballChanger.AddNewBall(newBall);

                }

                Destroy(other.gameObject);
                break;
            case "Trigger":

                HandleSimpleTriggers(other.name);

                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Pickup":
                break;
            case "Water":

                break;
            case "SouthPortal":
                print("Travel South");
                break;
        }
    }

    private void HandleSimpleTriggers(string triggerName)
    {
        switch (triggerName)
        {
            case "AlertGrass":
                if (ballChanger.GetActiveBallName() != "Grass")
                {
                    print("Equip GrassBall to jump higher");
                }
                break;
        }
    }

    private GameObject FindGameObjetWithName(GameObject parent, string name)
    {
        return parent.transform.Find(name).gameObject;
    }

}
