using System;

public class Dino
{
    private readonly int _groundY;
    private readonly int _airY;
    private int _jumpTimer;
    private const int JumpDurationFrames = 5;

    public int X { get; }
    public bool IsJumping { get; private set; }

    public Dino(int x, int groundY, int airY)
    {
        X = x;
        _groundY = groundY;
        _airY = airY;
    }

    public void StartJump()
    {
        if (IsJumping)
        {
            return;
        }

        IsJumping = true;
        _jumpTimer = JumpDurationFrames;
    }

    public void Update()
    {
        if (!IsJumping)
        {
            return;
        }

        _jumpTimer--;
        if (_jumpTimer <= 0)
        {
            IsJumping = false;
        }
    }

    public void Render()
    {
        if (IsJumping)
        {
            Console.SetCursorPosition(X, _groundY);
            Console.Write("_");
            Console.SetCursorPosition(X, _airY);
            Console.Write("D");
        }
        else
        {
            Console.SetCursorPosition(X, _airY);
            Console.Write(" ");
            Console.SetCursorPosition(X, _groundY);
            Console.Write("D");
        }
    }
}

public class Obstacle
{
    private readonly int _startX;
    public int GroundY { get; }
    public int X { get; private set; }

    public Obstacle(int startX, int groundY)
    {
        _startX = startX;
        GroundY = groundY;
        X = startX;
    }

    public void Move()
    {
        X--;
        if (X <= 0)
        {
            X = _startX;
        }
    }

    public void ClearPrevious(int previousX)
    {
        if (previousX <= 0)
        {
            return;
        }

        Console.SetCursorPosition(previousX, GroundY);
        Console.Write("_");
    }

    public void Draw()
    {
        Console.SetCursorPosition(X, GroundY);
        Console.Write("X");
    }

    public bool CollidesWith(Dino dino)
    {
        return X == dino.X && !dino.IsJumping;
    }
}
