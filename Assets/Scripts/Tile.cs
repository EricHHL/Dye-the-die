public interface ITile {

    bool OnPlayerEnter(Player player, DiceFace downwardFace);
    void OnPlayerEnterReverse(Player player, DiceFace downwardFace);
    bool CanPlayerEnter(Player player, DiceFace nextFace);
}
