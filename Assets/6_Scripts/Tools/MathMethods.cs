using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
	public class MathMethods
	{

		public static float GetPercent(float value, float inMin, float inMax, float outMin, float outMax)
		{
			return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		}
	}
}
