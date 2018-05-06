using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManagement : MonoBehaviour {

    static private int goldAmount = 0;
    public Text textGold;
	static public void AddGold(int add)
    {
        goldAmount += add;
    }
    private void Update()
    {
        textGold.text = goldAmount.ToString();
    }
}
