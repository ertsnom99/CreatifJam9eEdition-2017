using UnityEngine;
using System.Collections.Generic;

public class GlowBottle : MonoBehaviour
{
	public Color GlowColor = new Color(0F, 1F, 1F, 1F);
	public float LerpFactor = 10;
    public Color SelectedColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

	public Renderer[] Renderers
	{
		get;
		private set;
	}

	public Color CurrentColor
	{
		get { return _currentColor; }
	}

	private List<Material> _materials = new List<Material>();
	private Color _currentColor;
	private Color _targetColor;

	void Start()
	{
		Renderers = GetComponentsInChildren<Renderer>();

		foreach (var renderer in Renderers)
		{	
			_materials.AddRange(renderer.materials);
		}
	}

	public void TriggerGlow()
	{
        _targetColor = GlowColor;
		enabled = true;
	}

	public void StopGlow()
	{
		_targetColor = Color.black;
		enabled = true;
	}

    public void Select()
    {
        _currentColor = _targetColor = SelectedColor;

        for (int i = 0; i < _materials.Count; i++)
        {
            _materials[i].SetColor("_GlowColor", SelectedColor);
        }

        enabled = false;
    }

    public void RemoveGlow()
    {
        _currentColor = _targetColor = Color.black;

        for (int i = 0; i < _materials.Count; i++)
        {
            _materials[i].SetColor("_GlowColor", _currentColor);
        }

        enabled = false;
    }

	/// <summary>
	/// Loop over all cached materials and update their color, disable self if we reach our target color.
	/// </summary>
	private void Update()
	{
		_currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * LerpFactor);

		for (int i = 0; i < _materials.Count; i++)
		{
			_materials[i].SetColor("_GlowColor", _currentColor);
		}

		if (_currentColor.Equals(_targetColor))
		{
			enabled = false;
		}
	}
}
