﻿using Common.MathematicsHelper;

namespace InterestManagement
{
    public class Scene<TSceneObject> : IScene<TSceneObject>
        where TSceneObject : ISceneObject
    {
        public IMatrixRegion<TSceneObject> MatrixRegion { get; }

        public Scene(ILogger log, Vector2 worldSize, Vector2 regionSize)
        {
            MatrixRegion =
                new MatrixRegion<TSceneObject>(log, worldSize, regionSize);
        }

        public void Dispose()
        {
            MatrixRegion?.Dispose();
        }
    }
}