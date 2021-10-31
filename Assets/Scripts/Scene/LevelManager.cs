using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static GameObject enemyPrefab;
    public static GameObject enemyHealthBar;

    public static GameObject player;
    public static GameObject playerAttackPoint;

    public static void TestStaticMethod()
    {
        Debug.Log("testing a static method"); 

        //This method can be called without referencing the object of the class! 
        //You would usually need to:

        //public LevelManager levelManager; //referance class

        //LevelManager levelManager = new LevelManager(); //create instance (object) of class

        //levelManager.TestStaticMethod(); //use method attached to instance

        //Since this method belongs to the CLASS, you can simply use Class.Method();
        //eg. in another script: LevelManager.TestStaticMethod();         
    }
}
