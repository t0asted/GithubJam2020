using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;

		void Start () 
		{
			controller = GetComponent <CharController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		}

		void Update ()
		{
			if (Input.GetKey ("w")|| Input.GetKey("a")|| Input.GetKey("s")|| Input.GetKey("d")) 
			{
				anim.SetInteger ("AnimationPar", 1);
			}
			else 
			{
				anim.SetInteger ("AnimationPar", 0);
			}

		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log("space");
			bool playerFloat = anim.GetBool("FloatingPar");
            if (playerFloat == false) 
			{
				anim.SetBool("FloatingPar", true);
			}
			else
			{
				anim.SetBool("FloatingPar", false);
			}
		}
		else
		{
			anim.SetBool("FloatingPar", false);
		}
	}
}
