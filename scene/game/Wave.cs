using System.Numerics;

class Wave
{
    private Takusan.Scene.Game.Trigger _trigger;
    private Vector3[] _spawnPoints = new Vector3[4];
    private System.Random _rnd = new System.Random();

    public void Setup()
    {
        _trigger = new Takusan.Scene.Game.Trigger();
        _spawnPoints[0] = new Vector3(-10.0f, -10.0f, 0.0f);
        _spawnPoints[1] = new Vector3(10.0f, -10.0f, 0.0f);
        _spawnPoints[2] = new Vector3(-10.0f, 10.0f, 0.0f);
        _spawnPoints[3] = new Vector3(10.0f, 10.0f, 0.0f);

        _trigger.Every(1, () => { Game.Instance.AddEntityCube(getRandomPoints()); });
    }

    public void Update(float delta)
    {
        _trigger.Update(delta);
    }

    Vector3 getRandomPoints()
    {
        return _spawnPoints[_rnd.Next(_spawnPoints.Length)];
    }
}
