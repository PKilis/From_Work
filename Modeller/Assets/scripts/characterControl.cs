using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl : MonoBehaviour
{
    [Header("ForRun")]
    private Vector3 move;
    private float kaydirmaSpeed = 5f;
    private float runSpeed = 3f;
    private float wrongCollided = 1f;
    private float oldSpeed;
    private float collactableSpeed = 0f;

    [Header("Particle")]
    public ParticleSystem particleBlue;
    public ParticleSystem particleRed;

    [Header("Components")]
    private Rigidbody rb;
    public Animator anim;

    private bool yerdeMi = true;
    public float speedModifier = 0.005f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        #region pc controller
        transform.position += Vector3.forward * (runSpeed + collactableSpeed) * Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * kaydirmaSpeed * Time.deltaTime;
        move = new Vector3(x, 0f, 0f);
        transform.position += move;
        rb.AddForce(move);
        if (Input.GetButtonDown("Jump") && yerdeMi == true)
        {
            rb.AddForce(new Vector3(0f, 3.8f, 1f), ForceMode.Impulse);
            anim.SetTrigger("Jump");
            yerdeMi = false;
        }
        #endregion
        #region android controller
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier, transform.position.y, transform.position.z);
            }
            if (touch.phase == TouchPhase.Began && yerdeMi == true)
            {
                rb.AddForce(new Vector3(0f, 3.8f, 1f), ForceMode.Impulse);
                anim.SetTrigger("Jump");
                yerdeMi = false;
            }
        }
        #endregion
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("French"))
        {
            particleBlue.Play();
            Destroy(other.gameObject);
            collactableSpeed += .3f;
        }
        if (other.gameObject.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("ABD"))
        {
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
            StartCoroutine(YanlisObje());
        }
        if (other.gameObject.CompareTag("winAreaa"))
        {
            GetComponent<Animator>().enabled = false;
            runSpeed = .01f;
            collactableSpeed = .01f;
            GameManagement management = FindObjectOfType<GameManagement>();
            management.NextLevel();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            yerdeMi = true;
            anim.ResetTrigger("Jump");
        }
        if (collision.gameObject.CompareTag("trampoline"))
        {
            rb.AddForce(new Vector3(0f, 3f, 1f), ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
    }
    IEnumerator YanlisObje()
    {
        oldSpeed = runSpeed;
        runSpeed = wrongCollided;

        yield return new WaitForSeconds(.3f);

        runSpeed = oldSpeed;
    }
}
