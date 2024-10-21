using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Pot : MonoBehaviour
{
    [field: SerializeField] public float swingTime { get; private set; }
    [field: SerializeField] public LayerMask potLayerMask { get; private set; }
    [field: SerializeField] public Animator potAnimator { get; private set; }
    [field: SerializeField] public GameObject bubblePrefab { get; private set; }
    [field: SerializeField] public GameObject waterPrefab { get; private set; }
    private AnimatorClipInfo[] _currentClipInfo;
    private float _shotDelay = 0.3f;
    private int _shotType;

    void Start()
    {
        swingTime = Random.Range(0.5f, 1.5f);
        potAnimator = gameObject.GetComponent<Animator>();
        potAnimator.SetBool("PotWait", true);
        StartCoroutine(AnimOpen());
    }

    void Update()
    {
        _currentClipInfo = this.potAnimator.GetCurrentAnimatorClipInfo(0);
        _shotDelay -= Time.deltaTime;
        if (_currentClipInfo[0].clip.name == "PotClose" || _currentClipInfo[0].clip.name == "PotCloseIdle")
        {
            if (_shotType == 1)
            {
                if (_shotDelay <= 0)
                {
                    ShootBubbleRand();
                    _shotDelay = 0.3f;
                }
            }
            else if (_shotType == 2)
            {
                if (_shotDelay <= 0)
                {
                    ShootWater();
                    _shotDelay = 0.3f;
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
        _shotType = Random.Range(1, 3);
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
