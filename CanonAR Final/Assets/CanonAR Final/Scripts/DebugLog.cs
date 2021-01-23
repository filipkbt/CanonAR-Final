using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLog : MonoBehaviour {

  public static bool show;
	private static Text log;

  void Start () {
    show = false;
    log = gameObject.GetComponent(typeof(Text)) as Text;
  }

  public static void WriteLog (string msg) {
    if(show){
      log.text += "\n";
      log.text += msg;
    }
  }
}
