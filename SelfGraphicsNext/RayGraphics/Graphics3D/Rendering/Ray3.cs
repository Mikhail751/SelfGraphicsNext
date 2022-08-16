﻿using SelfGraphicsNext.BaseGraphics;
using SelfGraphicsNext.RayGraphics.Graphics3D.Geometry;

namespace SelfGraphicsNext.RayGraphics.Graphics3D.Rendering
{
    public class Ray3
    {
        public Direction3 Direction;
        public Point3 Position;
        public Point3 Aim;

        public Point ImagePosition;
        public Ray3(Direction3 direction, Point3 position)
        {
            Direction = direction;
            Position = position;
        }

        public Ray3(Point3 position)
        {
            Position = position;
            Direction = new Direction3();
        }

        public Ray3()
        {
            Position = new Point3(0, 0, 0);
            Direction = new Direction3();

        }

        public ColisionResult CollideInScene(Scene scene)
        {
            List<ColisionResult> results = new List<ColisionResult>();
            for (int i = 0; i < scene.Objects.Count; i++)
            {
                PolygonGroup groups = scene.Objects[i];
                var res = groups.Colide(this);
                if (res.Colided)
                    results.Add(res);
            }
            if (results.Count > 0)
            {
                var current = results.MinBy(i => i.Colision.Distance);
                Aim = current.Colision;
                return current;
            }
            return new ColisionResult() { Colided = false };
        }

        public ColisionResult CollideInSceneIns(Scene scene, Polygon pol)
        {
            List<ColisionResult> results = new List<ColisionResult>();
            for (int i = 0; i < scene.Objects.Count; i++)
            {
                PolygonGroup groups = scene.Objects[i];
                var res = groups.ColideIns(this, pol);
                if (res.Colided)
                    results.Add(res);
            }
            if (results.Count > 0)
            {
                var current = results.MinBy(i => i.Colision.Distance);
                Aim = current.Colision;
                return current;
            }
            return new ColisionResult() { Colided = false };
        }
        public override string ToString()
        {
            return $"Ray 3d[Position : {Position.ToString()}, Direction : {Direction.ToString()}]";
        }
    }
}
