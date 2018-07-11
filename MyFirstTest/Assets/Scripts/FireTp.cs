using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTp : MonoBehaviour {

    public Rigidbody2D m_Tp;                   
    public Transform m_FireTransform;

    public float m_LaunchForce = 30f;

    private string m_FireButton;                // The input axis that is used for launching shells.
    private string m_resetButton;
    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.

    private Rigidbody2D tpInstance;
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
        m_resetButton = "Fire2";
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
           
            // ... launch the shell.
            Tp();
            
        }else if(Input.GetButtonDown(m_resetButton) && m_Fired)
        {
            m_Fired = false;
            Destroy(tpInstance.gameObject);
        }
    }

    private void Tp()
    {
        m_Fired = false;
        transform.SetPositionAndRotation(tpInstance.position, transform.rotation);
        rb2d.velocity = new Vector2(0, 0);
        Destroy(tpInstance.gameObject);
    }

    private void Fire ()
    {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;
      
        Vector2 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(posMouse.x - m_FireTransform.position.x, posMouse.y - m_FireTransform.position.y);

        m_FireTransform.right = direction;

        // Create an instance of the shell and store a reference to it's rigidbody.
        tpInstance = Instantiate (m_Tp, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody2D;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        tpInstance.velocity = m_LaunchForce * m_FireTransform.right;

    }
}