using UnityEngine;
using System.Collections;

public class PowerUpFactory : MonoBehaviour {

	public static PowerUpFactory Instance;
	public float radius;
	public GameObject foodPrefab;
	public GameObject addHPPrefab;
	public	int	foodNum;
	public	int	addHPNum;

	void Awake()
	{
		Instance = this;
	}

	public void GenerateFood()
	{
		if(foodNum < 5) {
			Instantiate(foodPrefab, Random.insideUnitCircle * radius, Quaternion.identity);
			foodNum++;
		}
	}
	
	public void GenerateAddHP()
	{
		if(foodNum < 5) {
			Instantiate(addHPPrefab, Random.insideUnitCircle * radius, Quaternion.identity);
			addHPNum++;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(Vector3.zero, radius);
	}
}
