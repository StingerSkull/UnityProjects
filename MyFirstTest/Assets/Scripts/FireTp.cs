using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTp : MonoBehaviour {

    public Rigidbody2D m_Tp;                   
    public Transform m_FireTransform;

    public float m_LaunchForce = 15f;        

    private string m_FireButton;                // The input axis that is used for launching shells.
    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.

    private Rigidbody2D shellInstance;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start ()
    {
        // The fire axis is based on the player number.
        m_FireButton = "Fire1";

    }


    private void Update ()
    {

        if (Input.GetButtonDown(m_FireButton) && !m_Fired)
        {
            // ... launch the shell.
            Fire();
        }
        else if (Input.GetButtonDown(m_FireButton) && m_Fired)
        {
            m_Fired = false;
            // ... launch the shell.
            Tp();
            
        }
    }

    private void Tp()
    {
        transform.SetPositionAndRotation(shellInstance.position, transform.rotation);
        rb2d.velocity = new Vector2(0, 0);
        shellInstance.gameObject.SetActive(false);
    }

    private void Fire ()
    {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        shellInstance =
            Instantiate (m_Tp, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody2D;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_LaunchForce * m_FireTransform.right;

    }
}