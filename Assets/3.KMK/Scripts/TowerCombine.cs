using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ʵ忡 �̸� ������ ������Ʈ�� �����ؾ߸� ���ο� Ÿ���� �ռ��� �� �ִ�.
//�ʵ忡 Ÿ���� ������ ������ ����� ã�� ���ؼ� ������ Ÿ���� �������.
//�� ��� �Ⱥ��̴°��� Ÿ���� �־� �α⸸ �ϸ� �� �۵��� ���̴�.
public class TowerCombine : MonoBehaviour
{
    public Camera cam;
    public RaycastHit hit;

    public GameObject[] clickedObjects; // Ŭ���� ������Ʈ���� ������ �迭
    private int currentIndex = 0; // ������ ������Ʈ �ʱ�ȭ�� ����ϴ� �μ�.

    public GameObject towerPrefab; // ������ Ÿ���� ������

    public string towerTag2 = "Tower 2";

    public string towerTag3 = "Tower 3";

    public void Start()
    {
        //������ �� �迭 ����
        clickedObjects = new GameObject[2];
    }

    private void Update()
    {
        // ���콺 Ŭ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ���� ��ġ���� ���̸� ��� ������Ʈ ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //2��ũ�� Ÿ�� �ռ� ������
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.gameObject.tag == "Tower")
                {
                    // �迭�� ������Ʈ ����
                    clickedObjects[currentIndex] = clickedObject;

                    currentIndex++; // ���� �ε����� �̵�
                                    //���� Ÿ���� �����ߴ��� Ȯ��
                    if (clickedObjects[0] != clickedObjects[1])
                    {
                        if (currentIndex >= clickedObjects.Length)
                        {
                            currentIndex = 0; // �ε����� �迭 ũ�⸦ �Ѿ�� ó������ �ٽ� ����
                        }
                        if (clickedObjects[1] != null)
                        {
                            // Ÿ���� �ռ��ؼ� ���� ����� Ÿ���� �����ؾ� �Ѵ�.
                            if (clickedObjects[0].tag == clickedObjects[1].tag)
                            {
                                // ���ο� Ÿ�� ����
                                SpawnNewTowerRank2();
                                print("��ũ2�� Ÿ���� �����Ǿ����ϴ�");

                                Destroy(clickedObjects[0].gameObject);
                                Destroy(clickedObjects[1].gameObject);

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;


                            }
                            else
                            {
                                print("�ռ� �� �� ���� ��� �Դϴ�.");

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;
                            }
                        }
                    }
                    else
                    {
                        print("�ٸ� Ÿ���� ������ �ֽʽÿ�.");
                        clickedObjects[1] = null;
                    }
                }

                //��ũ3 Ÿ�� �ռ� ������
                if (clickedObject.gameObject.tag == "Tower 2")
                {
                    // �迭�� ������Ʈ ����
                    clickedObjects[currentIndex] = clickedObject;

                    currentIndex++; // ���� �ε����� �̵�

                    if (currentIndex >= clickedObjects.Length)
                    {
                        currentIndex = 0; // �ε����� �迭 ũ�⸦ �Ѿ�� ó������ �ٽ� ����
                    }
                    if (clickedObjects[1] != null)
                    {
                        //���� Ÿ���� �����ߴ��� Ȯ��
                        if (clickedObjects[0] != clickedObjects[1])
                        {
                            // Ÿ���� �ռ��ؼ� ���� ����� Ÿ���� �����ؾ� �Ѵ�.
                            if (clickedObjects[0].tag == clickedObjects[1].tag)
                            {
                                // ���ο� Ÿ�� ����
                                SpawnNewTowerRank3();
                                print("��ũ3�� Ÿ���� �����Ǿ����ϴ�");

                                Destroy(clickedObjects[0].gameObject);
                                Destroy(clickedObjects[1].gameObject);

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;


                            }
                            else
                            {
                                print("�ռ� �� �� ���� ��� �Դϴ�.");

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;
                            }
                        }
                        else
                        {
                            print("�ٸ� Ÿ���� ������ �ֽʽÿ�.");
                            clickedObjects[1] = null;
                        }
                    }
                }
                if (clickedObject.gameObject.tag == "Tower 3")
                {
                    print("�ش� Ÿ���� �� �̻� �ռ� �� �� �����ϴ�.");
                    clickedObjects[1] = null;
                    clickedObjects[0] = null;
                }
            }
        }
    }

    public void SpawnNewTowerRank2()
    {
        GameObject newTowerObject = FindObjectWithTag("Tower 2");
        if (newTowerObject != null)
        {
            Vector3 spawnPosition = clickedObjects[1].transform.position;
            Quaternion spawnRotation = clickedObjects[1].transform.rotation;

            Instantiate(newTowerObject, spawnPosition, spawnRotation);
        }
    }
    public void SpawnNewTowerRank3()
    {
        GameObject newTowerObject = FindObjectWithTag("Tower 3");
        if (newTowerObject != null)
        {
            Vector3 spawnPosition = clickedObjects[1].transform.position;
            Quaternion spawnRotation = clickedObjects[1].transform.rotation;

            Instantiate(newTowerObject, spawnPosition, spawnRotation);
        }
    }

    // Ư�� �±׸� ������ ������Ʈ ã��
    GameObject FindObjectWithTag(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        if (taggedObjects.Length > 0)
        {
            int randomIndex = Random.Range(0, taggedObjects.Length);
            return taggedObjects[randomIndex];
        }
        return null;
    }
}
