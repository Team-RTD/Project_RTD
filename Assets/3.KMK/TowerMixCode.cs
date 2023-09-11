using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TowerMixCode : MonoBehaviour
{
    public Camera cam;
    public int towerRank;
    public string componentName;
    public GameObject towerzone;
    public GameObject clickedTower;

    public void Start()
    {
        Tower_Manager tower_Manager = Tower_Manager.instance;
        cam = Camera.main;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                GameObject targetObject = hit.collider.gameObject;
                Component targetComponent = targetObject.GetComponent<Component>();
                
                //---------------고칠 부분
                Type intRank = targetComponent.GetType();
                FieldInfo rank= intRank.GetField("towerRank");

                int towerRank= (int)rank.GetValue(targetComponent);
                Debug.Log(towerRank);
                //---------------
                if (targetObject != null)
                {
                    clickedTower = hit.collider.gameObject;
                    Debug.Log(clickedTower);
                    Debug.Log(towerRank+"여기다.");

                    if (towerRank < 6 && towerRank >= 1)
                    {
                        string componentName = targetComponent.name;
                        Debug.Log(targetComponent.name);

                        GameObject towerzone = (GameObject)targetComponent.GetType().GetField("TowerZone").GetValue(targetComponent);
                        //이제 매니저가 가진 리스트에서 같은 이름을 가진 다른 타워를 찾아야한다.

                        string listName = "Tower" + towerRank;
                        List<GameObject> towerList = (List<GameObject>)typeof(Tower_Manager).GetField(listName).GetValue(Tower_Manager.instance);

                        GameObject towerObject = towerList.Find(GameObject => GameObject != clickedTower && targetComponent.name == GameObject.name);
                        if (towerObject != null)
                        {
                            Tower_Manager.instance.TowerSell(towerObject, false);
                            Tower_Manager.instance.TowerSell(clickedTower, false);

                            Tower_Manager.instance.TowerInstance(towerzone, towerRank + 1);
                        }
                        else
                        {
                            print("합성이 가능한 타워가 없습니다.");
                        }

                        /*foreach (GameObject towerObject in towerList)
                        {
                            if (towerObject != clickedTower && targetComponent.name == towerObject.name)
                            {
                                Tower_Manager.instance.TowerSell(towerObject, false);
                                Tower_Manager.instance.TowerSell(clickedTower, false);

                                Tower_Manager.instance.TowerInstance(towerzone, towerRank + 1);
                            }
                            else
                            {
                                print("합성이 가능한 타워가 없습니다.");
                            }
                        }*/
                    }
                    else
                    {
                        print("합성 할 수 없는 타워 입니다.");
                    }
                }
            }
            //누른 오브젝트의 컴포넌트에서 랭크에 대한 정보를 찾는다
            //매니저가 가지고 있는 리스트에서 같은 랭크의 리스트를 찾고, 본인을 제외한 같은 이름을 가지는 오브젝트를 찾는다.
            //첮번째로 선택한 오브젝트의 타워존 정보를 기억한다.
            //Find함수를 쓰면 찾은 오브젝트중 처음 발견한 오브젝트를 저장한다.
            //판매함수로 오브젝트를 제거한다.
            //오브젝트 생성함수로 받은 타워존 정보를 입력해서 다음 랭크의 타워를 생성한다.
        }
    }    
}