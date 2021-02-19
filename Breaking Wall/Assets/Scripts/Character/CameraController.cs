using UnityEngine;


[RequireComponent(typeof(PlayerController))]
public class CameraController : InputComponent
{
    static CameraController instance;
    public Transform cfTransform;
    Vector2 aim;

    float lerpFactor = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning("More than one Camera Controller in Scene : " + name + " and " + instance.name);
        }

    }

    [Header("Sensibility")]
    [Range(0.1f, 2)]
    public float verticalSens;

    [Range(0.1f, 2)]
    public float horizontalSens;

    [Header("GamePad Sensibility")]
    [Range(1, 100)]
    public float padVerticalSens;

    [Range(1, 100)]
    public float padHorizontalSens;


    public override void setPlayerControls(PlayerControls inputs)
    {
        inputs.DefaultActionMap.Aim.performed += ctx => {
            aim = ctx.ReadValue<Vector2>();

            aim.x *= horizontalSens;
            aim.y *= verticalSens;
        };

        inputs.DefaultActionMap.PadAim.performed += ctx =>
        {
            aim = ctx.ReadValue<Vector2>();
            
            aim.x *= padHorizontalSens;
            aim.y *= padVerticalSens;
        };

    }

    private void FixedUpdate()
    {
        updateCamera();
    }

    void updateCamera()
    {
        if (aim != Vector2.zero)
        {
            if (cfTransform != null)
            {
                Quaternion nextRotation = cfTransform.rotation;
                nextRotation *= Quaternion.AngleAxis(aim.y * verticalSens, Vector3.right);
                nextRotation *= Quaternion.AngleAxis(aim.x * verticalSens, Vector3.up);




                cfTransform.rotation = Quaternion.Lerp(cfTransform.rotation, nextRotation, Time.deltaTime * lerpFactor);


                var aux = cfTransform.eulerAngles;
                aux.z = 0;
                //                aux.z = 0;

                if (aux.x > 180 && aux.x < 340)
                {
                    aux.x = 340;
                }
                else if (aux.x < 180 && aux.x > 40)
                {
                    aux.x = 40;
                }

                //aux.x = Mathf.Clamp(aux.x, clampingMin, clampingMax);

                cfTransform.eulerAngles = aux;



            }

            aim = Vector2.zero;
        }
    }

    public static void LockCamera()
    {
        instance.enabled = false;
    }

    public static void FreeCamera()
    {
        instance.enabled = true;
    }
}
