using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniShot : MonoBehaviour
{

	[SerializeField]
	int numberOfProjectiles;

	[SerializeField]
	GameObject projectile;

	Vector2 startPoint;

	float radius, moveSpeed;

	//Laser fire cooldown
	private float _fireRate = 0.5f;
	private float _canfire = -0.2f;

	// Use this for initialization
	void Start()
	{
		radius = 5f;
		moveSpeed = 5f;
	}

	// Update is called once per frame
	void Update()
	{	
			FireOmniShot();


	}
	void FireOmniShot()
    {
		_fireRate = Random.Range(3f, 7f);
		_canfire = Time.time + _fireRate;
		startPoint = transform.position;
		SpawnProjectiles(numberOfProjectiles);
	}
	void SpawnProjectiles(int numberOfProjectiles)
	{
		float angleStep = 360f / numberOfProjectiles;
		float angle = 0f;

		for (int i = 0; i <= numberOfProjectiles - 1; i++)
		{

			float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
			float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

			Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
			Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

			var proj = Instantiate(projectile, startPoint, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity =
				new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

			angle += angleStep;
		}
	}
}