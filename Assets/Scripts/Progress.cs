using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Progress : MonoBehaviour {
	private Image foregroundImage;
	public int _Value {
		get {
			if(foregroundImage != null)
				return (int)(foregroundImage.fillAmount*100);	
			else
				return 0;	
		} set {
			if(foregroundImage != null)
				foregroundImage.fillAmount = value/100f;	
		} 
	}

	void Start () {
		foregroundImage = gameObject.GetComponent<Image>();		
		_Value = 0;
	}	

	public void SetValue(float val) {
		_Value = (int)(val*100);
	}

	public float GetValue() {
		return (float)(((float)_Value)/100.0);
	}
}