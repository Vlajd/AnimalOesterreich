using Godot;
using System;

public class textbox : TextureRect
{
   [Signal] public delegate void StartBadEndgame();
    private Label _label;
    private File _dialog = new File();
    private AudioStreamPlayer2D _buttonSound;
    private AudioStreamPlayer2D _speakSound;
    private Timer _endGameTimer;
    private TextureButton _nextButton;
    private GridContainer _questioning;
    private bool _endgame;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _dialog.Open("res://dialog/0.tres", File.ModeFlags.Read);
        _buttonSound = GetNode<AudioStreamPlayer2D>("ButtonClickSound");
        _speakSound = GetNode<AudioStreamPlayer2D>("ButtonSpeakSound");
        _endGameTimer = GetNode<Timer>("EndGameTimer");
        _nextButton = GetNode<TextureButton>("Button");
        _questioning = GetNode<GridContainer>("Questioning");
        _questioning.Visible = false;
        _endgame = false;
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            if (Visible && !_questioning.Visible) NextText();
        }
    }

    public void NextText()
    {
        _questioning.Visible = false;

        _label.Text = _dialog.GetLine();
        
        if (_label.Text == "Hat es dir gefallen?") ShowQuestioning();
        if (_label.Text == "Einen schoenen Tag noch!" && !_endgame) StartEndGame();

        _buttonSound.Play();
        PlaySpeakSound();
    }

    private void ShowQuestioning()
    {
        _nextButton.QueueFree();
        _questioning.Visible = true;
    }

    public void StartEndGame()
    {
        _endGameTimer.Start();
    }

    public void EndGame()
    {
        GetTree().Quit();
    }

    public void _StartBadEndgame()
    {
        EmitSignal("StartBadEndgame");
        _endgame = true;
    }

    public void PlaySpeakSound()
    {
        _speakSound.Play();
    }
}
