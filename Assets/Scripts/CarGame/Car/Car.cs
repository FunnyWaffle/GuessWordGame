using Assets.Scripts.Car;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Car : MonoBehaviour
{
    [SerializeField] private float _maxSpeenInKMH = 200;
    [SerializeField] private float _timeToReachMaxSpeed = 10f;
    [SerializeField] private float _brakePower = 10000f;
    [SerializeField] private float _steeringSpeed = 2f;
    [SerializeField] private DriveType Drive;
    private enum DriveType { WheelDrive, FrontWheelDrive, RearWheelDrive }

    [SerializeField] private Rigidbody _carBody;

    [SerializeField] private List<Wheel> _frontWheels;
    [SerializeField] private List<Wheel> _rearWheels;

    [SerializeField] private LightsGroup _frontLights;
    [SerializeField] private LightsGroup _reverseLights;
    [SerializeField] private LightsGroup _leftIndicators;
    [SerializeField] private LightsGroup _rightIndicators;
    [SerializeField] private LightsGroup _brakeLights;

    [SerializeField] private List<VisualEffect> _smokes;

    private float _motorPower;
    private float _currentVelocityKMH;
    private float _moveDirectionSign;

    private void Start()
    {
        if (_carBody == null)
            _carBody = gameObject.GetComponent<Rigidbody>();

        //foreach (var smoke in _smokes)
        //{
        //    smoke.enabled = true;
        //}

        ComputeMotorSpecification();
        DisableLights();
    }
    private void ComputeMotorSpecification()
    {
        var maxSpeedMS = _maxSpeenInKMH / 3.6f;
        _motorPower = _carBody.mass * (maxSpeedMS / _timeToReachMaxSpeed);
    }
    private void Update()
    {
        var carInput = Assets.Scripts.Input.Input._inputActions.Car;

        var torqueInput = carInput.Torque.ReadValue<float>();
        Debug.Log($"Torque Input: {torqueInput}.");

        var steerInput = carInput.Steering.ReadValue<float>();
        Debug.Log($"Steer Input: {steerInput}.");

        var wasBrakePressed = carInput.Brake.IsPressed();
        Debug.Log($"Brake Input: {wasBrakePressed}.");

        UpdateCurrentSpeed();
        var wasReachedMaxSpeed = _currentVelocityKMH >= _maxSpeenInKMH;

        UpdateWheels(torqueInput, steerInput, wasBrakePressed, wasReachedMaxSpeed);

        UpdateModels();

        DisableLights();
        TurnOnLights(steerInput, wasBrakePressed);
        //SetSmokesActivity(wasBrakePressed);
    }
    private void UpdateCurrentSpeed()
    {
        var forwardVelocityThisFrame = Vector3.Dot(_carBody.linearVelocity, transform.forward);

        if (Mathf.Abs(forwardVelocityThisFrame) < 0.01f)
            forwardVelocityThisFrame = 0f;

        var forwardVelocityThisFrameKMH = forwardVelocityThisFrame * 3.6f;

        _currentVelocityKMH = forwardVelocityThisFrameKMH;
        LogDirectionChange();

        Debug.Log($"Speed: {Mathf.Abs(_currentVelocityKMH):0.#} KM/H");
        Debug.DrawLine(transform.position, transform.position + Vector3.Normalize(_carBody.linearVelocity) * 15f, Color.red);
    }
    private void LogDirectionChange()
    {
        if (_currentVelocityKMH == 0)
            return;

        if (Mathf.Abs(_currentVelocityKMH) < 0.5f)
            return;

        var thisFrameDirectionSign = Mathf.Sign(_currentVelocityKMH);
        if (_moveDirectionSign != thisFrameDirectionSign)
        {
            _moveDirectionSign = thisFrameDirectionSign;
            Debug.Log($"Direction has changed to {(thisFrameDirectionSign == 1 ? "forward" : "backward")}");
        }
    }
    private void UpdateWheels(float powerInput, float steerInput, bool wasBrakePressed, bool wasReachedMaxSpeed)
    {
        foreach (var wheel in _frontWheels)
        {
            if (!wasReachedMaxSpeed)
            {
                if (Drive != DriveType.RearWheelDrive)
                    wheel.Torque(powerInput * _motorPower);
            }
            else
                wheel.Torque(0f);

            wheel.Brake(wasBrakePressed ? _brakePower : 0f);

            RotateWheel(steerInput, wheel);
        }
        foreach (var wheel in _rearWheels)
        {
            if (!wasReachedMaxSpeed)
            {
                if (Drive != DriveType.FrontWheelDrive)
                    wheel.Torque(powerInput * _motorPower);
            }
            else
                wheel.Torque(0f);

            wheel.Brake(wasBrakePressed ? _brakePower : 0f);
        }
    }
    private void RotateWheel(float angle, Wheel wheel)
    {
        var maxSteeringAngle = 45f;
        wheel.Steer(Mathf.MoveTowards(wheel.TotalSteerAngle, angle * maxSteeringAngle, _steeringSpeed * Time.deltaTime));
    }
    private void UpdateModels()
    {
        var wheelsCount = _frontWheels.Count;
        for (int i = 0; i < wheelsCount; i++)
        {
            _frontWheels[i].RotateModelAfterCollider();
            _rearWheels[i].RotateModelAfterCollider();
        }
    }
    private void DisableLights()
    {
        _frontLights.SetActiveLights(false);
        _reverseLights.SetActiveLights(false);
        _leftIndicators.SetActiveLights(false);
        _rightIndicators.SetActiveLights(false);
        _brakeLights.SetActiveLights(false);
    }
    private void TurnOnLights(float steerAngle, bool wasBrakePressed)
    {
        if (_currentVelocityKMH > 0)
        {
            _frontLights.SetActiveLights(true);
        }
        else if (_currentVelocityKMH < 0)
        {
            _reverseLights.SetActiveLights(true);
        }

        if (steerAngle < 0)
        {
            _leftIndicators.SetActiveLights(true);
        }
        else if (steerAngle > 0)
        {
            _rightIndicators.SetActiveLights(true);
        }

        if (wasBrakePressed)
        {
            _brakeLights.SetActiveLights(true);
        }
    }
    private void SetSmokesActivity(bool wasBrakePressed)
    {
        var wheelCount = _frontWheels.Count;
        for (int i = 0; i < wheelCount; i++)
        {
            var wheel = _frontWheels[i];
            if (wheel.GetGroundHit(out var hit))
            {
                var slip = Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);

                if (slip > 0.5f * hit.force / wheel.Radius)
                {
                    var minSpawnRate = 75f;
                    var maxSpawnRate = 125f;
                    var currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
                    foreach (var smoke in _smokes)
                    {
                        smoke.SetFloat("SpawnRate", currentSpawnRate);
                    }
                }
                else
                {
                    foreach (var smoke in _smokes)
                    {
                        smoke.SetFloat("SpawnRate", 0f);
                    }
                }
            }
        }

        //var currentSpeedPercent = Mathf.Clamp01(_currentVelocityKMH / _maxSpeenInKMH);
        //if ((wasBrakePressed && _currentVelocityKMH != 0f)
        //    || (currentSpeedPercent < 0.1 && currentSpeedPercent != 0))
        //{

        //}
    }
}
