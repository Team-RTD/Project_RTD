using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Manager : MonoBehaviour
{
    public static Tower_Manager instance { get; private set; }
    public List<GameObject> Tower1;
    public List<GameObject> Tower2;
    public List<GameObject> Tower3;
    public List<GameObject> Tower4;
    public List<GameObject> Tower5;
    public List<GameObject> Tower6;

    public GameObject[] Towers;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
