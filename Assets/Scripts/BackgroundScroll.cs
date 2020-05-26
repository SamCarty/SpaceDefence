using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
    [SerializeField] float scrollSpeedHorizontal = 0.4f;
    [SerializeField] float scrollSpeedVertical = 0.4f;
    Material material;
    Vector2 offset;
    
    // Start is called before the first frame update
    void Start() {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(scrollSpeedHorizontal, scrollSpeedVertical);
    }

    // Update is called once per frame
    void Update() {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
