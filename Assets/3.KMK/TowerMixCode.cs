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
                
                //---------------��ĥ �κ�
                Type intRank = targetComponent.GetType();
                FieldInfo rank= intRank.GetField("towerRank");

                int towerRank= (int)rank.GetValue(targetComponent);
                Debug.Log(towerRank);
                //---------------
                if (targetObject != null)
                {
                    clickedTower = hit.collider.gameObject;
                    Debug.Log(clickedTower);
                    Debug.Log(towerRank+"�����.");

                    if (towerRank < 6 && towerRank >= 1)
                    {
                        string componentName = targetComponent.name;
                        Debug.Log(targetComponent.name);

                        GameObject towerzone = (GameObject)targetComponent.GetType().GetField("TowerZone").GetValue(targetComponent);
                        //���� �Ŵ����� ���� ����Ʈ���� ���� �̸��� ���� �ٸ� Ÿ���� ã�ƾ��Ѵ�.

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
                            print("�ռ��� ������ Ÿ���� �����ϴ�.");
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
                                print("�ռ��� ������ Ÿ���� �����ϴ�.");
                            }
                        }*/
                    }
                    else
                    {
                        print("�ռ� �� �� ���� Ÿ�� �Դϴ�.");
                    }
                }
            }
            //���� ������Ʈ�� ������Ʈ���� ��ũ�� ���� ������ ã�´�
            //�Ŵ����� ������ �ִ� ����Ʈ���� ���� ��ũ�� ����Ʈ�� ã��, ������ ������ ���� �̸��� ������ ������Ʈ�� ã�´�.
            //�R��°�� ������ ������Ʈ�� Ÿ���� ������ ����Ѵ�.
            //Find�Լ��� ���� ã�� ������Ʈ�� ó�� �߰��� ������Ʈ�� �����Ѵ�.
            //�Ǹ��Լ��� ������Ʈ�� �����Ѵ�.
            //������Ʈ �����Լ��� ���� Ÿ���� ������ �Է��ؼ� ���� ��ũ�� Ÿ���� �����Ѵ�.
        }
    }    
}