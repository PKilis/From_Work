using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl : MonoBehaviour
{
    private Vector3 move;
    private float speed = 5f;
    private float runSpeed = 3f;
    [SerializeField] private float collactableSpeed = 0f;
    [SerializeField] private Animator animatorum;
    public GameObject restartCanvas;

    private void Start()
    {
        animatorum = GetComponent<Animator>();
    }
    void Update()
    {
        animatorum.Play("Movement", 3);
        transform.position += Vector3.forward * (runSpeed + collactableSpeed) * Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        move = new Vector3(x, 0f, 0f);
        transform.position += move;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("French"))
        {
            Destroy(other.gameObject);
            collactableSpeed += .3f;
        }
        if (other.gameObject.CompareTag("ABD"))
        {
            Destroy(other.gameObject);
            if (collactableSpeed <= 0)
            {
                runSpeed -= .3f;
            }
            else
            {
                collactableSpeed -= .3f;
            }
        }
        if (other.gameObject.CompareTag("winAreaa"))
        {
            GetComponent<Animator>().enabled = false;
            runSpeed = .01f;
            collactableSpeed = .01f;
            restartCanvas.SetActive(true);
        }
    }
}
