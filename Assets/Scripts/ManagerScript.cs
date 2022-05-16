using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerScript : MonoBehaviour
{

  public Animator Chest;
    

  public void onEnd(){
      Chest.enabled = true;
  }

}
