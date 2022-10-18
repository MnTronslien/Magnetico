using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpecialObject", order = 1)]
public class SpecialObject : ScriptableObject
{


    public GameObject prefab;
    public AudioClip[] anouncment;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
