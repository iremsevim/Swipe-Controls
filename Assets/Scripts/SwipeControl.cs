using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private float minDistance;
    private Vector3 firstPos;
    private Vector3 lastPos;
    private float maxDis;
    private bool canSwipe;

    public Vector3 Result
    {
        get
        {
            return result;
        }
    }
    public Vector3 result;

    void Start()
    {
        minDistance = Screen.height * 20 / 100; //dragDistance is 15% height of the screen
   
        maxDis = minDistance * 0.1f;

    }


    void Update()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:

                if (Input.touchCount > 0)
                {
                    Touch item = Input.GetTouch(0);
                    switch (item.phase)

                    {
                        case TouchPhase.Began:

                            firstPos = item.position;
                            break;
                        case TouchPhase.Moved:

                            float xdist = item.position.x - firstPos.x;
                            float ydist = item.position.y - firstPos.y;
                            if (Mathf.Abs(xdist) > minDistance)
                            {
                                result.x = Mathf.Clamp(xdist / maxDis, -1f, 1f);
                                firstPos = Input.mousePosition;
                            }
                            else if (Mathf.Abs(ydist) > minDistance)
                            {
                                result.y = Mathf.Clamp(ydist / maxDis, -1f, 1f);
                                firstPos = Input.mousePosition;
                            }

                            break;
                        case TouchPhase.Ended:
                            result = Vector2.zero;
                            break;

                    }

                }


                break;


            default:
                if (Input.GetMouseButtonDown(0)) //check for the first touch
                {
                    firstPos = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0)) //update the last position based on where they moved
                {
                    float xdist = Input.mousePosition.x - firstPos.x;
                    float ydist = Input.mousePosition.y - firstPos.y;
                    if (Mathf.Abs(xdist) > minDistance)
                    {
                        result.x = Mathf.Clamp(xdist / maxDis, -1, 1);

                        firstPos = Input.mousePosition;


                    }
                    else if ((Mathf.Abs(ydist) > minDistance))
                    {
                        result.y = Mathf.Clamp(ydist / maxDis, -1, 1);
                        firstPos = Input.mousePosition;
                    }

                }
                else if (Input.GetMouseButtonUp(0)) //check if the finger is removed from the screen
                {
                    result = Vector2.zero;
                }

                break;
        }
    }

}
