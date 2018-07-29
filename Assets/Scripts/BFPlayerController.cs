using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using TMPro;

[RequireComponent(typeof(GunComponent))]
public class BFPlayerController : MonoBehaviour {

	public int playerId = 0;
	public Player player;
	Rigidbody rb;
	public float speed = 50f, dashSpeed = 35f;
	Vector3 moveVector, lookVector;
	bool fire, dash, drop, start, select, reload, switchWeapons;
	public GameObject playerObj;

	public GunComponent gunComponent;

	bool bIsInIFrames = false;
	public int resources = 15, kills = 0;
	float rofTimer, dashTimer;
	public float dashInterval = 0.5f; // rate of fire in seconds
	public Rigidbody barrierPrefab;
	Transform spawner;

	public TextMeshProUGUI stats;

	// Use this for initialization
	void Start () {
		gunComponent = GetComponent<GunComponent>();
		player = ReInput.players.GetPlayer(playerId);
		rb = this.GetComponent<Rigidbody>();
		spawner = this.transform.GetChild(0).GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		ProcessInput();
		dashTimer -= Time.deltaTime;
		if (dashTimer > 0f)
		{
			bIsInIFrames = true;
		}
		else
		{
			bIsInIFrames = false;
		}
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
		switchWeapons = player.GetButtonDown("SwitchWeapons");
	}

	void ProcessInput() {
		rb.AddForce(moveVector * speed);
		rb.transform.forward = lookVector;
		//playerObj.transform.forward = lookVector;

		if(fire) gunComponent.Shoot();
		if(dash && dashTimer < 0) { 
			rb.AddForce(moveVector * dashSpeed, ForceMode.Impulse);
			dashTimer = 0.5f;
		}
		if(drop) DropBarrier();
		if(start) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		if(select) print("Select");
		if(reload) gunComponent.Reload();
		if (switchWeapons) gunComponent.SwitchWeapons();
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
		stats.text = "Resources = " + resources + "\nAmmo = " + gunComponent.currentClip + "/" + gunComponent.ammo + "\nTouches Avoided = " + kills;
	}

	void KillSelf()
	{

	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			if (!bIsInIFrames)
				KillSelf();
		}
	}
}
