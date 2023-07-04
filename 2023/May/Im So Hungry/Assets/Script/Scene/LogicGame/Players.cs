using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public Stomach stomach;
    public Animator Ani;
    public Joystick joystick;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = joystick.Horizontal;
        float yMovement = joystick.Vertical;
        transform.position += new Vector3(xMovement, yMovement, 0f) * speed * 2f * Time.deltaTime;
        if (xMovement == 0)
        {
            CheckAni(0, false);
            stomach.isCheck = false;
        }
        else if (xMovement != 0)
        {
            stomach.isCheck = true;
            CheckAni(0, true);
        }

        if (xMovement > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xMovement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (stomach.min == 0)
        {
            speed = 0;
            joystick.gameObject.SetActive(false);
            CheckAni(0, false);
            CheckAni(1, true);
        }
    }

    public void CheckAni(int index, bool bl)
    {
        switch (index)
        {
            case 0:
                Ani.SetBool("PlayerRun", bl);
                break;
            case 1:
                Ani.SetBool("PlayerDead", bl);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Animals" && speed != 0)
        {
            GameScene.game.ShowEffect(3, other.transform.position);
            GameScene.game.checkCoin(0);
            stomach.CheckUpFull();
            Destroy(other.gameObject);
        }
    }
}
