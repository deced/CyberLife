namespace CyberLife
{
    /// <summary>
    /// Релизует отрисовку мира.
    /// </summary>
    public interface IVisualizer
    {
        /// <summary>
        /// Обновляет визуализацию на основании изменений мира.
        /// </summary>
        /// <param name="world">Мир, для которого происходит обновление</param>
        void Update(Simple2DWorld.Simple2DWorld world);
    }
}