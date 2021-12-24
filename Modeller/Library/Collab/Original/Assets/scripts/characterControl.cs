using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl : MonoBehaviour
{
    private Vector3 move;
    private Rigidbody rb;
    private float speed = 5f;
    private float runSpeed = 3f;
    [SerializeField] private float collactableSpeed = 0f;
    public GameObject restartCanvas;
    private Touch touch;
    public float speedModifier = 0.005f;
    public ParticleSystem particleBlue;
    public ParticleSystem particleRed;
    private Animator anim;
    private Animation anim2;
    private bool yerdeMi = true;


    private void Start()
    {
        anim = GetComponent<Animator>();
        anim2 = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        anim.Play("Movement");
        #region pc controlls
        transform.position += Vector3.forward * (runSpeed + collactableSpeed) * Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        move = new Vector3(x, 0f, 0f);
        transform.position += move;
        rb.AddForce(move);

        if (Input.GetButtonDown("Jump") && yerdeMi == true)
        {
            //anim.SetTrigger("Jump");
            anim2.Play();
            rb.AddForce(new Vector3(0f, 5f, 0f), ForceMode.Impulse);
            yerdeMi = false;
        }

        #endregion
        #region android controlls
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier, transform.position.y, transform.position.z);
            }
        }
        #endregion

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("French"))
        {
            particleBlue.Play();
            Destroy(other.gameObject);
            collactableSpeed += .3f;
        }
        if (other.gameObject.CompareTag("ABD"))
        {
            StartCoroutine(KarakteriYavaslat());
            particleRed.Play(); 
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
            GameManagement.instance.WinAndShow();
            winnerWho kazananKim = FindObjectOfType<winnerWho>();
            if (restartCanvas)
            {
                kazananKim.kazananKisi.text = "Kazanan " + kazananKim.A.name + "l�".ToString();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            yerdeMi = true;
        }
        if (collision.gameObject.CompareTag("trampoline"))
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector3(0f, 2f, 1f), ForceMode.Impulse);
        }
    }
    IEnumerator KarakteriYavaslat()
    {
        runSpeed = 0.5f;
        yield return new WaitForSeconds(.5f);
        runSpeed = 3f;
    }
}
