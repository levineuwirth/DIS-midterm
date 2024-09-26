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

    private SpriteRenderer spriteRenderer;
    private int counter;
    private bool running = false;
    private bool open = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        swingTime = Random.Range(1f, 5f);

        ovenLayerMask = LayerMask.GetMask("OvenLayer");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = gameObject.transform.position;
        float distance = 20;

        swingTime -= Time.deltaTime;
        if(swingTime <= 0 && !running)
        {
            StartCoroutine(AnimOpen());
            running = true;
            swingTime = Random.Range(1f, 5f);
        }
        if (running && !open)
        {
            RaycastHit2D hitLeft = Physics2D.Raycast(origin, Vector2.left, distance, ~ovenLayerMask);
            if(hitLeft.collider != null)
            {
                Debug.Log(hitLeft.collider);
                if (hitLeft.collider.CompareTag("Player"))
                {
                    Debug.Log("Player Detected");
                    ReloadScene();
                }
            }
        }
    }

    private IEnumerator AnimOpen()
    {
        float animWait = 0.1f / framesOpen.Length;
        counter = 0;
        while (counter < framesOpen.Length)
        {
            spriteRenderer.sprite = framesOpen[counter];
            yield return new WaitForSeconds(animWait);
            counter++;
        }
        open = true;
        float waitForClose = Random.Range(1f, 2f);
        yield return new WaitForSeconds(waitForClose);
        open = false;
        StartCoroutine(AnimClose());
        StopCoroutine(AnimOpen());
    }

    private IEnumerator AnimClose()
    {
        float animWait = 0.1f / framesClose.Length;
        counter = 0;
        while (counter < framesClose.Length)
        {
            spriteRenderer.sprite = framesClose[counter];
            yield return new WaitForSeconds(animWait);
            counter++;
        }
        running = false;
        StopCoroutine(AnimClose());
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
