//Attach this script to a Rigidbody with a sphere colider and a camera as a child.
//Adjust rigidbody to no gravity, freeze rotation on the X and Z
//Set rigidbody's drag to 2 and angular drag to 1

#pragma strict


var Pitch = 3.0;                                             //the amount to rotate the camera up and down
var Rot = 3.0;                                               //amount to rotate the rigidbody
var Thrust = 5.0;                                            //how much force to apply forward
var Lift = 3.0;                                              //how much up and down force to apply
private var RB: Rigidbody;                                   //the rigidbody that this script is attached to 
var Cam : Transform;                                         //our camera

function Start() 
{
    RB = GetComponent.<Rigidbody>();                         //lets get our rigidbody component var
}

function Update() 
{

    var MY = Input.GetAxis ("Mouse Y") * Pitch;               //our var to control the camera rotation up and down based on our mouse movement
    var MX = Input.GetAxis ("Mouse X") * Rot;                 //our var to control the rigidbody rotation left and right based on our mouse movement
 
    if (Input.GetKey("w"))                                     //if we are pressing the W key...
    {
        RB.AddRelativeForce(Vector3.forward * Thrust);         //add forward force to our rigidbody
    }

    if (Input.GetKey("s"))                                    //if we are pressing the S key...
    {
        RB.AddRelativeForce(Vector3.forward * -Thrust);	      //add opposing force to our rigidbody 
    }
  
    if (Input.GetKey ("a"))                                   //if we are pressing the A key...
    {
        RB.AddRelativeForce(Vector3.left * Thrust);           //add left force to our rigidbody
    }
        
    if (Input.GetKey ("d"))                                  //if we are pressing the D key...
    {
        RB.AddRelativeForce(Vector3.right * Thrust);         //add right force to our rigidbody
    }

    if (Input.GetAxis ("Mouse X"))                           //if our mouse moves left or right...
    {
        transform.Rotate(0, MX, 0);	                         //rotate our rigidbody by our var amount on the Y axis
    }

    if (Input.GetAxis ("Mouse Y"))                           //if our mouse moves up and down...
    {
        Cam.Rotate(-MY, 0, 0);	                             //rotate the camera by our var amount on the X axis
    }
 
    if (Input.GetButton("Fire1"))                            //declare what happens on left mouse click here
    {
        
    }
        
    if (Input.GetKey ("space"))                               //if we are pressing space bar...
    {
        RB.AddRelativeForce(Vector3.up * Lift);              //add upwards force to our rigidbody
    }

    if (Input.GetKey ("left shift"))                          //if we are pressing the left shift...
    {
        RB.AddRelativeForce(Vector3.down * Lift);             //add downwards force to our rigidbody
    }

}

function Freeze()                                            //function not called
{
    RB.velocity = Vector3.zero;                              //this will stop our rigidbody's movement
}