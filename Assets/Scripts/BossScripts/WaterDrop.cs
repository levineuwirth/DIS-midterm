using UnityEngine;
using System.Collections;

public class WaterDrop : MonoBehaviour
{

	[field: SerializeField] public float speed { get; private set; }
	[field: SerializeField] public float lifeTime { get; private set; }
	[field: SerializeField] public Vector2 direction { get; private set; }
	private float _waterAngleLeft = -1.2f;
	private float _waterAngleRight = 2.2f;

	void Start()
	{
		direction = new Vector2(Random.Range(_waterAngleLeft, _waterAngleRight), 1);
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
