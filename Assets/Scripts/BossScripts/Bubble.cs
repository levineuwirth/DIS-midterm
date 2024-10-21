using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;

public class Bubble : MonoBehaviour
{

	[field: SerializeField] public float speed;
	[field: SerializeField] public float lifeTime;
	[field: SerializeField] public Vector2 direction;

	private float bubbleAngleLeft = -0.7f;
	private float bubbleAngleRight = 1.7f;

	void Start()
	{
		direction = new Vector2(Random.Range(bubbleAngleLeft, bubbleAngleRight), 1);
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

}
