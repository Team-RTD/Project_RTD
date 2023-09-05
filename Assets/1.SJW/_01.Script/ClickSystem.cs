using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSystem : MonoBehaviour
{

    public static ClickSystem instance;

    public Camera cam;
    public GameObject test;
    private RaycastHit[] hit;

    public GameObject SummonEffect;
    public AudioClip SummonSound;

    public enum PlayerMode
    {
        Nomal,
        TowerBuild,
        TowerMix,
        TowerSell
    }

    public PlayerMode playerMode = PlayerMode.Nomal;

    public GameObject[] towerZone;



    private void Awake()
    {
        instance = this;
    }
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
                switch (playerMode)
                    { case PlayerMode.TowerBuild ://타워건설모드에서 클릭 시

                         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                         hit = Physics.RaycastAll(ray, 100f);
                         if (hit != null)
                         {
                                foreach (RaycastHit hitob in hit)
                                {
                                     GameObject hitObject = hitob.transform.gameObject;
                                     if (hitObject.tag == "TowerZone")
                                     {
                                        TowerZone t_zone = hitObject.GetComponent<TowerZone>();
                                        if(!t_zone.towerOn)
                                        { 
                                         Material mat = hitObject.GetComponent<Renderer>().material;
                                        t_zone.towerOn = true;
                                        mat.SetColor("_EmisColor", t_zone.summonZoneColor[1]);
                                        GameObject tower1 = Instantiate(test, hitObject.transform.position, Quaternion.Euler(Vector3.zero));
                                        GameObject summoneffect = Instantiate(SummonEffect, hitObject.transform.position, Quaternion.Euler(Vector3.zero));
                                        Destroy(summoneffect, 3); // 이펙트는 3초뒤 삭제
                                        Sound_Manager.instance.EffectPlay(SummonSound);
                                        //tower1.AddComponent<Outline>();
                                        hitObject.SetActive(false);
                                        //hit.collider.gameObject.GetComponent<Renderer>().material.GetColor("_EmisColor");
                                        //print(zone.GetComponent<Renderer>().material);
                                        //print(mat.enabledKeywords.ToString());
                                        //print(mat.shader.GetPropertyName(0));
                                        //print(mat.shader.GetPropertyName(1));
                                        }
                                     }
                                     else
                                     {
                                         print("렌더러 없음");
                                     }

                                }
                          }
                        break;

                      case PlayerMode.TowerSell :


                        break;
                     }

               
            }
        }





        if(playerMode == PlayerMode.TowerSell)
        {


        }

    }


   

    public void TowerBuildBtn()
    {
        if(playerMode!=PlayerMode.TowerBuild)
        {
            playerMode = PlayerMode.TowerBuild;

            foreach(GameObject zone in towerZone)
            {
                if(!zone.GetComponent<TowerZone>().towerOn)
                { 
                    zone.SetActive(true); 
                }
                else
                {
                    zone.SetActive(false);
                }
                
            }
        }
        else
        {
            playerMode = PlayerMode.Nomal;
            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
                {
                    zone.SetActive(false); 
                }
                else
                {
                    zone.SetActive(true);
                }
            }

        }
        
    }


    public void TowerSellBtn() 
    {
        if (playerMode != PlayerMode.TowerSell)
        {
            playerMode = PlayerMode.TowerSell;

            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
                {
                    zone.SetActive(false);
                }
                else
                {
                    zone.SetActive(true);
                }
            }
        }
        else
        {
            playerMode = PlayerMode.Nomal;
            foreach (GameObject zone in towerZone)
            {
                if (!zone.GetComponent<TowerZone>().towerOn)
                {
                    zone.SetActive(false);
                }
                else
                {
                    zone.SetActive(true);
                }
            }

        }


    }
}
