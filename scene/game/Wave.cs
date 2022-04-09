class Wave
{
    private Game _game;
    private Takusan.Scene.Game.Trigger _trigger;

    public void Setup(Game game)
    {
        _game = game;
        _trigger = new Takusan.Scene.Game.Trigger();

        _trigger.Every(1, () => { _game.AddEntityCube(); });
    }

    public void Update(float delta)
    {
        _trigger.Update(delta);
    }
}
