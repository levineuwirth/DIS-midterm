using UnityEngine;
using System.Collections;

public class WaterDrop : MonoBehaviour
{

	[field: SerializeField] public float speed ;
	[field: SerializeField] public float lifeTime;
	[field: SerializeField] public Vector2 direction;
	private float waterAngleLeft = -1.2f;
	private float waterAngleRight = 2.2f;

	void Start()
	{
		direction = new Vector2(Random.Range(waterAngleLeft, waterAngleRight), 1);
		direction.Normalize();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void Update()
	{
		lifeTime -= Time.deltaTime;
		transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

		if(lifeTime < 0) {
			Destroy(this.gameObject);
		}
	}
}
