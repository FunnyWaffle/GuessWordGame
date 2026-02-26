using System.Collections.Generic;
using UnityEngine;

public class Semaphor : MonoBehaviour
{
    public List<GameObject> TrafficLightSections;
    public GameObject CurrentTrafficLightSection;

    public readonly List<Light> _lights = new();
    private Light _currentLight;

    private readonly List<MeshRenderer> _renderers = new();
    private MeshRenderer _currentMeshRenderer;

    public List<Material> Materials = new();
    private MaterialPropertyBlock _currentMaterial;

    private readonly List<Color> _baseColors = new();

    public float LightSwitchTime = 2f;

    private int _currentLightIndex;
    private float _lastLightSwitchTime;
    private int _switchLightsDirectionSing = 1;

    private void Awake()
    {
        _currentMaterial = new();
        Materials.Clear();
    }

    void Start()
    {
        var lightsCount = TrafficLightSections.Count;

        if (TrafficLightSections == null && lightsCount == 0) return;
        GetSectionParts();

        if (CurrentTrafficLightSection == null)
        {
            GetRandomFirstSection(lightsCount);
        }
        else
        {
            _currentLightIndex = TrafficLightSections.IndexOf(CurrentTrafficLightSection);
        }
    }

    void Update()
    {
        UpdateMaterials();

        if (TrafficLightSections == null) return;

        var lightsCount = TrafficLightSections.Count;
        if (lightsCount == 0) return;

        if (Time.time - _lastLightSwitchTime < LightSwitchTime) return;

        _currentLightIndex += _switchLightsDirectionSing;
        UpdateSwitchLightsDirectionSing(lightsCount);

        _currentLight.enabled = false;
        EnableCurrentLight();
    }

    private void GetSectionParts()
    {
        foreach (var lightSection in TrafficLightSections)
        {
            var sectionTransform = lightSection.transform;
            var light = GetSectionLight(sectionTransform);
            if (light == null) Debug.LogError($"{sectionTransform.name} doesn't have any lights.");
            _lights.Add(light);

            _currentMeshRenderer = GetMeshRenderer(sectionTransform);
            _renderers.Add(_currentMeshRenderer);

            var material = _currentMeshRenderer.material;
            Materials.Add(material);

            _baseColors.Add(material.color);
            SetActiveLampMaterialEmmision(_currentMeshRenderer, false);
        }
    }

    private Light GetSectionLight(Transform sectionTransform)
    {
        Light light = null;
        foreach (Transform child in sectionTransform)
        {
            if (child.name == "Light")
            {
                light = child.GetComponent<Light>();

                light.enabled = false;
            }
            else
            {
                light = GetSectionLight(child);
            }
            if (light != null) break;
        }
        return light;
    }

    private MeshRenderer GetMeshRenderer(Transform sectionTransform)
    {
        var lamp = sectionTransform.Find("Lamp");
        return lamp.GetComponent<MeshRenderer>();
    }

    private void GetRandomFirstSection(int lightsCount)
    {
        var minLightIndex = 0;
        _currentLightIndex = Random.Range(minLightIndex, lightsCount - 1);
        EnableCurrentLight();
    }

    private void UpdateSwitchLightsDirectionSing(int lightsCount)
    {
        if (_currentLightIndex == lightsCount - 1)
        {
            _switchLightsDirectionSing = -1;
        }
        else if (_currentLightIndex == 0)
        {
            _switchLightsDirectionSing = 1;
        }
    }

    private void EnableCurrentLight()
    {
        CurrentTrafficLightSection = TrafficLightSections[_currentLightIndex];

        _currentLight = _lights[_currentLightIndex];
        _currentLight.enabled = true;

        SetActiveLampMaterialEmmision(_currentMeshRenderer, false);
        _currentMeshRenderer = _renderers[_currentLightIndex];
        SetActiveLampMaterialEmmision(_currentMeshRenderer, true);

        _lastLightSwitchTime = Time.time;
    }

    private void SetActiveLampMaterialEmmision(MeshRenderer meshRenderer, bool isActive)
    {
        meshRenderer.GetPropertyBlock(_currentMaterial);
        Color color = Color.black;
        if (isActive)
        {
            color = _baseColors[_currentLightIndex] * 5f;
        }
        _currentMaterial.SetColor("_EmissionColor", color);
        meshRenderer.SetPropertyBlock(_currentMaterial);
    }

    private void UpdateMaterials()
    {
        var materialsCount = Materials.Count;
        for (int i = 0; i < materialsCount; i++)
        {
            var material = Materials[i];
            var renderer = _renderers[i];
            var actualMaterial = renderer.sharedMaterial;

            if (material != actualMaterial)
            {
                renderer.material = material;
                _baseColors[i] = material.color;
            }
        }
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        if (TrafficLightSections == null && TrafficLightSections.Count == 0) return;

        Materials.Clear();

        foreach (var section in TrafficLightSections)
        {
            var renderer = GetMeshRenderer(section.transform);

            Materials.Add(renderer.sharedMaterial);
        }
    }
#endif
}
