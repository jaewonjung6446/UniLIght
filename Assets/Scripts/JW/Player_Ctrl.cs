using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CharacterController Con;
    public float movSpd;
    public float jumpPower;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        Con = GetComponent<CharacterController>();
        rb.freezeRotation = true;
    }
    public void Update()
    {
        Mov();
        //Jump();
    }
    public void Mov()
    {
        float movH = Input.GetAxisRaw("Horizontal");
        float movV = Input.GetAxisRaw("Vertical");
        Vector3 mov = new Vector3(movH, 0, movV);
        Con.Move(transform.TransformDirection(mov) * movSpd * Time.deltaTime);
    }
    /*public void Jump()
    {
        if (Input.GetAxisRaw("Jump") != 0)
        {
            Debug.Log("점프인식");
            this.rb.AddForce(0, jumpPower, 0);
        }
    }*/
}
