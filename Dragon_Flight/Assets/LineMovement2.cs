using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMovement2 : MonoBehaviour
{
	GameObject target;
	public GameObject meteor;

	public float speed = 0f;
	private float timer = 0f;

	void Start()
	{
		//�÷��̾��� ��ġ�� target�� ����
		target = GameObject.Find("Player");
	}

	void Update()
	{
		//3�� �� new Vector3(transform.position.x, 6, 0)�� meteor ����
		timer += Time.deltaTime;
		if (timer >= 3f)
		{
			Instantiate(meteor, new Vector3(transform.position.x, 6, 0), Quaternion.identity);
			timer = 0f;
			Destroy(gameObject);
		}
	}
}