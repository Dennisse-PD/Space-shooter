using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniShot : MonoBehaviour
{

	[SerializeField]
	int numberOfProjectiles;

	[SerializeField]
	GameObject projectile;

	[SerializeField]
	GameObject positionAnchor;

	Vector2 startPoint;

	float radius, moveSpeed;

	//OmniShot Courtoutine Control
	private bool _isOmniShotActive = false;

	//omnishot cooldown
	private float _fireRate = 10.0f;
	private float _canfire = -0.6f;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(OmniShotRoutine());
		radius = 5f;
		moveSpeed = 5f;

	}

	// Update is called once per frame
	void Update()
	{
		//OnBecameInvisible();
	}
	void FireOmniShot()
    {

		startPoint = positionAnchor.transform.position;
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
	IEnumerator OmniShotRoutine()
	{
		yield return new WaitForSeconds(2.0f);
		FireOmniShot();
	}
	public void ActivateOmniShot()
    {
		_isOmniShotActive = true;
    }
	void OmniMovement()
    {

    }
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				player.damagePlayer();
				Destroy(this.gameObject);//check if it really destroys this
			}
			

		}
		if (other.tag == "Deadzone")
		{
			Debug.Log("I hit a deadzone!");
			Destroy(this.gameObject);
		}
	}
	
}