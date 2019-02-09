using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	RectTransform thisRT;

    Vector3 originalSize;
    Vector3 hoverSize;

	// Use this for initialization
	void Start () {
		thisRT = GetComponent<RectTransform>();
        originalSize = thisRT.localScale;
        hoverSize = new Vector3(originalSize.x * 1.1f, originalSize.y * 1.1f, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
        thisRT.localScale = hoverSize;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        thisRT.localScale = originalSize;
    }
}
