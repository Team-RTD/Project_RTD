using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.AddComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        print("마우스 감지!");
    }


    private void OnMouseExit()
    {
        print(" 마우스 나감!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
