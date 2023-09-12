using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerMixCode: MonoBehaviour
{
    public static TowerMixCode instance { get; private set; }

    public Camera cam;
    public string componentName;
    public GameObject towerzone;
    public GameObject clickedTower;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        Tower_Manager tower_Manager = Tower_Manager.instance;
        cam = Camera.main;
    }



    // Update is called once per frame
    void Mix(RaycastHit hit)
    {
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
                        // ������Ʈ �ȿ� int towerRank ���� �ִ��� Ȯ��
                        if (yourComponent.GetType().GetField("towerRank") != null)
                        {
                            // int towerRank ���� �ִ� ���, ���ϴ� �۾� ����
                            int towerRank = (int)yourComponent.GetType().GetField("towerRank").GetValue(yourComponent);

                            GameObject towerZone = (GameObject)yourComponent.GetType().GetField("TowerZone").GetValue(yourComponent);



                            string listName = "Tower" + (towerRank + 1);
                            Debug.Log(listName);

                            List<GameObject> towerList = (List<GameObject>)typeof(Tower_Manager).GetField(listName).GetValue(Tower_Manager.instance);
                            GameObject towerObject = towerList.Find(GameObject => GameObject != clickedTower && GameObject.name == clickedTower.name);
                            Debug.Log(this + "�̰�");
                            if (towerRank < 6 && towerRank >= 0)
                            {
                                Debug.Log("����");
                                //GameObject towerzone = (GameObject)targetObject.GetType().GetField("TowerZone").GetValue(targetObject);
                                //���� �Ŵ����� ���� ����Ʈ���� ���� �̸��� ���� �ٸ� Ÿ���� ã�ƾ��Ѵ�.

                                if (towerObject != null)
                                {
                                    Tower_Manager.instance.TowerSell(towerObject, false);
                                    Tower_Manager.instance.TowerSell(clickedTower, false);

                                    Tower_Manager.instance.TowerInstance(towerZone, towerRank + 2);
                                }
                                else
                                {
                                    print("�ռ��� ������ Ÿ���� �����ϴ�.");
                                }
                            }
                            break; // ���ϴ� ������Ʈ�� ã������ ���� ����
                        }
                    }
                }
            }
            else
            {
                print("�ռ� �� �� ���� Ÿ�� �Դϴ�.");
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