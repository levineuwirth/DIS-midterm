using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{

	[field: SerializeField] public float speed;
	[field: SerializeField] public float lifeTime;
	[field: SerializeField] public Vector2 direction = new Vector2(-1, 0);

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
}
