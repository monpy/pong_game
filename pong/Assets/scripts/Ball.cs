using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float first_speed = 2.0f;
	private float speed;

	// Use this for initialization
	void Start () {
		speed = first_speed;
		rigidbody2D.velocity = Vector2.one.normalized * speed;

	}
	
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "RacketLeft"){
			//left racket
			float y = hitFactor(transform.position, col.transform.position, ((BoxCollider2D)col.collider).size.y);
			Vector2 dir = new Vector2(1, y).normalized;
			rigidbody2D.velocity = dir * speed;
			speed *= 1.01f;
		}else if(col.gameObject.name == "RacketRight"){
			//right racket
			float y = hitFactor(transform.position, col.transform.position, ((BoxCollider2D)col.collider).size.y);
			Vector2 dir = new Vector2(-1, y).normalized;
			rigidbody2D.velocity = dir * speed;
			speed *= 1.02f;
		}else if(col.gameObject.name == "WallRight" || col.gameObject.name == "WallLeft"){
			// restart
//			Debug.Log("hit");
			speed = first_speed;
			rigidbody2D.velocity = Vector2.one.normalized * speed;
			transform.localPosition = new Vector2(0,0);
		}else{
			speed *= 1.1f;
		}
	}

	void Update(){
//		Debug.Log(rigidbody2D.velocity.y);
	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
	                float racketHeight) {
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.y - racketPos.y) / racketHeight;
	}
}
