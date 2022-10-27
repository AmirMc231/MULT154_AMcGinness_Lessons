using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody rbPlayer;
    private Vector3 direction = Vector3.zero;
    //private bool islocalPlayer;
    public float speed = 10.0f;
    public GameObject[] spawnPoint = null;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        rbPlayer = GetComponent<Rigidbody>();
        spawnPoint = GameObject.FindGameObjectsWithTag("Respawn");

        
    }


    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float horzMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        direction = new Vector3(horzMove, 0, vertMove);
    }

    private void OnDrawGizmos()
    {
       
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(5, 6, 5));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rbPlayer.AddForce(direction * speed, ForceMode.Force);
        if (transform.position.z > 40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y , 40);
        } else if (transform.position.z < -40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        }
    }

    private void Respawn()
    {
        int index = 0;
        while(Physics.CheckBox(spawnPoint[index].transform.position, new Vector3(1.5f, 1.5f, 1.5f)))
        {
            index++;
        }
        rbPlayer.MovePosition(spawnPoint[index].transform.position);
    }

    


    private void OnTriggerExit(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (other.CompareTag("Hazard"))
        {
            Respawn();
        }
    }


}
