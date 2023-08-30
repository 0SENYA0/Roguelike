namespace Assets.Infrastructure.DataStorageSystem
{
    public interface IPlayerData
    {
        int Money { get; }
        bool IsMusicOn { get; }
        bool IsSfxOn { get; }
    }
}