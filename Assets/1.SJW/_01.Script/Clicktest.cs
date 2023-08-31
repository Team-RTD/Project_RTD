using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicktest : MonoBehaviour
{
    public Camera cam;
    public GameObject test;
    private RaycastHit hit;
    
    public enum PlayerMode
    {
        Nomal,
        TowerBuild,
        TowerMix,
        TowerSell
    }

    public PlayerMode playerMode = PlayerMode.Nomal;

    public GameObject[] towerZone;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        towerZone = null;
        towerZone = GameObject.FindGameObjectsWithTag("TowerZone");
        foreach (GameObject zone in towerZone)
        {
            zone.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = cam.ScreenPointToRay( Input.mousePosition );
                 if(Physics.Raycast( ray, out hit ) ) 
                 {
                     string obname = hit.collider.gameObject.name;
                    print(obname);
               
                    if (hit.collider.gameObject.tag == "TowerZone")
                    {
                        Material mat = hit.collider.gameObject.GetComponent<Renderer>().material;
                        hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_EmisColor", Color.yellow);
                        Instantiate(test, hit.collider.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
                        //hit.collider.gameObject.GetComponent<Renderer>().material.GetColor("_EmisColor");
                        print(hit.collider.gameObject.GetComponent<Renderer>().material);
                        print(mat.enabledKeywords.ToString());
                        print(mat.shader.GetPropertyName(0));
                        print(mat.shader.GetPropertyName(1));
                    }
                    else
                    {
                        print("·»´õ·¯ ¾øÀ½");

                    }
                 }
            }

        }
    }


    public void TowerBuildBtn()
    {
        if(playerMode!=PlayerMode.TowerBuild)
        {
            playerMode = PlayerMode.TowerBuild;

            foreach(GameObject zone in towerZone)
            {
                zone.SetActive(true);
            }
        }
        else
        {
            playerMode = PlayerMode.Nomal;
            foreach (GameObject zone in towerZone)
            {
                zone.SetActive(false);
            }

        }
        
    }
}
