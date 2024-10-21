using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;

public class Bubble : MonoBehaviour
{

	[field: SerializeField] public float speed { get; private set; }
	[field: SerializeField] public float lifeTime { get; private set; }
	[field: SerializeField] public Vector2 direction { get; private set; }

	private float _bubbleAngleLeft = -0.7f;
	private float _bubbleAngleRight = 1.7f;

	void Start()
	{
		direction = new Vector2(Random.Range(_bubbleAngleLeft, _bubbleAngleRight), 1);
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
