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
    private Timer _preEndGame;
    private TextureButton _nextButton;
    private GridContainer _questioning;
    private bool _endgame;
    [Export] private string TextDirectory = "res://dialog/";

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        if(!_dialog.FileExists(TextDirectory))
        {
            GD.PrintErr("Could Not Open Text File");
        }
        else
        {
            _dialog.Open(TextDirectory, File.ModeFlags.Read);
        }
        _buttonSound = GetNode<AudioStreamPlayer2D>("ButtonClickSound");
        _speakSound = GetNode<AudioStreamPlayer2D>("ButtonSpeakSound");
        _endGameTimer = GetNode<Timer>("EndGameTimer");
        _nextButton = GetNode<TextureButton>("Button");
        _questioning = GetNode<GridContainer>("Questioning");
        _questioning.Visible = false;
        _endgame = false;
        _preEndGame = GetNode<Timer>("PreEndGame");
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

    public void _PreStartBadEndgame()
    {
        _preEndGame.Start();
        _questioning.QueueFree();
        _label.QueueFree();
        this.Visible = false;
    }

    public void PlaySpeakSound()
    {
        _speakSound.Play();
    }
}
