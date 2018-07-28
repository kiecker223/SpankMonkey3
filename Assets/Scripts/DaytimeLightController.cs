using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeLightController : MonoBehaviour
{
	private Light m_SunLight;
	private Light m_MoonLight;
	private Light m_ActiveLight;
	public GameObject Sun;
	public GameObject Moon;
	private GameObject m_ActiveObject;

	private float m_StartIntensity;
	private float m_EndIntensity;

	private Quaternion m_StartRotation;
	private Quaternion m_EndRotation;

	public Color dayTimeStartColor;
	public Color dayTimeEndColor;

	public Color nightTimeStartColor;
	public Color nightTimeEndColor;

	private Color m_LastColor;
	private Color m_CurrentStartCol;
	private Color m_CurrentEndCol;

	private bool m_bDayTimeCycleInitialized = false;

	void Awake()
	{
		DayTimeCycle.SwitchToDaytimeActions.Add(DayLightCycle);
		DayTimeCycle.SwitchToNightTimeActions.Add(NightLightCycle);
		DayTimeCycle.ToDaytimeTransitions.Add(TransitionToDays);
		DayTimeCycle.ToNightTimeTransitions.Add(TransitionToNights);
	}

	void Start()
	{
		m_SunLight = Sun.GetComponent<Light>();
		m_MoonLight = Moon.GetComponent<Light>();
		m_StartRotation = Quaternion.Euler(0f, -30f, 0f);
		m_EndRotation = Quaternion.Euler(179.99f, -30f, 0f);
		Moon.SetActive(false);
	}

	private float DayTimeRatio()
	{
		float Result = 0f;
		if (DayTimeCycle.GetTime().IsDayTime)
		{
			Result = DayTimeCycle.GetTime().CurrentTime / 12;
		}
		else if (DayTimeCycle.GetTime().IsNightTime)
		{
			Result = (DayTimeCycle.GetTime().CurrentTime - 12) / 12;
		}
		return Result;
	}

	bool bNightFirst = true;
	void TransitionToNights(float f)
	{
		float factor = Mathf.Clamp(1 - f, 0f, 1f);
		if (bNightFirst)
		{
			m_ActiveObject.SetActive(false);
		}
	}

	bool bDayFirst = true;
	void TransitionToDays(float f)
	{
		float factor = Mathf.Clamp(1 - f, 0f, 1f);
		if (bDayFirst)
		{
			m_ActiveObject.SetActive(false);
		}
	}

	void NightLightCycle()
	{
		bNightFirst = true;
		Moon.SetActive(true);
		Sun.SetActive(false);
		m_ActiveLight = m_MoonLight;
		m_StartIntensity = 0.6f;
		m_EndIntensity = 0.6f;
		m_ActiveObject = Moon;
		m_CurrentStartCol = nightTimeStartColor;
		m_CurrentEndCol = nightTimeEndColor;
	}

	void DayLightCycle()
	{
		bDayFirst = true;
		Moon.SetActive(false);
		Sun.SetActive(true);
		m_ActiveLight = m_SunLight;
		m_StartIntensity = 1.0f;
		m_EndIntensity = 0.8f;
		m_ActiveObject = Sun;
		m_CurrentStartCol = dayTimeStartColor;
		m_CurrentEndCol = dayTimeEndColor;
	}

	void Update()
	{
		if (!m_bDayTimeCycleInitialized)
		{
			DayTimeCycle.Initialize();
			m_bDayTimeCycleInitialized = true;
		}
		float dtRatio = DayTimeRatio();
		m_ActiveObject.transform.rotation = Quaternion.Lerp(m_StartRotation, m_EndRotation, dtRatio);
		m_ActiveLight.intensity = Mathf.Lerp(m_StartIntensity, m_EndIntensity, dtRatio);
		m_ActiveLight.color = Color.Lerp(m_CurrentStartCol, m_CurrentEndCol, dtRatio);
		DayTimeCycle.Tick(Time.deltaTime);
	}
}
