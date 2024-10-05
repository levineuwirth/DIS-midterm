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
    private int counter;

    private AnimatorClipInfo[] CurrentClipInfo;

    private float shotDelayWall = 1.5f;
    private float shotDelayZag = 0.5f;
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
        counter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentClipInfo = this.ovenAnimator.GetCurrentAnimatorClipInfo(0);
        shotDelayWall -= Time.deltaTime;
        shotDelayZag -= Time.deltaTime;
        if(CurrentClipInfo[0].clip.name == "OvenIdleOpen" || CurrentClipInfo[0].clip.name == "OvenOpen")
        {
            Debug.Log("right clip");
            if(shotType == 1)
            {
                if(shotDelayWall <= 0)
                {
                    ShootFirewall();
                    shotDelayWall = 1.5f;
                }
            }
            else if(shotType == 2)
            {
                Debug.Log("right shot type");
                if(shotDelayZag <= 0){
                    Debug.Log("shot delay over");
                    ShootZigZag(counter);
                    counter++;
                    if (counter == 6)
                    {
                        Debug.Log("counter reset");
                        counter = 1;
                    }
                    shotDelayZag = 0.5f;
                }
            }
            Debug.Log("shot delay reset");
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

    void ShootZigZag(int counter)
    {
        float spacingY = 1.5f;
        float spacingX = 4f;

        Instantiate(fireballPrefab, transform.position + new Vector3(counter * spacingX - 4, counter * spacingY - 4, 0), Quaternion.identity);

    }


    private IEnumerator AnimOpen()
    {
        shotType = Random.Range(1, 3);
        float waitForOpen = 1f;
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
