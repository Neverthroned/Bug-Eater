using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
   public float speed = 5;
   public Rigidbody rb;
   void Update()
   {
       MovePlayer();
   }
   void MovePlayer()
   {
       float moveX = Input.GetAxis("Horizontal");
       float moveZ = Input.GetAxis("Vertical");
       Vector3 movement = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
       rb.MovePosition(transform.position + movement);
   }
}