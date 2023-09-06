using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�ʵ忡 �̸� ������ ������Ʈ�� �����ؾ߸� ���ο� Ÿ���� �ռ��� �� �ִ�.
//�ʵ忡 Ÿ���� ������ ������ ����� ã�� ���ؼ� ������ Ÿ���� �������.
//�� ��� �Ⱥ��̴°��� Ÿ���� �־� �α⸸ �ϸ� �� �۵��� ���̴�.
public class TowerCombine : MonoBehaviour
{
    //������Ʈ�� ������ ī�޶� �־�� ��.
    public Camera cam;
    public RaycastHit hit;

    public GameObject[] clickedObjects; // Ŭ���� ������Ʈ���� ������ �迭
    private int currentIndex = 0; // ������ ������Ʈ �ʱ�ȭ�� ����ϴ� �μ�.

    public GameObject towerPrefab; // ������ Ÿ���� ������

    public string towerTag2 = "Tower_2";

    public string towerTag3 = "Tower_3";

    public bool combineProsses = false;

    public void Start()
    {
        //������ �� �迭 ����
        clickedObjects = new GameObject[2];
    }

    private void Update()
    {
        if (combineProsses == false)
        {
            if (Input.GetKey(KeyCode.Alpha2))
            {
                Combine();
            }
        }
    }

    private void Combine()
    {        
        print("Ȯ��");
        // ���콺 Ŭ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            combineProsses = true;
            // ���콺 Ŭ���� ��ġ���� ���̸� ��� ������Ʈ ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //2��ũ�� Ÿ�� �ռ� ������
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.gameObject.tag == "Tower_1")
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
                                combineProsses = false;
                            }
                            else
                            {
                                //Ÿ�� �ռ��� ���� ���� ��� ���.
                                print("�ռ� �� �� ���� ��� �Դϴ�.");

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;
                                combineProsses = false;
                            }
                        }
                    }
                    else
                    {
                        //���� Ÿ�� 2�� ���ý� ���.
                        print("�ٸ� Ÿ���� ������ �ֽʽÿ�.");
                        clickedObjects[1] = null;
                        combineProsses = false;
                    }
                }

                //��ũ3 Ÿ�� �ռ� ������
                else if (clickedObject.gameObject.tag == "Tower_2")
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

                                combineProsses = false;
                            }
                            else
                            {
                                //Ÿ�� �ռ��� ���� ���� ��� ���.
                                print("�ռ� �� �� ���� ��� �Դϴ�.");

                                clickedObjects[1] = null;
                                clickedObjects[0] = null;

                                combineProsses = false;
                            }
                        }
                        else
                        {
                            //���� Ÿ�� 2�� ���ý� ���.
                            print("�ٸ� Ÿ���� ������ �ֽʽÿ�.");
                            clickedObjects[1] = null;
                        }
                    }
                }
                else if (clickedObject.gameObject.tag == "Tower_3")
                {
                    //3Ƽ�� Ÿ�� ���ý� ���.
                    print("�ش� Ÿ���� �� �̻� �ռ� �� �� �����ϴ�.");
                    clickedObjects[1] = null;
                    clickedObjects[0] = null;
                }
                else
                {
                    print("Ÿ���� ������ �ֽʽÿ�");
                }
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                combineProsses = false;
                print("��ȯ�մϴ�.");
                return;
            }
        }

        print("���Դϴ�");
        combineProsses = false;
    }

    //1Ƽ�� Ÿ���� 2Ƽ�� Ÿ���� �ռ��� ��� ����.
    public void SpawnNewTowerRank2()
    {
        GameObject newTowerObject = FindObjectWithTag("Tower_2");
        if (newTowerObject != null)
        {
            Vector3 spawnPosition = clickedObjects[1].transform.position;
            Quaternion spawnRotation = clickedObjects[1].transform.rotation;

            Instantiate(newTowerObject, spawnPosition, spawnRotation);
        }
    }
    //2Ƽ�� Ÿ���� 3Ƽ�� Ÿ���� �ռ��� ��� ����.
    public void SpawnNewTowerRank3()
    {
        GameObject newTowerObject = FindObjectWithTag("Tower_3");
        if (newTowerObject != null)
        {
            Vector3 spawnPosition = clickedObjects[1].transform.position;
            Quaternion spawnRotation = clickedObjects[1].transform.rotation;

            Instantiate(newTowerObject, spawnPosition, spawnRotation);
        }
    }
    //�߰��� Ƽ� �߰��� �� ���� Ƽ���� Ÿ���� �ռ��� �ʿ��� ��� �����ؼ� �Լ� �̸��� �ٲ㼭 �ۼ�.



    //Do not change--------------
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
    //--------------------------
}
