/// <summary>
/// 病原菌が実装すべきインターフェース
/// </summary>
public interface IPathogen
{
    /// <summary>
    /// true のとき行動を妨害する（移動停止など）
    /// </summary>
    void SetImpeded(bool impeded);
}
