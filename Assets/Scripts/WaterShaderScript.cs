using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShaderScript : MonoBehaviour
{
    //public GameObject riverWater;
    private Renderer rend;
    public Color setColor = Color.yellow;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.SetColor("_ShimmerColor", Color.yellow);
    }
}
