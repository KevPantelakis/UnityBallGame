using System;
using UnityEngine;


public class Ball : MonoBehaviour
{
    
    public float m_MaxVelocity = 25; // The maximum Velocity the ball can move at.
    public float m_JumpPower = 2; // The force added to the ball when it jumps. 

    [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
    [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    private Rigidbody m_Rigidbody; // The RigidBody component of the GameObject excuting this script.
    private RaycastHit hitInfo; // The info of the collider when there is a collision with another object.
    private bool isGrounded; // Tells if the ball is grounded.
    private Vector3 velocity; // Current velocity of the ball.

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        // Set the maximum angular velocity.
        m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocity;

    }

    public void Move(Vector3 moveDirection, bool jump)
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hitInfo, k_GroundRayLength);
        velocity = m_Rigidbody.velocity;
        //Vector3 tengentialSpeed = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
        //print(tengentialSpeed.magnitude);

        // Add torque until velocity is 12.5(max velocity with torque) or if the ball is in the air 
        if (!isGrounded ||  new Vector3(velocity.x, 0, velocity.z).magnitude < 12.5)
        {
            m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower);
        }
        // Use addForce to go faster than 12.5 in the z-x plane if the ball is grounded
        else
        {

            if (velocity.magnitude > m_MaxVelocity)
            {
                m_Rigidbody.velocity = velocity.normalized * m_MaxVelocity;
            }

            m_Rigidbody.AddForce(moveDirection * m_MovePower);
        }

        // If on the ground and jump is pressed...
        if (isGrounded && jump)
        {
            if (!(hitInfo.collider.CompareTag("Water") || hitInfo.collider.CompareTag("Trigger")))
            {
                m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
            }
        }
    }
}


