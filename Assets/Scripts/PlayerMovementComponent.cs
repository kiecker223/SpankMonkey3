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
		// Can we check for this the triggers to be down like this?????
		return Input.GetButtonDown(Buttons[(int)button]);
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
	public Vector3 direction;
	public Vector3 movementDir;
	public Vector3 rbVel;
	public GameObject playerObj;
	private NavMeshAgent m_NavMesh;

	void Start()
    {
		m_NavMesh = playerObj.GetComponent<NavMeshAgent>();
		m_NavMesh.updateRotation = false;
	}
	
	void Update()
    {
		Vector2 controllerDir = ControllerMappings.GetRightStickDirection();
		if (controllerDir.magnitude > 1e-1f)
		{
			direction = new Vector3(controllerDir.x, 0f, -controllerDir.y).normalized;
			playerObj.transform.forward = direction;
		}
		Vector2 movementPow = ControllerMappings.GetLeftStickDirection();
		if (movementPow.magnitude > 1e-1f)
		{
			movementDir = new Vector3(movementPow.x, 0f, -movementPow.y).normalized * (speed * Time.deltaTime);
			Vector3 destination = transform.position + movementDir;
			m_NavMesh.destination = destination;
		}
		else
		{
			movementDir = new Vector3(0f, 0f, 0f);
			m_NavMesh.destination = transform.position;
		}
		Vector3 oldPosition = transform.position;
		oldPosition.x = playerObj.transform.position.x;
		oldPosition.z = playerObj.transform.position.z;
		transform.position = oldPosition;
	}
}
