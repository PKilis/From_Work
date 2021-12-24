using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creaters : MonoBehaviour
{

    [SerializeField] private GameObject[] circlePrefab;
    private int randomCircleNumber;
    [SerializeField] private GameObject circleParent;
    [SerializeField] private List<GameObject> createdCircleList;


    [Range(0, 350)]
    [SerializeField] private int poolingCount;

    [Range(0, 350)]
    public int nextCount;

    [SerializeField] private int activeCount;


    [Header("CircleProperty")]

    [SerializeField] public float nextRadius;
    [SerializeField] public float startRadius;
    [SerializeField] private float radiusIncrease;
    [SerializeField] private float positionZIncrease;

    [Header("AngleProperty")]
    [SerializeField] private float angleStart;
    [SerializeField] private float totalAngle;
    private float angle;

    [Header("AngleIncreaseProperty")]
    [SerializeField] private float angleIncreaseStart;
    [SerializeField] private float angleIncreaseEnd;
    [SerializeField] private float angleIncreaseLimit;
    private float angleIncrease;



    GameObject temporaryGameObject;
    float positionX, positionY, positionZ;
    float radianValue;
    void Start()
    {
        angle = angleStart;
        angleIncrease = angleIncreaseStart;
        activeCount = 0;


        ChildPooling(createdCircleList, poolingCount);


    }
    private void Update()
    {

        if (nextCount >= poolingCount)
        {
            nextCount = poolingCount;
        }

        if (nextCount <= 0)
        {
            nextCount = 0;
        }
        CircleIncreas(createdCircleList, activeCount, nextCount);
        CircleDiscrease(createdCircleList, activeCount, nextCount);
        activeCount = nextCount;
    }

    void ChildPooling(List<GameObject> createdList, int count)
    {


        for (int i = 0; i < count; i++)
        {

            if (angleIncrease <= angleIncreaseEnd)
            {

                if (angle <= totalAngle) // bir sonraki objeye ge�me ge�mek i�in ge�me               
                {

                    angle += totalAngle / angleIncrease;

                    if (angle > totalAngle) // bir sonraki radiusa ge�me ve angle s�f�rla ve bir sonraki radiusta olu�acak obje miktar�n� artt�rmak i�in a�� hesaplamas�
                    {
                        angle = angleStart;
                        nextRadius += radiusIncrease;
                        angleIncrease += angleIncreaseLimit;
                    }
                }

                if (angleIncrease > angleIncreaseEnd)  // radiustaki obje mikatar� istenilen seviyeye ula�t���nda yap�lan i�lemler
                {
                    angleIncrease = angleIncreaseStart; //ilk de�erine e�itle
                    nextRadius = startRadius;       // ilk de�erine e�itle
                    angle = totalAngle;          //total de�erine e�itle
                    positionZ -= positionZIncrease; // azalt
                }
            }

            CreateObject(createdList);
        }
    }

    void CircleIncreas(List<GameObject> createdList, int activeCount, int nextCount)
    {
        for (int i = activeCount; i < nextCount; i++)
        {
            createdList[i].SetActive(true);
        }

    }

    void CircleDiscrease(List<GameObject> createdList, int activeCount, int nextCount)
    {

        for (int i = activeCount; i > nextCount; i--)
        {
            if (i > 0)
            {
                createdList[i - 1].SetActive(false);
            }

        }

    }

    private void CreateObject(List<GameObject> createdList)
    {
        radianValue = angle * Mathf.Deg2Rad;

        positionX = Mathf.Sin(radianValue);
        positionY = Mathf.Cos(radianValue);

        randomCircleNumber = Random.Range(0, circlePrefab.Length - 1);

        temporaryGameObject = Instantiate(circlePrefab[randomCircleNumber]);
        temporaryGameObject.SetActive(false);
        temporaryGameObject.transform.parent = circleParent.transform;
        createdList.Add(temporaryGameObject);

        temporaryGameObject.transform.localPosition = new Vector3(positionX * nextRadius, positionY * nextRadius, positionZ);
        temporaryGameObject.transform.localEulerAngles = new Vector3(90, 0, 0);
    }

}
