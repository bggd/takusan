using Godot;
using System;

public class Main : Node2D
{
    public override void _Ready()
    {
        GetTree().ChangeScene("res://scene/title_screen/title.tscn");
    }
}
