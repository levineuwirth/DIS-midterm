using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Pot : MonoBehaviour
{
    public float swingTime;
    public LayerMask potLayerMask;
    public Sprite[] framesOpen;
    public Sprite[] framesClose;
    public Animator potAnimator { get; private set; }
    public GameObject bubblePrefab;

    private AnimatorClipInfo[] CurrentClipInfo;
    private float shotDelay = 0.5f;


    //private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        swingTime = Random.Range(1f, 5f);
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
        if (CurrentClipInfo[0].clip.name == "PotClose" && shotDelay <= 0 || CurrentClipInfo[0].clip.name == "PotCloseIdle" && shotDelay <= 0)
        {
            ShootBubble();
            shotDelay = 0.5f;
        }
    }

    void ShootBubble()
    {
        Instantiate(bubblePrefab, transform.position, Quaternion.identity);

    }

    private IEnumerator AnimOpen()
    {
        float waitForOpen = 2;
        yield return new WaitForSeconds(waitForOpen);
        potAnimator.SetBool("PotWait", false);
        StartCoroutine(AnimClose());
        StopCoroutine(AnimOpen());
    }

    private IEnumerator AnimClose()
    {
        potAnimator.SetBool("PotWait", true);
        float waitForOpen = 10;
        yield return new WaitForSeconds(waitForOpen);
        potAnimator.SetBool("PotWait", false);
        StartCoroutine(AnimOpen());
        StopCoroutine(AnimClose());
    }
}
