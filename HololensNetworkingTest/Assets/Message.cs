using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.IO;

[DataContract() ]
public class Message : System.Object {

	[DataMember()]
	public int commandID;
	[DataMember()]
	public string[] parameters;

}
