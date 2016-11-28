#pragma strict
import UnityEngine.SceneManagement;                                            //we need to import our scene management functions


function Update () 
{
    if(Input.GetKeyDown("r"))                                                  //pressing the R key...
    {	
        Restart();                                                             //...will call the restart function
    }
}

function Restart ()                                                            //call the restart function
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);                //access our scene manager and reload our current scene
}