using Godot;
using System;

public class isabelle : Spatial
{
    AnimationPlayer anim;

    public override void _Ready()
    {
        anim = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _Process(float delta)
    {
        if (anim.IsPlaying()) return;
        anim.Play();
    }
}
