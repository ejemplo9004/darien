using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LauncherController : MonoBehaviour
{
    #region References

    [Header("Launcher")] 
    [SerializeField] private GameObject launcherBase;
    [SerializeField] private float speedRelation;
    [SerializeField] private GameObject launcher;
    [SerializeField] private ThrowPredictor predictor;

    [Header("InmigrantsCounter")] 
    [SerializeField] private List<GameObject> inmigrants;

    [Header("Wheel")] 
    [SerializeField] private GameObject wheel;
    [SerializeField] private Transform pivot;
    [SerializeField] private LayerMask wheelLayer;
    private Vector3 startClic;
    private Vector3 relativeMousePosition;
    public float angle;
    private bool wheelMoving;

    [Header("HeightLever")] 
    [SerializeField] private Transform lever;
    [SerializeField] private LayerMask leverLayer;
    [SerializeField] private float heightMultiplier = 1;
    [SerializeField] private float leverSensivity = 1;
    private float startRotation = 0;
    private bool leverMoving;
    private float leverDistance;

    [Header("ForceSlider")] 
    [SerializeField] private Transform slider;
    [SerializeField] private Transform top, bottom;
    [SerializeField] private LayerMask sliderMask;
    [SerializeField] private float forceMultiplier = 1;
    [SerializeField] private float sliderMultiplier = 1;
    private bool sliderMoving;
    private float sliderDistance;
    private Vector3 startpos;
    public float force;

    [Header("LaunchButton")] 
    [SerializeField] private GameObject launchButton;
    [SerializeField] private LayerMask buttonMask;
    [SerializeField] private UnityEvent buttonAction;

    [Header("ChairLaunch")] 
    [SerializeField] private Transform chair;
    [SerializeField] private Transform front;
    [SerializeField] private Transform back;
    [SerializeField] private float launchTime = 1;
    [SerializeField] private float backTime = 10;
    private bool moveLock;
    private bool buttonPressed;

    private Camera _camera;
    private bool mouseUp, mouseDown, mouseHold;
    private bool raycasted;

    #endregion
    private IEnumerator Start()
    {
        _camera = Camera.main;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(predictor.DrawLine());
    }

    private void Update()
    {
        predictor.UpdateParameters(front.position,
            force * forceMultiplier * ControlGeneral.singleton.GetInmigrante().transform.forward, new Vector3(0, -9.8f, 0));
        raycasted = false;
        CheckMouse();
        MoveLever();
        MoveSlider();
        RotateWheel();
        Launch();
    }

    private void Launch()
    {
        if (mouseDown)
        {
            if (!CheckClic(buttonMask)) return;
            buttonPressed = true;
            launchButton.transform.localScale = new Vector3(1,1,0.5f);
        }

        if (mouseUp && buttonPressed)
        {
            buttonPressed = false;
            launchButton.transform.localScale = Vector3.one;
            if (!CheckClic(buttonMask)) return;
            if (!moveLock)
            {
                buttonAction.Invoke();
                StartCoroutine(ExecuteLaunch());
            }
        }
    }

    private IEnumerator ExecuteLaunch()
    {
        moveLock = true;
        float timePassed = 0;
        while (timePassed <= launchTime)
        {
            chair.position = Vector3.Lerp(back.position, front.position, timePassed / launchTime);
            timePassed += Time.deltaTime;
            yield return null;
        }

        chair.position = front.position;
        ControlGeneral.singleton.GetCampamento().Desemparentar();
        ThrowablePerson person = ControlGeneral.singleton.GetInmigrante().persona;
        person.StartAirTrip(force * forceMultiplier * person.transform.forward, Vector3.zero);
        predictor.StopDrawing(true);
        yield return new WaitForSeconds(1);

        timePassed = 0;
        while (timePassed <= backTime)
        {
            chair.position = Vector3.Lerp(front.position, back.position, timePassed / (backTime));
            timePassed += Time.deltaTime;
            yield return null;
        }
        chair.position = back.position;

        moveLock = false;
    }

    private void CheckMouse()
    {
        mouseUp = Input.GetMouseButtonUp(0);
        mouseDown = Input.GetMouseButtonDown(0);
        mouseHold = Input.GetMouseButton(0);
    }

    private void MoveSlider()
    {
        Vector3 sliderPos;
        Vector3 sliderField = top.position - bottom.position;
        Vector3 screenDist = _camera.WorldToScreenPoint(top.position) - _camera.WorldToScreenPoint(bottom.position);
        if (mouseDown)
        {
            if (!CheckClic(sliderMask)) return;
            sliderMoving = true;
            startpos = slider.position;
            Vector3 mousePos = Input.mousePosition;
            sliderPos = _camera.WorldToScreenPoint(slider.position);
            startClic = mousePos - sliderPos;
        }

        if (sliderMoving && mouseHold)
        {
            Vector3 mousePos = Input.mousePosition;
            sliderPos = _camera.WorldToScreenPoint(startpos);
            relativeMousePosition = mousePos - sliderPos;
            sliderDistance = relativeMousePosition.y - startClic.y;
            float relativeMove = Mathf.Abs(sliderDistance / screenDist.magnitude) * sliderMultiplier;
            if (sliderDistance > 0)
            {
                slider.position = Vector3.MoveTowards(slider.position, top.position, Time.deltaTime * relativeMove);
            }
            else if (sliderDistance < 0)
            {
                slider.position = Vector3.MoveTowards(slider.position, bottom.position, Time.deltaTime * relativeMove);
            }

            startClic = mousePos - sliderPos;
        }


        if (sliderMoving && mouseUp)
        {
            sliderMoving = false;
        }

        force = (slider.position - bottom.position).magnitude / sliderField.magnitude;

    }

    private void MoveLever()
    {
        Vector3 leverPos;
        if (mouseDown)
        {
            if (!CheckClic(leverLayer)) return;
            leverMoving = true;
            Vector3 mousePos = Input.mousePosition;
            leverPos = _camera.WorldToScreenPoint(lever.position);
            startClic = mousePos - leverPos;
        }

        if (leverMoving && mouseHold)
        {
            Vector3 mousePos = Input.mousePosition;
            leverPos = _camera.WorldToScreenPoint(lever.position);
            relativeMousePosition = mousePos - leverPos;
            leverDistance = (relativeMousePosition.y - startClic.y) * leverSensivity;
            startRotation += leverDistance;
            startClic = relativeMousePosition;
            if (startRotation < 0 || startRotation > 80)
            {
                startRotation -= leverDistance;
                return;
            }

            lever.Rotate(-leverDistance, 0, 0);
            launcher.transform.Rotate(leverDistance * heightMultiplier, 0, 0);
        }

        if (leverMoving && mouseUp)
        {
            leverMoving = false;
        }
    }

    private void RotateWheel()
    {
        Vector3 wheelPos;
        if (mouseDown)
        {
            if (!CheckClic(wheelLayer)) return;
            wheelMoving = true;
            Vector3 mousePos = Input.mousePosition;
            wheelPos = _camera.WorldToScreenPoint(pivot.position);
            startClic = mousePos - wheelPos;
        }

        if (wheelMoving && mouseHold)
        {
            Vector3 mousePos = Input.mousePosition;
            wheelPos = _camera.WorldToScreenPoint(pivot.position);
            relativeMousePosition = mousePos - wheelPos;
            angle = Vector3.Angle(startClic, relativeMousePosition);
            Vector3 cross = Vector3.Cross(startClic, relativeMousePosition);
            if (cross.z < 0)
            {
                angle = -angle;
            }

            float actualAngle = launcherBase.transform.eulerAngles.y;
            if (!((actualAngle < 120 && angle > 0) || (actualAngle > 240 && angle < 0)))
            {
                wheel.transform.Rotate(0, 0, -angle);
                launcherBase.transform.Rotate(0, 0, -angle / speedRelation);
            }
            startClic = relativeMousePosition;
        }

        if (wheelMoving && mouseUp)
        {
            wheelMoving = false;
        }
    }

    private bool CheckClic(LayerMask mask)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10, mask))
        {
            if (!raycasted)
            {
                raycasted = true;
                return true;
            }
        }

        return false;
    }

    public void RandomizePositions()
    {
        //Slider
        Vector3 sliderField = top.position - bottom.position;
        float rnd = Random.Range(0f, 1f);
        slider.position = Vector3.MoveTowards(bottom.position, top.position, sliderField.magnitude * rnd);
        
        //Lever
        lever.localRotation = new Quaternion(0.4f, 0f, 0f, 0.9f);
        startRotation = Random.Range(0f, 80f);
        launcher.transform.localRotation = Quaternion.identity;
        lever.Rotate(-startRotation, 0, 0);
        launcher.transform.Rotate(startRotation * heightMultiplier, 0, 0);
        
        //Wheel
        float rangle = Random.Range(0, 100);
        launcherBase.transform.localRotation = new Quaternion(0f, 0f, 0f, 1f);
        launcherBase.transform.Rotate(0, 0, rangle-60);
    }

    public void UpdateCounter(int n)
    {
        foreach (var inmigrant in inmigrants)
        {
            inmigrant.SetActive(n > 0);
            n--;
        }
    }
}