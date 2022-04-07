using Godot;
using System;

public class menu : Control
{
    private PackedScene _game;

    public override void _Ready()
    {
        _game = ResourceLoader.Load<PackedScene>("res://scene/game/game.tscn");
    }
    public void _on_Button_PlayGame_pressed()
    {
        GetTree().ChangeSceneTo(_game);
    }

    public void _on_Button_Quit_pressed()
    {
        GetTree().Quit();
    }
}
