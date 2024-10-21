using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Pot : MonoBehaviour
{
    [field: SerializeField] public float swingTime;
    [field: SerializeField] public LayerMask potLayerMask;
    [field: SerializeField] public Animator potAnimator { get; private set; }
    [field: SerializeField] public GameObject bubblePrefab;
    [field: SerializeField] public GameObject waterPrefab;

    private AnimatorClipInfo[] CurrentClipInfo;
    private float shotDelay = 0.3f;
    private int shotType;

    void Start()
    {
        swingTime = Random.Range(0.5f, 1.5f);
        potAnimator = gameObject.GetComponent<Animator>();
        potAnimator.SetBool("PotWait", true);
        StartCoroutine(AnimOpen());
    }

    void Update()
    {
        CurrentClipInfo = this.potAnimator.GetCurrentAnimatorClipInfo(0);
        shotDelay -= Time.deltaTime;
        if (CurrentClipInfo[0].clip.name == "PotClose" || CurrentClipInfo[0].clip.name == "PotCloseIdle")
        {
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
                if (shotDelay <= 0)
                {
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
