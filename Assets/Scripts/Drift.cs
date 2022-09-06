using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    public float speed = 5;
    public enum DRIFT_DIRECTION
    {
        LEFT = -1,
        RIGHT = 1
    }
    public DRIFT_DIRECTION driftDirection = DRIFT_DIRECTION.LEFT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (driftDirection)
        {
            case DRIFT_DIRECTION.LEFT:
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
            case DRIFT_DIRECTION.RIGHT:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
        }
        
        

        if (transform.position.x < -100 || transform.position.x > 100)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject child = collision.gameObject;
            child.transform.SetParent(gameObject.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject child = collision.gameObject;
            child.transform.SetParent(null);
        }
    }
}
