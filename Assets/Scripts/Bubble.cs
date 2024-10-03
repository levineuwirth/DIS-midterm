using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{

	public float speed = 5;
	public float lifeTime = 3;
	private Vector2 direction;

	void Start()
	{
		direction = new Vector2(Random.Range(-1.25f, 2.25f), 1);
		direction.Normalize();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void Update()
	{
		transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
	}

	/**
		void OnTriggerEnter2D(Collider2D other)
		{

		}
	**/
}
