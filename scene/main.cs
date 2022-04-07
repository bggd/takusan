using Godot;
using System;

public class main : Node2D
{
    public override void _Ready()
    {
        GetTree().ChangeScene("res://scene/title_screen/title.tscn");
    }
}
