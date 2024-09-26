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
        //Debug.Log(ovenAnimator.GetBool("OvenOpen"));
    }


    private IEnumerator AnimOpen()
    {
        doorCollider.setDoorTrigger(false);
        float waitForOpen = Random.Range(1f, 5f);
        yield return new WaitForSeconds(waitForOpen);
        doorCollider.setDoorTrigger(true);
        ovenAnimator.SetBool("OvenWait", false);
        StartCoroutine(AnimClose());
        StopCoroutine(AnimOpen());
    }

    private IEnumerator AnimClose()
    {
        doorCollider.setDoorTrigger(false);
        ovenAnimator.SetBool("OvenWait", true);
        float waitForOpen = Random.Range(1f, 5f);
        yield return new WaitForSeconds(waitForOpen);
        doorCollider.setDoorTrigger(true);
        ovenAnimator.SetBool("OvenWait", false);
        StartCoroutine(AnimOpen());
        StopCoroutine(AnimClose());
    }
}
