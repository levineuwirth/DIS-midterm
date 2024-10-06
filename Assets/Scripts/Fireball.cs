using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{

	public float speed;
	public float lifeTime;
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
		
		lifeTime -= Time.deltaTime;
		if(lifeTime < 0) {
			Destroy(this.gameObject);
		}
	}

/**
	void OnTriggerEnter2D(Collider2D other)
	{

	}
**/
}
