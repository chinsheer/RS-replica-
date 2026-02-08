using System;

public sealed class EffectHandle
{
    public readonly Action<float> Update;
    public readonly Action Cleanup;

    public EffectHandle(Action<float> update, Action cleanup)
    {
        Update = update;
        Cleanup = cleanup;
    }

    public void Tick(float deltaTime)
    {
        Update?.Invoke(deltaTime);
    }

    public void Dispose()
    {
        Cleanup?.Invoke();
    }
}
