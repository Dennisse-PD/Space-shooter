using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniShot : MonoBehaviour
{
	//--------------------------------------------------------//
	[SerializeField]
	int numberOf_projectilePrefabs;//how many to instantiate 

	[SerializeField]
	GameObject _projectilePrefab;//to instatntiate 

	[SerializeField]
	GameObject positionAnchor; //spawn point

	Vector2 startPoint; //stores starting point variables

	//--------------------------------------------------------//



	float radius, moveSpeed; //how fast the projectiles move

	

	

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
		
		

	}
	void FireOmniShot()
    {
		
		startPoint = positionAnchor.transform.position;
		Spawn_projectilePrefabs(numberOf_projectilePrefabs);
		


	}
	void Spawn_projectilePrefabs(int numberOf_projectilePrefabs)
	{
		float angleStep = 360f / numberOf_projectilePrefabs;
		float angle = 0f;

		for (int i = 0; i <= numberOf_projectilePrefabs - 1; i++)
		{

			float _projectilePrefabDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
			float _projectilePrefabDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

			Vector2 _projectilePrefabVector = new Vector2(_projectilePrefabDirXposition, _projectilePrefabDirYposition);
			Vector2 _projectilePrefabMoveDirection = (_projectilePrefabVector - startPoint).normalized * moveSpeed;

			var proj = Instantiate(_projectilePrefab, startPoint, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(_projectilePrefabMoveDirection.x, _projectilePrefabMoveDirection.y);

			angle += angleStep;
		}
	}
	IEnumerator OmniShotRoutine()
	{
		yield return new WaitForSeconds(4.0f);
		FireOmniShot();
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
			Debug.Log("I hit a deadzone");
			Destroy(this.gameObject);
		}
	}
	
}