﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArrayLayout  {

	[System.Serializable]
	public struct rowData{
		public room[] row;
	}

	public rowData[] rows = new rowData[2];

}
