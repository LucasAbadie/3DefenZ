using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolMath {

	public static float GetPercent(float value, float inMin, float inMax, float outMin, float outMax) {
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
