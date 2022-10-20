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
    public GameObject spawnPoint = null;
    private Dictionary<Item.VEGETABLE_TYPE, int> ItemInventory = new Dictionary<Item.VEGETABLE_TYPE, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        rbPlayer = GetComponent<Rigidbody>();

        foreach(Item.VEGETABLE_TYPE item in System.Enum.GetValues(typeof(Item.VEGETABLE_TYPE)))
        {
            ItemInventory.Add(item, 0);
        }
    }

    private void AddToInventory(Item item)
    {
        ItemInventory[item.typeOfVeggie]++;
    }

    private void PrintInventory()
    {
        string output = "";

        foreach (KeyValuePair<Item.VEGETABLE_TYPE, int> kvp in ItemInventory)
        {
            output += string.Format("{0}: {1}", kvp.Key, kvp.Value);
        }
        Debug.Log(output);
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
        rbPlayer.MovePosition(spawnPoint.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (other.CompareTag("Item"))
        {
            Item item = other.gameObject.GetComponent<Item>();
            AddToInventory(item);
            PrintInventory();
        }
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
