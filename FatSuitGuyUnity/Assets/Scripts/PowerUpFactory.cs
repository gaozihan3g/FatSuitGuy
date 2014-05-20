using UnityEngine;
using System.Collections;

public class PowerUpFactory : MonoBehaviour {

	public static PowerUpFactory Instance;
	public float radius;
	public GameObject foodPrefab;

	void Awake()
	{
		Instance = this;
	}

	public void GenerateFood()
	{
		Instantiate(foodPrefab, Random.insideUnitCircle * radius, Quaternion.identity);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(Vector3.zero, radius);
	}
}
