using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoScene : MonoBehaviour {

	//public Animator animator;
	public Text text;
	//public Animator animator;

	private int _index = 0;

	Vector3 startPos = new Vector3 (0, 0, 0);
	Quaternion startRot = new Quaternion (0, 0, 0, 1);

	// Use this for initialization
	void Start () {

		ChangeAnimation();
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			_index -= 1;
			if (_index < 0)
			{
				_index = 44;
			}
			ChangeAnimation();
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			_index += 1;
			if (_index > 44)
			{
				_index = 0;
			}
			ChangeAnimation();
		}

		
	}

	void ChangeAnimation ()
	{
		

		Animator animator1 = GameObject.Find("clothingSet_01").GetComponent<Animator>();
		animator1.SetInteger("index", _index);

		GameObject.Find ("clothingSet_01").transform.position = startPos;
		GameObject.Find ("clothingSet_01").transform.rotation = startRot;

		string name = "";
		switch (_index)
		{
			case 0:
				name = "idle";
				break;
			case 1:
				name = "walk";
				break;
			case 2:
				name = "run";
				break;
			case 3:
				name = "getHit";
				break;
			case 4:
				name = "death";
				break;
			case 5:
				name = "bow_attack";
				break;
			case 6:
				name = "bow_death";
				break;
			case 7:
				name = "bow_get";
				break;
			case 8:
				name = "bow_idle";
				break;
			case 9:
				name = "bow_run";
				break;
			case 10:
				name = "fists_attack";
				break;
			case 11:
				name = "fists_death";
				break;
			case 12:
				name = "fists_get";
				break;
			case 13:
				name = "fists_idle";
				break;
			case 14:
				name = "oneHandedSword_attack";
				break;
			case 15:
				name = "oneHandedSword_death";
				break;
			case 16:
				name = "oneHandedSword_get";
				break;
			case 17:
				name = "oneHandedSword_idle";
				break;
			case 18:
				name = "shield_attack_fists";
				break;
			case 19:
				name = "shield_attack_oneHandedSword";
				break;
			case 20:
				name = "shield_attack_stick";
				break;
			case 21:
				name = "shield_death_fists";
				break;
			case 22:
				name = "shield_death_oneHandedSword";
				break;
			case 23:
				name = "shield_death_stick";
				break;
			case 24:
				name = "shield_get_fists";
				break;
			case 25:
				name = "shield_get_oneHandedSword";
				break;
			case 26:
				name = "shield_get_stick";
				break;
			case 27:
				name = "shield_idle_fists";
				break;
			case 28:
				name = "shield_idle_oneHandedSword";
				break;
			case 29:
				name = "shield_idle_stick";
				break;
			case 30:
				name = "shield_run";
				break;
			case 31:
				name = "stick_attack";
				break;
			case 32:
				name = "stick_death";
				break;
			case 33:
				name = "stick_get";
				break;
			case 34:
				name = "stick_idle";
				break;
			case 35:
				name = "twoHandedAxe_attack";
				break;
			case 36:
				name = "twoHandedAxe_death";
				break;
			case 37:
				name = "twoHandedAxe_get";
				break;
			case 38:
				name = "twoHandedAxe_idle";
				break;
			case 39:
				name = "twoHandedAxe_run";
				break;
			case 40:
				name = "twoHandedStaff_attack";
				break;
			case 41:
				name = "twoHandedStaff_death";
				break;
			case 42:
				name = "twoHandedStaff_get";
				break;
			case 43:
				name = "twoHandedStaff_idle";
				break;
			case 44:
				name = "twoHandedStaff_run";
				break;

		}

		text.text = string.Concat(_index.ToString(), ". ", name);
	}
}
