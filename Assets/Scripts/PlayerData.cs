using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [HideInInspector] public float gravityStrength;
    [HideInInspector] public float gravityScale;
    
    public float fallGravityMult;
    public float maxFallSpeed;
    public float fastFallGravityMult;
    public float maxFastFallSpeed;
    
    public float runMaxSpeed;
    public float runAcceleration;
    [HideInInspector] public float runAccelAmount;
    public float runDecceleration;
    [HideInInspector] public float runDeccelAmount;
    
    [Range(0f, 1)] public float accelInAir;
    [Range(0f, 1)] public float deccelInAir;
    
    public bool doConserveMomentum = true;
    
    public float jumpHeight;
    public float jumpTimeToApex;
    [HideInInspector] public float jumpForce;
    
    public float jumpCutGravityMult;
    [Range(0f, 1)] public float jumpHangGravityMult;
    public float jumpHangTimeThreshold;
    
    public float jumpHangAccelerationMult;
    public float jumpHangMaxSpeedMult;
    
    [Range(0.01f, 0.5f)] public float coyoteTime;
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;

    private void OnValidate()
    {
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        gravityScale = gravityStrength / Physics2D.gravity.y;

        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
    }
}
