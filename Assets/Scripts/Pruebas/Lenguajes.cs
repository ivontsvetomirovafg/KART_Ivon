using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lenguajes", menuName = "Scriptable Objects/Lenguajes")]
public class Lenguajes : ScriptableObject
{
    public List<string> keys = new List<string>();
    public List<string> values = new List<string>();
}
