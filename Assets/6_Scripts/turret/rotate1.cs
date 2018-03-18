using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate1 : MonoBehaviour {
    public int vitesse;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(vitesse, 0, 0 * Time.deltaTime); //rotate de x degrés par seconde
    }
}
