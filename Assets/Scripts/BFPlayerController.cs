using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using TMPro;

public class BFPlayerController : MonoBehaviour {

	public int playerId = 0;
	public Player player;
	Rigidbody rb;
	public float speed = 50f, dashSpeed = 35f, bulletSpeed = 2500f;
	Vector3 moveVector, lookVector;
	bool fire, dash, drop, start, select, reload;
	public GameObject playerObj;

	public int resources = 15, ammo = 90, kills = 0;		
	float timer;
	public float interval = 0.2f;			// rate of fire in seconds
	public Rigidbody bulletPrefab, barrierPrefab;
	Transform spawner;

	public TextMeshProUGUI stats;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(playerId);
		rb = this.GetComponent<Rigidbody>();
		spawner = this.transform.GetChild(0).GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		ProcessInput();
		timer -= Time.deltaTime;
		UpdateUI();
	}

	void GetInput() {
		moveVector.x = player.GetAxis("MoveHorizontal");
		moveVector.z = player.GetAxis("MoveVertical");
		lookVector.x = player.GetAxis("LookHorizontal");
		lookVector.z = player.GetAxis("LookVertical");

		fire = player.GetButton("Fire");
		dash = player.GetButtonDown("Dash");
		drop = player.GetButtonDown("DropBarrier");
		start = player.GetButtonDown("Start");
		select = player.GetButtonDown("Select");
		reload = player.GetButtonDown("Reload");
	}

	void ProcessInput() {
		rb.AddForce(moveVector * speed);
		rb.transform.forward = lookVector;
		//playerObj.transform.forward = lookVector;

		if(fire) Shoot();
		if(dash) rb.AddForce(moveVector * dashSpeed, ForceMode.Impulse);
		if(drop) DropBarrier();
		if(start) print("I wanna play!");
		if(select) print("Select");
		if(reload) Reload();

	}

	void Shoot() {
		
		if(ammo > 0 && timer <= 0) {
			print("Pow!");
			Rigidbody rb = Instantiate(bulletPrefab, spawner.position, spawner.rotation);
			rb.AddRelativeForce(Vector3.forward * bulletSpeed);
			rb.GetComponent<BulletController>().refPlayer = this;
			ammo--;
			timer = interval;
		}
		
	}

	void Reload() {
		print("Reloading!");
	}

	void DropBarrier() {
		if(resources >= 5) {
			print("Barrier Dropped!");
			resources -= 5;
			Instantiate(barrierPrefab, spawner.position, spawner.rotation);
		} else {
			print("You need more resources!");
		}
	}

	void UpdateUI() {
		stats.text = "Resources = " + resources + "\nAmmo = " + ammo + "\nTouches Avoided = " + kills;
	}

	// for picking up resources, ammo, and getting killed by enemies.
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Resource") {
			resources += 5;
		}
		else if(other.gameObject.tag == "Ammo") {
			ammo += 30;
		}
		else if(other.gameObject.tag == "Enemy") {
			print("Game over man! Game over!");
		}
	}
}
