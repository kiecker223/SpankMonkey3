using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GunType
{
	GT_Laser,
	GT_Bullets
}

public class GunComponent : MonoBehaviour
{
	// This is represented as how many milliseconds until it fires again
	[Range(0,1000)]
	public float rateOfFire = 1;
	public AudioClip soundClip;
	[Range(1,5)]
	public float bulletSpeed = 2500;

	// probably set this in Start() as it will not work if we plan on using more than one type of gun
	public Transform gunTransform;
	BFPlayerController m_OwningController;
	Transform m_Spawner;
	public GunType gunType;
	private LineRenderer m_LaserRenderer;
	public Material laserMaterial;
	public Rigidbody bulletPrefab;
	public int resources = 15;
	public int ammo = 90;
	public int currentClip = 20;
	public int maxClip = 20;
	private float rofTimer = 0;
	public float rofInterval = 0.2f;
	private float m_clearLinesTimer = 0.1f;
	public string weaponName;

	void Start()
	{
		m_OwningController = GetComponent<BFPlayerController>();
		m_LaserRenderer = GetComponent<LineRenderer>();
		m_Spawner = transform.GetChild(0).GetChild(0);
	}

	void Update()
	{
		rofTimer -= Time.deltaTime;
		if (m_clearLinesTimer > 0f)
			m_clearLinesTimer -= Time.deltaTime;
		if (m_clearLinesTimer < 0f)
			m_LaserRenderer.positionCount = 0;
	}

	public void SwitchWeapons()
	{
		switch (gunType)
		{
			case GunType.GT_Bullets:
				gunType = GunType.GT_Laser;
				break;
			case GunType.GT_Laser:
				gunType = GunType.GT_Bullets;
				break;
		}
		Reload();
	}

	public void Shoot()
	{
		if (currentClip > 0 && rofTimer <= 0)
		{
			if (gunType == GunType.GT_Bullets)
			{
				Rigidbody rb = Instantiate(bulletPrefab, m_Spawner.position, m_Spawner.rotation);
				rb.AddRelativeForce(Vector3.forward * bulletSpeed);
				rb.GetComponent<BulletController>().refPlayer = m_OwningController;
				currentClip--;
				rofTimer = rofInterval;
			}
			else if (gunType == GunType.GT_Laser)
			{
				RaycastHit rayHit;
				if (Physics.Raycast(new Ray(m_Spawner.position, m_Spawner.forward), out rayHit, 400f))
				{
					var otherGameObj = rayHit.collider.gameObject;
					if (otherGameObj.tag == "Enemy")
					{
						var enemyController = otherGameObj.GetComponent<EnemyController>();
						enemyController.ApplyForce(m_Spawner.forward * 2);

						int otherHealth = (enemyController.health -= 3);
						if (otherHealth <= 0)
						{
							EnemySpawner.totalEnemies--;
							m_OwningController.kills++;
							Destroy(otherGameObj);
						}
					}
				}
				m_LaserRenderer.material = laserMaterial;
				m_LaserRenderer.startWidth = 0.2f;
				m_LaserRenderer.endWidth = 0.2f;
				m_LaserRenderer.positionCount = 2;
				m_LaserRenderer.SetPositions(new Vector3[] { m_Spawner.position, m_Spawner.position + (m_Spawner.forward * 400f) });
				m_clearLinesTimer = 1f;
				currentClip -= 2;
				rofTimer = rofInterval * 3;
			}
			if (currentClip == 0)
				Reload();
		}
	}

	public void Reload()
	{
		StartCoroutine(InternalReload());
	}

	private IEnumerator InternalReload()
	{
		yield return new WaitForSeconds(0.3f);
		ammo += currentClip;
		if (ammo >= maxClip)
		{
			currentClip = maxClip;
			ammo -= maxClip;
		}
		else
		{
			currentClip = ammo;
			ammo = 0;
		}
	}
}
