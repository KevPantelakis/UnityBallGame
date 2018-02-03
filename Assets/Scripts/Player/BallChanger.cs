using System.Collections.Generic;
using UnityEngine;

public struct SimpleBall
{
    public float mass;                      // Mass of the ball (will affect mouvement and jump).
    public string name;                     // Name of the ball.
    public float jumpPower;                 // Controls how high the ball will jump.

    public Mesh mesh;                       // Ball model.
    public GameObject trail;                // Trail GameObject (must have a Trail script component).
    public Material ballTexture;            // Apparence of the ball .
    public PhysicMaterial physicMaterial;   // Physic material of the ball (affects bonciness and friction).
}

public class BallChanger : MonoBehaviour
{

    private List<SimpleBall> m_Balls;       // List of available balls.
    private SimpleBall m_CurrentBall;       // Currently active ball.
    private Ball m_Ball;                    // Reference to the Ball script, used to modifiy jump power.
    private Collider m_Collider;            // Reference to the Collider component, used to modifiy the physic material of the object (affects bonciness and friction).
    private Rigidbody m_RigidBody;          // Reference to the RigidBody Component of the object executing this script (used to change the mass of the object). 
    private MeshFilter m_MeshFilter;        // Reference to the MeshFilter component, used to modify the model of the object.
    private MeshRenderer m_MeshRenderer;    // Reference to the mesh renderer component, used to modifiy the apperence(skin) of the ball.
    private BallUserControl m_BallUserCtrl; // Reference to the BallUserControl script, used to change trails.
    private int currentIndex = 0;           // Determines the index of the currently active ball in the list


    // Unity Start, Awake and Update functions

    // Use this for initialization
    private void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_MeshFilter = GetComponent<MeshFilter>();
        m_BallUserCtrl = GetComponent<BallUserControl>();
        m_Ball = GetComponent<Ball>();
        m_RigidBody = GetComponent<Rigidbody>();


        m_Balls = new List<SimpleBall> {
            new SimpleBall {
                name = "Basic",
                physicMaterial = m_Collider.material,
                ballTexture = m_MeshRenderer.material,
                mesh = m_MeshFilter.mesh,
                trail = null,
                jumpPower = 1,
                mass = 1.5f
            }
        };

        m_CurrentBall = m_Balls[0];

    }

    // Called every frame
    private void Update()
    {
        bool keyPressed = Input.GetButtonUp("z");

        if (keyPressed)
        {
            NextBall();
        }
    }


    // Public Functions

    public void AddNewBall(SimpleBall newBall)
    {
        m_Balls.Add(newBall);
    }

    public void ChangeBallTo(SimpleBall newBall)
    {
        // De-Activate old trail if there is one
        ActivateTrail(false);

        // Change the current ball and the index values to the new ball;
        m_CurrentBall = newBall;
        int newIndex = m_Balls.IndexOf(newBall);
        if (newIndex == -1)
        {
            AddNewBall(newBall);
            currentIndex = m_Balls.Count - 1;
        }
        else
        {
            currentIndex = newIndex;
        }

        //Set ball modifiers
        SetModifiers();

        //Activate new trail if there is one
        ActivateTrail(true);
    }

    public string GetActiveBallName()
    {
        return m_CurrentBall.name;
    }


    // Private Functions

    private void NextBall() {

        if (m_Balls.Count > 1)
        {
            // De-Activate old trail if there is one
            ActivateTrail(false);

            // Change index and ball to the next in the list 
            currentIndex = (currentIndex + 1) % m_Balls.Count;
            m_CurrentBall = m_Balls[currentIndex];

            //Set ball modifiers
            SetModifiers();

            //Activate new trail if there is one
            ActivateTrail(true);
        }
    }

    private void ActivateTrail(bool activate)
    {
        if (m_CurrentBall.trail != null)
        {
            if (activate)
            {
                m_BallUserCtrl.trail = m_CurrentBall.trail.GetComponent<Trail>();
            }
            m_CurrentBall.trail.SetActive(activate);
        }
    }

    private void SetModifiers() {
        m_Collider.material = m_CurrentBall.physicMaterial;
        m_MeshRenderer.material = m_CurrentBall.ballTexture;
        m_MeshFilter.mesh = m_CurrentBall.mesh;
        m_Ball.m_JumpPower = m_CurrentBall.jumpPower;
        m_RigidBody.mass = m_CurrentBall.mass;
    }

}
