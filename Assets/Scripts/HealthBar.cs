using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;
	/// <summary>
	/// Set the max value of the bar to the enemy's health when the enemy instantiated
	/// </summary>
	/// <param name="health">Health of the starting enemy</param>
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		SetHealth(health);
	}
	/// <summary>
	/// Set the current value of the bar to the current enemy's health
	/// </summary>
	/// <param name="health">Current enemy's health</param>
	public void SetHealth(int health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
