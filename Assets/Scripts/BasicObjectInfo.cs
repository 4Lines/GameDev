using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObjectInfo : MonoBehaviour {

	private string name;
	private string description;
	private Sprite icon;

	public BasicObjectInfo(string oName)
	{
		name = oName;
	}

	public BasicObjectInfo(string oName, string oDescription)
	{
		name = oName;
		description = oDescription;
	}

	public BasicObjectInfo(string oName, string oDescription, Sprite oIcon)
	{
		name = oName;
		description = oDescription;
		icon = oIcon;
	}

	public string ObjectName {
		get { return name; }
	}

	public string ObjectDescription
	{
		get { return description; }
	}

	public Sprite ObjectIcon
	{
		get { return icon; }
	}
}
