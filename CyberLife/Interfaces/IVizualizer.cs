﻿namespace CyberLife
{
    /// <summary>
    /// Занимается отрисовкой мира.
    /// </summary>
    public interface IVisualizer
    {
        /// <summary>
        /// Обновить визуализацию на основании изменений мира.
        /// </summary>
        /// <param name="metadata"></param>
        void Update(Simple2DWorld.Simple2DWorld world);


    }
}