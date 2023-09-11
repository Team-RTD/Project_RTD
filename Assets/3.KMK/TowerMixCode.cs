using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMixCode : MonoBehaviour
{
    private Camera cam;
    private List<GameObject> foundObjects = new List<GameObject>();
    private MixTowerManager mixTowerManager;
    public int towerRank;

    public void Start()
    {
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
                GameObject towerObject = hit.collider.gameObject;

                //���̿� ���� ��� ������Ʈ�� �����Ѵ�
                Component[] clickedObject = towerObject.GetComponents<Component>();

                //����� ������Ʈ �� ��ũ�� ���� Ÿ������ ã�´�.
                foreach (Component component in clickedObject)
                {
                    if (component.GetType().GetField("towerRank") != null)
                    {
                        //������ ��ũ�� int ������ ��ȯ�ϴ� �ڵ�, ���߿� Ÿ���� ������ �� ������.
                        int towerRank = (int)component.GetType().GetField("towerRank").GetValue(component);

                        //Ÿ���� ����� �ִ� 3�ܰ��� ������ 3�ܰ��� Ÿ���� �ռ��� �Ұ����ϰ� ����.
                        if (towerRank != 3)
                        {
                            //�̹� Ŭ���� ������Ʈ�� ���� ���.
                            if (foundObjects != null)
                            {
                                if (foundObjects.Count < 2)
                                {
                                    //Ŭ���� �����Ͱ� �̹� ����Ǿ� �ִ� ������ �ƴ϶�� ����.
                                    if (!foundObjects.Contains(towerObject))
                                    {
                                        foundObjects.Add(towerObject);
                                    }
                                    else
                                    {
                                        print("�ٸ� Ÿ���� �����ؾ� �մϴ�");
                                        foundObjects.Clear();
                                    }
                                }
                                // �̹� ���õ� ������Ʈ�� ���� ���, �� ������Ʈ�� ���Ͽ� �������� Ȯ���մϴ�.
                                if (foundObjects[0] == towerObject)
                                {
                                    //Ÿ�� �ռ� �ǽ�
                                    TowerMix();
                                    foundObjects.Clear();
                                }
                                else
                                {
                                    print("���� ������ Ÿ���� �ռ��� �����մϴ�.");
                                    foundObjects.Clear();
                                }
                            }
                        }
                        else
                        {
                            print("�ش� Ÿ���� �ռ��� �Ұ��� �մϴ�.");
                            clickedObject = new Component[0];

                        }

                        break;
                    }
                }
            }
        }
    }

    public void TowerMix()
    {
        // Ÿ�� �ռ� ���� ����
        GameObject tower1 = foundObjects[0];
        GameObject tower2 = foundObjects[1];

        if (towerRank == 1)
        {
            List<GameObject> rank2tower = mixTowerManager.rank2Tower;

            int randomeIndex = Random.Range(0, rank2tower.Count);
            GameObject instantiateTower = rank2tower[randomeIndex];

            Vector3 spwan = foundObjects[1].transform.position;
            GameObject newTower = Instantiate(instantiateTower, spwan, Quaternion.identity);

            Destroy(foundObjects[0]);
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }
        else if (towerRank == 2)
        {
            List<GameObject> rank3tower = mixTowerManager.rank3Tower;

            int randomeIndex = Random.Range(0, rank3tower.Count);
            GameObject instantiateTower = rank3tower[randomeIndex];

            Vector3 spwan = foundObjects[1].transform.position;
            GameObject newTower = Instantiate(instantiateTower, spwan, Quaternion.identity);

            Destroy(foundObjects[0]);
            Destroy(foundObjects[1]);

            foundObjects.Clear();
        }

    }
}


