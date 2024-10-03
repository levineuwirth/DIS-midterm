using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{

	public float speed = 2;
	public float lifeTime = 3;
	public Vector2 direction = new Vector2(-1, 0);

	void Start()
	{
		direction.Normalize();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 150;
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
