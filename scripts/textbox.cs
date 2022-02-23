using Godot;
using System;

public class textbox : ColorRect
{
   
    private Label _label;
    private File _dialog = new File();
    private int _line = 0;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _dialog.Open("res://dialog/0.tres", File.ModeFlags.Read);
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
     if (Input.IsActionJustPressed("ui_down"))
     {
         NextText();
     }
 }

    public void NextText()
    {
        _label.Text = _dialog.GetLine();
        _line++;
    }
}
