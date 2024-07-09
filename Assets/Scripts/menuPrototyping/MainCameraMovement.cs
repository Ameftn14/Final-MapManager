using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour {
    // Start is called before the first frame update
    public float speed = 10.0f;
    public GameObject map = null;
    [SerializeField] private Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
    void Start() {
        Debug.Assert(map != null);
    }

    // Update is called once per frame
    void Update() {
        bounds = map.GetComponent<SpriteRenderer>().bounds;
        CameraMovement();
    }

    void CameraMovement() {
        Vector3 position = transform.localPosition;
        float horizontalDeltat = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalDelta = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if (WillNotMoveOutsideTheMapHorizontally(horizontalDeltat)) {
            position += new Vector3(horizontalDeltat, 0, 0);
        }
        if (WillNotMoveOutsideTheMapVertically(verticalDelta)) {
            position += new Vector3(0, verticalDelta, 0);
        }
        transform.localPosition = position;
    }

    bool WillNotMoveOutsideTheMapHorizontally(float delta) {
        Vector3 position = transform.localPosition;
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float mapWidth = bounds.size.x / 2;
        return position.x + halfWidth + delta <= mapWidth && position.x - halfWidth + delta >= -mapWidth;
    }
    bool WillNotMoveOutsideTheMapVertically(float delta) {
        Vector3 position = transform.localPosition;
        float halfHeight = Camera.main.orthographicSize;
        float mapHeight = bounds.size.y / 2;
        return position.y + halfHeight + delta <= mapHeight && position.y - halfHeight + delta >= -mapHeight;
    }
}
