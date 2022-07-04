using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOmniShot : MonoBehaviour
{
    
    private int projectileAmount = 4;

   
    private float startAngle = 110f, endAngle = 300f;

    private Vector2 projectileMoveDirection;

    

    // Start is called before the first frame update
    void Start()
    {
     InvokeRepeating("Fire", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        BossFight boss = GetComponent<BossFight>();
        if (boss.currentHealth <= 40)
        {
            projectileAmount = 5;
        }
    }
    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / projectileAmount;
        float angle = startAngle;

        for (int i = 0; i < projectileAmount; i++)
        {
            float projDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 projMoveVector = new Vector3(projDirX, projDirY, 0f);
            Vector2 projDir = (projMoveVector - transform.position).normalized;

            GameObject proj = OmniShotPool.OmnishootPoolInstance.GetProjectile();
            proj.transform.position = transform.position;
            proj.transform.rotation = transform.rotation;
            proj.SetActive(true);
            proj.GetComponent<OmniShotMovement>().SetMoveDirection(projDir);

            angle += angleStep;

        }
    }
}
