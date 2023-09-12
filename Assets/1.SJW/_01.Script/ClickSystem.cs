using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSystem : MonoBehaviour
{

    public static ClickSystem instance { get; private set; }

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
                    { 
                    case PlayerMode.TowerBuild ://타워건설모드에서 클릭 시

                         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                         hit = Physics.RaycastAll(ray, 100f);
                         if (hit != null)
                         {
                                foreach (RaycastHit hitob in hit)
                                {
                                     GameObject hitObject = hitob.transform.gameObject;
                                     if (hitObject.tag == "TowerZone")
                                     {
                                     Tower_Manager.instance.TowerInstance(hitObject, 1 );
                                     }
                                     else
                                     {
                                         print("렌더러 없음");
                                     }

                                }
                         }
                         break;

                    case PlayerMode.TowerMix:

                        RaycastHit hit1;
                        Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);


                        if (Physics.Raycast(ray1, out hit1))
                        {
                            GameObject clickedTower = hit1.collider.gameObject;
                            if (clickedTower != null)
                            {
                                Component[] components = clickedTower.GetComponents<Component>();
                                foreach (Component component in components)
                                {
                                    if (component is Twr_0Base yourComponent)
                                    {
                                        // 컴포넌트 안에 int towerRank 값이 있는지 확인
                                        if (yourComponent.GetType().GetField("towerRank") != null)
                                        {
                                            // int towerRank 값이 있는 경우, 원하는 작업 수행
                                            int towerRank = (int)yourComponent.GetType().GetField("towerRank").GetValue(yourComponent);

                                            GameObject towerZone = (GameObject)yourComponent.GetType().GetField("TowerZone").GetValue(yourComponent);



                                            string listName = "Tower" + (towerRank + 1);

                                            List<GameObject> towerList = (List<GameObject>)typeof(Tower_Manager).GetField(listName).GetValue(Tower_Manager.instance);
                                            GameObject towerObject = towerList.Find(GameObject => GameObject != clickedTower && GameObject.name == clickedTower.name);
                                            if (towerRank < 6 && towerRank >= 0)
                                            {
                                                Debug.Log("지점");
                                                //GameObject towerzone = (GameObject)targetObject.GetType().GetField("TowerZone").GetValue(targetObject);
                                                //이제 매니저가 가진 리스트에서 같은 이름을 가진 다른 타워를 찾아야한다.

                                                towerRank += 2;
                                                Debug.Log(towerObject.name);
                                                Debug.Log(clickedTower.name);
                                                if (towerObject != null)
                                                {
                                                    Debug.Log("여기");
                                                    Tower_Manager.instance.TowerSell(towerObject, false);
                                                    Tower_Manager.instance.TowerSell(clickedTower, false);
                                                    Debug.Log(towerZone);
                                                    Debug.Log(towerRank);
                                                    Tower_Manager.instance.TowerInstance(towerZone, towerRank);
                                                    towerZone.SetActive(true);
                                                    Debug.Log("출력확인");
                                                }
                                                else
                                                {
                                                    print("합성이 가능한 타워가 없습니다.");
                                                }
                                            }
                                            break; // 원하는 컴포넌트를 찾았으면 루프 종료
                                        }
                                    }
                                }
                            }
                            else
                            {
                                print("합성 할 수 없는 타워 입니다.");
                            }
                        }
                        break;



                    case PlayerMode.TowerSell :


                        break;

                    case PlayerMode.Nomal:

                        Ray rays = cam.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitinfo = new RaycastHit();
                        if (Physics.Raycast(rays,out hitinfo))
                        {
                            if (!(hitinfo.transform.tag == "TowerZone" || hitinfo.transform.tag == "Tower"))
                            {
                                print(hitinfo.transform.gameObject.name);
                                Ui_Manager.instance.InfoPannelActive = false;
                                Ui_Manager.instance.towerInfoPannel.SetActive(false);
                            }
                        }
                       
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
            Ui_Manager.instance.state.text = "건설 모드";
            BtnColorReset();
            BuildBtnDark();

            foreach (GameObject zone in towerZone)
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
            BtnColorReset();
            Ui_Manager.instance.state.text = "";
            Image[] imgs = Input_Manager.instance.towerBuildBtn.GetComponentsInChildren<Image>();

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
            Ui_Manager.instance.state.text = "판매 모드";
            BtnColorReset();
            SellBtnDark();

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
            Ui_Manager.instance.state.text = "";
            BtnColorReset();
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
    public void TowerMixBtn()
    {
        if (playerMode != PlayerMode.TowerMix)
        {
            playerMode = PlayerMode.TowerMix;
            Ui_Manager.instance.state.text = "합성 모드";
            BtnColorReset();
            MixBtnDark();

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
            Ui_Manager.instance.state.text = "";
            BtnColorReset();
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

    public void BtnColorReset()
    {
        Image[] imgs = Input_Manager.instance.towerBuildBtn.GetComponentsInChildren<Image>();
        for(int i = 0; i < imgs.Length; i++) 
        {
            imgs[i].color = Input_Manager.instance.SaveColor[i];
        }
          
        

        imgs = Input_Manager.instance.towerSellBtn.GetComponentsInChildren<Image>();
        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].color = Input_Manager.instance.SaveColor[i];
        }

        imgs = Input_Manager.instance.towerMixBtn.GetComponentsInChildren<Image>();
        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].color = Input_Manager.instance.SaveColor[i];
        }

    }

    public void BuildBtnDark()
    {
        Image[] imgs = Input_Manager.instance.towerBuildBtn.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
        {
            img.color -= new Color(0.5f, 0.5f, 0.5f, 0f);
        }
    }
    public void SellBtnDark()
    {
        Image[] imgs = Input_Manager.instance.towerSellBtn.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
        {
            img.color -= new Color(0.5f, 0.5f, 0.5f, 0f);
        }
    }
    public void MixBtnDark()
    {
        Image[] imgs = Input_Manager.instance.towerMixBtn.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
        {
            img.color -= new Color(0.5f, 0.5f, 0.5f, 0f);
        }
    }


}
