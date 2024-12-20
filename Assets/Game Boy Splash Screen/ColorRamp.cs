using UnityEngine;
using System.Collections;

public class ColorRamp : MonoBehaviour {

	public float speed = 375f;

	private RectTransform rt;
	private float movement;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (rt.anchoredPosition.x <= 200) {
			movement = -speed * Time.deltaTime;
			rt.localPosition = new Vector3 (rt.localPosition.x - movement, rt.localPosition.y, rt.localPosition.z);
		}

		if (rt.anchoredPosition.x >= 120 && rt.anchoredPosition.x <= 130) {
			
		}
	}
}
