using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandCall
{
    IEnumerator Command(List<string> args);
}
