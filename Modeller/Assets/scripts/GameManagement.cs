using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject canvasim;
    public GameObject[] canvaslar;

    public GameObject[] levels = new GameObject[2];
    internal GameObject destroyLevel;
    public static GameManagement instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Time.timeScale = 0;
        levels[0].SetActive(true);
        Instantiate(levels[0], transform.position, transform.rotation);
        canvasim.SetActive(true);
    }
    private void Update()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            StartCoroutine(CanvasGoster());
            Time.timeScale = 1;
        }
    }
    IEnumerator CanvasGoster()
    {
        yield return new WaitForSeconds(2f);

        foreach (GameObject item in canvaslar)
        {
            item.SetActive(false);
        }
    }
    public void NextLevel()
    {
        destroyLevel = GameObject.FindGameObjectWithTag("Levels");
        Destroy(destroyLevel);
        Instantiate(levels[1], transform.position, transform.rotation);
        levels[1].SetActive(true);
        Invoke("NewLevel", 0f);
        canvasim.SetActive(true);
        StartCoroutine(CanvasGoster());
    }
    public void NewLevel()
    {
        cameraFollow.instance.ResetCamera();
        cameraFollow.instance.hedef = GameObject.FindWithTag("FrenchPlayer").transform;
    }

}
