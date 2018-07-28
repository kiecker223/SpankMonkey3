using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ControllerAxis
{
	LS_Horizontal,
	LS_Vertical,
	RS_Horizontal,
	RS_Vertical,
	L_Trigger,
	R_Trigger,
	NUM
}

public enum ControllerButtons
{
	Button_A,
	Button_B,
	Button_X,
	Button_Y,
	Bumper_L,
	Bumper_R,
	Button_Start,
	LeftStickDown,
	RightStickDown,
	LeftTrigger,
	RightTrigger,
	NUM
}

public static class ControllerMappings
{
	public static string[] Axises = new string[]
	{
		"JHorizontal",
		"JVertical",
		"rx axis",
		"ry axis",
		"9",
		"10"
	};

	public static string[] Buttons = new string[]
	{
		"joystick button 0",
		"joystick button 1",
		"joystick button 2",
		"joystick button 3",
		"joystick button 4",
		"joystick button 5",
		"joystick button 7",
		"joystick button 8",
		"joystick button 9",
		"LTrigger",
		"RTrigger"
	};

	public static float GetAxis(ControllerAxis axis)
	{
		return Input.GetAxis(Axises[(int)axis]);
	}

	public static bool GetButton(ControllerButtons button)
	{
		if ((int)button < 9) return Input.GetButton(Buttons[(int)button]);
		else return Input.GetAxis(Buttons[(int)button]) > 1e-3f;
	}

	public static bool GetButtonDown(ControllerButtons button)
	{
		if ((int)button < 9) return Input.GetButtonDown(Buttons[(int)button]);
		else return Input.GetAxis(Buttons[(int)button]) > 1e-3f;
	}

	public static bool GetButtonUp(ControllerButtons button)
	{
		if ((int)button < 9) return Input.GetButtonUp(Buttons[(int)button]);
		else return Input.GetAxis(Buttons[(int)button]) < 1e-3f;
	}

	public static Vector2 GetRightStickDirection()
	{
		return new Vector2(GetAxis(ControllerAxis.RS_Horizontal), GetAxis(ControllerAxis.RS_Vertical));
	}

	public static Vector2 GetLeftStickDirection()
	{
		return new Vector2(GetAxis(ControllerAxis.LS_Horizontal), GetAxis(ControllerAxis.LS_Vertical));
	}
}

public class PlayerMovementComponent : MonoBehaviour
{
	public float speed;
	public float stamina = 36f;
	public Vector3 direction;
	public Vector3 movementDir;
	public Vector3 rbVel;
	public GameObject playerObj;
	private bool m_bCanDash = true;
	private bool m_bIsDashing = false;
	private NavMeshAgent m_NavMesh;
	private HealthComponent m_PlayerHealth;

	void Start()
    {
		m_NavMesh = playerObj.GetComponent<NavMeshAgent>();
		m_PlayerHealth = GetComponent<HealthComponent>();
		m_NavMesh.updateRotation = false;
	}
	
	// bad name
	IEnumerator InvincibleForIFrames()
	{
		m_PlayerHealth.bIsInIFrames = true;
		yield return new WaitForSeconds(1f);
	}

	float timer;
	void Update()
	{
		// Movement and look
		Vector2 controllerDir = ControllerMappings.GetRightStickDirection();
		if (controllerDir.magnitude > 1e-1f)
		{
			direction = new Vector3(controllerDir.x, 0f, -controllerDir.y).normalized;
			playerObj.transform.forward = direction;
		}

		if (!m_bIsDashing)
		{
			Vector2 movementPow = ControllerMappings.GetLeftStickDirection();
			if (movementPow.magnitude > 1e-1f)
			{
				float dashMultiplier = 1f;
				float totalSpeed = speed;
				if (ControllerMappings.GetButtonDown(ControllerButtons.LeftTrigger) && m_bCanDash)
				{
					if (stamina - 25f > 0)
					{
						m_bCanDash = false;
						m_bIsDashing = true;
						m_PlayerHealth.bIsInIFrames = true;
						m_NavMesh.speed = 2f;
						dashMultiplier = 25f;
						totalSpeed *= dashMultiplier;
						stamina -= 25f;
					}
				}
				movementDir = new Vector3(movementPow.x, 0f, -movementPow.y) * (totalSpeed * Time.deltaTime);
				Vector3 destination = transform.position + (movementDir);
				m_NavMesh.destination = destination;
			}
			else
			{
				movementDir = new Vector3(0f, 0f, 0f);
				m_NavMesh.destination = transform.position;
			}
		}
		if (m_NavMesh.speed > 1f)
		{
			m_NavMesh.speed = m_NavMesh.speed - (Time.deltaTime);
		}
		// Dashing
		float dt = Time.deltaTime;
		timer += dt;
		if (timer > 0.9f)
		{
			m_bCanDash = true;
			m_bIsDashing = false;
			m_PlayerHealth.bIsInIFrames = false;
			m_NavMesh.speed = 1f;
			timer = 0f;
		}
		if (stamina < 36f)
		{
			// 23 stamina per 1.5 seconds
			stamina += 23f * (dt / 1.5f);
		}

		// Updating camera position
		// TODO: add lerping
		Vector3 oldPosition = transform.position;
		oldPosition.x = playerObj.transform.position.x;
		oldPosition.z = playerObj.transform.position.z;
		transform.position = oldPosition;
	}
}
