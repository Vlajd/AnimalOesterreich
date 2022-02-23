using Godot;
using System;

public class main : Spatial
{
    private isabelle _isabelle;
    private textbox _textbox;
    private Camera _mainCamera;
    private Camera _scndCamera;
    private AudioStreamPlayer2D _music;
    private Timer _badEndgameTimer;
    public override void _Ready()
    {
        _isabelle = GetNode<isabelle>("Isabelle");

        _mainCamera = GetNode<Camera>("MainCamera");
        _mainCamera.Current = true;
        _scndCamera = GetNode<Camera>("scndCamera");
        _scndCamera.Current = false;

        _badEndgameTimer = GetNode<Timer>("BadEndgameTimer");

        _textbox = GetNode<textbox>("TextBox");
        _textbox.Visible = false;
        _music = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
    }

    public void InitTextbox()
    {
        _textbox.Visible = true;
        _textbox.PlaySpeakSound();
    }

    public void StartBadEndgame()
    {
        _badEndgameTimer.Start();
        _textbox.QueueFree();
        _mainCamera.Current = false;
        _scndCamera.Current = true;
        _music.VolumeDb = 36.0f;
    }

    public void EndGame()
    {
        GetTree().Quit();
    }
}
