﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCmd : MonoBehaviour, ICommandCall
{
	public IEnumerator Command(List<string> args)
	{
        float waitTime = float.Parse(args[0]);
        yield return new WaitForSeconds(waitTime);
    }
}
