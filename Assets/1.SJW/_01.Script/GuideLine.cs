using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour
{
    public GameObject[] Pos;
    int posloc = 0;
    public Transform startpos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoToPos(Pos[posloc]));
        startpos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator GoToPos(GameObject setpos)
    {
        Vector3 dir =setpos.transform.position - transform.position;
        Vector3 loc = dir.normalized;
        float speed = 2.0f;
        while (dir.magnitude > 1f)
        {
            if (Data_Manager.instance.isPause)
            {
                speed =0;
            }
            else
            {
                speed = 2.0f;
            }

            transform.LookAt(setpos.transform);
            //transform.position = Vector3.Lerp(transform.position,setpos.transform.position,0.3f);
            transform.position = Vector3.MoveTowards(transform.position, setpos.transform.position, speed);
            //transform.Translate(loc,Space.World);
            dir = setpos.transform.position - transform.position;



            yield return null;
        }
        posloc++;
        if(posloc <= 11)
        { StartCoroutine(GoToPos(Pos[posloc])); }
        else
        {
            yield return new WaitForSeconds(3f);
            posloc = 0;
            transform.position = startpos.position;
            StartCoroutine(GoToPos(Pos[posloc]));
        }
       
    }

}
