using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Pot : MonoBehaviour
{
    public float swingTime;
    public LayerMask potLayerMask;
    public Animator potAnimator { get; private set; }
    public GameObject bubblePrefab;
    public DoorCollider doorCollider;
    public GameObject waterPrefab;

    private AnimatorClipInfo[] CurrentClipInfo;
    private float shotDelay = 0.3f;
    private int shotType;


    //private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        swingTime = Random.Range(0.5f, 1.5f);
        potAnimator = gameObject.GetComponent<Animator>();
        potAnimator.SetBool("PotWait", true);
        StartCoroutine(AnimOpen());
    }

    // Update is called once per frame
    void Update()
    {
        CurrentClipInfo = this.potAnimator.GetCurrentAnimatorClipInfo(0);
        Debug.Log(CurrentClipInfo[0].clip.name);
        shotDelay -= Time.deltaTime;
        if (CurrentClipInfo[0].clip.name == "PotClose" || CurrentClipInfo[0].clip.name == "PotCloseIdle")
        {
            Debug.Log("right clip");
            if (shotType == 1)
            {
                if (shotDelay <= 0)
                {
                    ShootBubbleRand();
                    shotDelay = 0.3f;
                }
            }
            else if (shotType == 2)
            {
                Debug.Log("right shot type");
                if (shotDelay <= 0)
                {
                    Debug.Log("shot delay over");
                    ShootWater();
                    shotDelay = 0.3f;
                }
            }
        }
    }

    void ShootBubbleRand()
    {
        Instantiate(bubblePrefab, transform.position, Quaternion.identity);

    }

    void ShootWater()
    {
        Instantiate(waterPrefab, transform.position, Quaternion.identity);

    }

    private IEnumerator AnimOpen()
    {
        shotType = Random.Range(1, 3);
        float waitForOpen = 3;
        yield return new WaitForSeconds(waitForOpen);
        potAnimator.SetBool("PotWait", false);
        StartCoroutine(AnimClose());
        StopCoroutine(AnimOpen());
    }

    private IEnumerator AnimClose()
    {
        potAnimator.SetBool("PotWait", true);
        float waitForOpen = 3;
        yield return new WaitForSeconds(waitForOpen);
        potAnimator.SetBool("PotWait", false);
        StartCoroutine(AnimOpen());
        StopCoroutine(AnimClose());
    }
}
