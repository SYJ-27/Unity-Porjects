using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    public static Parallax instance;
    public bool isMove;
    private float speed;
    private float offset;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        image = GetComponent<Image>();
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            offset -= Time.deltaTime * 0.5f;
            image.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
            image.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
    }
}
