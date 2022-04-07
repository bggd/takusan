using Godot;
using System;

public class title : Control
{
    private PackedScene _mainMenu;

    public override void _Ready()
    {
        _mainMenu = ResourceLoader.Load<PackedScene>("res://scene/main_menu/menu.tscn");
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent && keyEvent.Pressed)
        {
            GetTree().ChangeSceneTo(_mainMenu);
        }
    }
}
