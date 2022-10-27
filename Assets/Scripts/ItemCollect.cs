using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemCollect : NetworkBehaviour
{
    private Dictionary<Item.VEGETABLE_TYPE, int> ItemInventory = new Dictionary<Item.VEGETABLE_TYPE, int>();

    public delegate void CollectItem(Item.VEGETABLE_TYPE item);
    public static event CollectItem ItemCollected;

    Collider itemCollider = null;

    void Start()
    {
        foreach (Item.VEGETABLE_TYPE item in System.Enum.GetValues(typeof(Item.VEGETABLE_TYPE)))
        {
            ItemInventory.Add(item, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (itemCollider && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space bar and item collected");
            Item item = itemCollider.gameObject.GetComponent<Item>();
            AddToInventory(item);
            PrintInventory();

            CmdItemCollected(item.typeOfVeggie);
        }
       
    }

    [Command]
    void CmdItemCollected(Item.VEGETABLE_TYPE itemType)
    {
        Debug.Log("CommandItemCollect: " + itemType);
        RpcItemCollected(itemType);
    }

    [ClientRpc]
    void RpcItemCollected(Item.VEGETABLE_TYPE itemType)
    {
        ItemCollected?.Invoke(itemType);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (other.CompareTag("Item"))
        {
            itemCollider = other;

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (other.CompareTag("Item"))
        {
            itemCollider = null;
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
}
