using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{

	[field: SerializeField] public float speed { get; private set; }
	[field: SerializeField] public float lifeTime { get; private set; }
	[field: SerializeField] public Vector2 direction { get; private set; }

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
