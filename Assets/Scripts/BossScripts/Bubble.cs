using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;

public class Bubble : MonoBehaviour
{

	public float speed;
	public float lifeTime;
	public Vector2 direction;

	void Start()
	{
		direction = new Vector2(Random.Range(-1.25f, 2.25f), 1);
		direction.Normalize();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void Update()
	{
		lifeTime -= Time.deltaTime;
		if(lifeTime < 0) {
			Destroy(this.gameObject);
		}
		transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
	}

	/**
		void OnTriggerEnter2D(Collider2D other)
		{

		}
	**/
}