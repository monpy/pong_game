using UnityEngine;
using System.Collections;

public class MoveRacket : MonoBehaviour {

	// up and down keys (to be set in the Inspector)
	public KeyCode up;
	public KeyCode down;
	public float speed = 0.01f;

	// Use this for initialization
	void FixedUpdate () {
		if(Input.GetKey(up)){
			transform.Translate(Vector2.up * speed);
		}

		if(Input.GetKey(down)){
			transform.Translate(Vector2.up * -speed);
		}
	}

	void RemoveOwn(){
		Destroy(gameObject);
	}
}
