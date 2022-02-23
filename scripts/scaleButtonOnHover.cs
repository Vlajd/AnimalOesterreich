using Godot;
using System;

public class scaleButtonOnHover : TextureButton
{
    private Vector2 _currentScale = new Vector2(1.0f, 1.0f);
    [Export] private Vector2 HoverScale = new Vector2(1.5f, 1.5f);
    [Export] float HoverScaleSpeed = 20.0f;

    public override void _Process(float delta)
    {
        RectScale = RectScale.LinearInterpolate(_currentScale, delta * HoverScaleSpeed);
    }

    public void _onButtonMouseEntered()
    {
        _currentScale = HoverScale;
    }

    public void _onButtonMouseExited()
    {
        _currentScale = new Vector2(1.0f, 1.0f);
    }
}
