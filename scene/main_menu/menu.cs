using Godot;
using System;

public class menu : Control
{
    public void _on_Button_Quit_pressed()
    {
        GetTree().Quit();
    }
}
