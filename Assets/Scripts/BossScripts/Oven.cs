using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Oven : MonoBehaviour
{
    [field: SerializeField] public float swingTime;
    [field: SerializeField] public LayerMask ovenLayerMask;
    [field: SerializeField] public Animator ovenAnimator { get; private set; }
    [field: SerializeField] public GameObject fireballPrefab;

    private int counter;

    private AnimatorClipInfo[] CurrentClipInfo;

    private float shotDelayWall = 1.5f;
    private float shotDelayZag = 0.5f;
    private int shotType;

    void Start()
    {
        swingTime = Random.Range(1f, 5f);
        ovenAnimator = gameObject.GetComponent<Animator>();
        ovenAnimator.SetBool("OvenWait", true);
        StartCoroutine(AnimOpen());
        counter = 1;
    }

    void Update()
    {
        CurrentClipInfo = this.ovenAnimator.GetCurrentAnimatorClipInfo(0);
        shotDelayWall -= Time.deltaTime;
        shotDelayZag -= Time.deltaTime;
        if(CurrentClipInfo[0].clip.name == "OvenIdleOpen" || CurrentClipInfo[0].clip.name == "OvenOpen")
        {
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
                if(shotDelayZag <= 0){
                    ShootZigZag(counter);
                    counter++;
                    if (counter == 6)
                    {
                        counter = 1;
                    }
                    shotDelayZag = 0.5f;
                }
            }
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
        float waitForOpen = 3f;
        yield return new WaitForSeconds(waitForOpen);
        ovenAnimator.SetBool("OvenWait", false);
        StartCoroutine(AnimClose());
        StopCoroutine(AnimOpen());
    }

    private IEnumerator AnimClose()
    {
        ovenAnimator.SetBool("OvenWait", true);
        float waitForOpen = 3f;
        yield return new WaitForSeconds(waitForOpen);
        ovenAnimator.SetBool("OvenWait", false);
        StartCoroutine(AnimOpen());
        StopCoroutine(AnimClose());
    }
}
