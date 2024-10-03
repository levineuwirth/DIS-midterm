using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Oven : MonoBehaviour
{
    public float swingTime;
    public LayerMask ovenLayerMask;
    public Sprite[] framesOpen;
    public Sprite[] framesClose;
    public Animator ovenAnimator { get; private set; }
    public DoorCollider doorCollider;
    public GameObject fireballPrefab;

    private bool shootReady;

    private AnimatorClipInfo[] CurrentClipInfo;

    private float shotDelay = 1.5f;
    private int shotType;


    //private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        swingTime = Random.Range(1f, 5f);
        ovenAnimator = gameObject.GetComponent<Animator>();
        ovenAnimator.SetBool("OvenWait", true);
        StartCoroutine(AnimOpen());
    }

    // Update is called once per frame
    void Update()
    {
        CurrentClipInfo = this.ovenAnimator.GetCurrentAnimatorClipInfo(0);
        shotDelay -= Time.deltaTime;
        if (CurrentClipInfo[0].clip.name == "OvenIdleOpen" && shotDelay <= 0 || CurrentClipInfo[0].clip.name == "OvenOpen" && shotDelay <= 0)
        {
            if(shotType == 1)
            {
                ShootFirewall();
            }
            else if(shotType == 2)
            {
                ShootZigZag();
            }
            shotDelay = 1.5f;
        }

    }

    void ShootFirewall()
    {
        float spacing = 1.5f;

        Instantiate(fireballPrefab, transform.position + new Vector3(0, -2 * spacing, 0), Quaternion.identity);
        Instantiate(fireballPrefab, transform.position + new Vector3(0, -spacing, 0), Quaternion.identity);
        Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Instantiate(fireballPrefab, transform.position + new Vector3(0, spacing, 0), Quaternion.identity);
        Instantiate(fireballPrefab, transform.position + new Vector3(0, 2 * spacing, 0), Quaternion.identity);
        Instantiate(fireballPrefab, transform.position + new Vector3(0, 3 * spacing, 0), Quaternion.identity);

    }

    void ShootZigZag()
    {
        float spacingY = 1.5f;
        float spacingX = 4f;

        Instantiate(fireballPrefab, transform.position + new Vector3(2 * spacingX, -2 * spacingY, 0), Quaternion.identity);
        Instantiate(fireballPrefab, transform.position + new Vector3(spacingX, -spacingY, 0), Quaternion.identity);
        Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Instantiate(fireballPrefab, transform.position + new Vector3(-spacingX, spacingY, 0), Quaternion.identity);
        Instantiate(fireballPrefab, transform.position + new Vector3(-2 * spacingX, 2 * spacingY, 0), Quaternion.identity);
        Instantiate(fireballPrefab, transform.position + new Vector3(-3 * spacingX, 3 * spacingY, 0), Quaternion.identity);

    }


    private IEnumerator AnimOpen()
    {
        shotType = Random.Range(1, 3);
        float waitForOpen = 1;
        yield return new WaitForSeconds(waitForOpen);
        ovenAnimator.SetBool("OvenWait", false);
        StartCoroutine(AnimClose());
        StopCoroutine(AnimOpen());
    }

    private IEnumerator AnimClose()
    {
        ovenAnimator.SetBool("OvenWait", true);
        float waitForOpen = 10;
        yield return new WaitForSeconds(waitForOpen);
        ovenAnimator.SetBool("OvenWait", false);
        StartCoroutine(AnimOpen());
        StopCoroutine(AnimClose());
    }
}
