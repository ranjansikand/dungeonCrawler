using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{

	[SerializeField] GameObject detectionMark;
    [SerializeField] Vector3 heightOffset;

	bool playerFound = false;

	public override void Update()
	{
		base.Update();

		if (player != null && playerFound == false)
		{
			playerFound = true;

            Instantiate(detectionMark, transform.position + heightOffset, Quaternion.identity);

		}
	}
}
