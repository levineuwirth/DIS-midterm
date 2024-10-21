using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Oven : MonoBehaviour
{
    [field: SerializeField] public float swingTime { get; private set; }
    [field: SerializeField] public LayerMask ovenLayerMask { get; private set; }
    [field: SerializeField] public Animator ovenAnimator { get; private set; }
    [field: SerializeField] public GameObject fireballPrefab { get; private set; }

    private int _counter;

    private AnimatorClipInfo[] _CurrentClipInfo;

    private float _shotDelayWall = 1.5f;
    private float _shotDelayZag = 0.5f;
    private int _shotType;

    void Start()
    {
        swingTime = Random.Range(1f, 5f);
        ovenAnimator = gameObject.GetComponent<Animator>();
        ovenAnimator.SetBool("OvenWait", true);
        StartCoroutine(AnimOpen());
        _counter = 1;
    }

    void Update()
    {
        _CurrentClipInfo = this.ovenAnimator.GetCurrentAnimatorClipInfo(0);
        _shotDelayWall -= Time.deltaTime;
        _shotDelayZag -= Time.deltaTime;
        if(_CurrentClipInfo[0].clip.name == "OvenIdleOpen" || _CurrentClipInfo[0].clip.name == "OvenOpen")
        {
            if(_shotType == 1)
            {
                if(_shotDelayWall <= 0)
                {
                    ShootFirewall();
                    _shotDelayWall = 1.5f;
                }
            }
            else if(_shotType == 2)
            {
                if(_shotDelayZag <= 0){
                    ShootZigZag(_counter);
                    _counter++;
                    if (_counter == 6)
                    {
                        _counter = 1;
                    }
                    _shotDelayZag = 0.5f;
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

    void ShootZigZag(int _counter)
    {
        float spacingY = 1.5f;
        float spacingX = 4f;

        Instantiate(fireballPrefab, transform.position + new Vector3(_counter * spacingX - 4, _counter * spacingY - 4, 0), Quaternion.identity);

    }


    private IEnumerator AnimOpen()
    {
        _shotType = Random.Range(1, 3);
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
