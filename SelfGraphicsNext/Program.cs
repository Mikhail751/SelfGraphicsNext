﻿using SelfGraphicsNext.RayGraphics.Graphics3D.Geometry;
using SelfGraphicsNext.RayGraphics.Graphics3D.Rendering;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace SelfGraphicsNext
{
    class Run
    {
        public static Scene scene;
        public static RenderWindow _rw;
        public static Camera3 camera;
        public static uint x = 300;
        public static uint y = 300;
        public const int mRatio = 9;
        public static Task process;

        public static void Main(string[] args)
        {
            {
                scene = Scene.LoadSceneObj(@"C:\Users\Mical\Projects\BlenderProjects\outs\sphere2Level.obj");
                scene.Light = new Point3(0, 2, 2);
                camera = new Camera3(50, new Point3(-5, 0, 0), new Direction3(0, 0));
                _rw = new RenderWindow(new VideoMode(x * 1, y * 1), "SGN test", Styles.Close);
                _rw.Closed += (o, e) => _rw.Close();
                _rw.SetActive(true);
                _rw.Display();
                _rw.SetFramerateLimit(60);
                process = camera.RenderSceneMulti(scene, x, y, mRatio);
                while (!process.IsCompleted) ;
                camera.LiveRenderInage = new Image(x, y);
                Stopwatch time = new Stopwatch();
                while (_rw.IsOpen)
                {
                    _rw.Clear();
                    _rw.DispatchEvents();
                    {
                        _rw_MouseMoved();
                        {
                            if (process.IsCompleted && time.IsRunning)
                            {
                                time.Stop();
                                Console.WriteLine(time.Elapsed);
                            }
                            else if (!time.IsRunning && !process.IsCompleted)
                            {
                                time.Restart();
                            }
                            _rw.SetTitle($"Time : {time.Elapsed.ToString()}");
                            _rw.Draw(new Sprite(new Texture(camera.LiveRenderInage)));


                        }
                    }
                    _rw.Display();
                }

                static void _rw_MouseMoved()
                {
                    void updateImg()
                    {
                        process = camera.RenderSceneMulti(scene, x, y, mRatio);
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                            continue;
                        camera.Direction.Vertical += 10;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                            continue;
                        camera.Direction.Vertical -= 10;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                            continue;
                        camera.Direction.Horisontal -= 10;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                            continue;
                        camera.Direction.Horisontal += 10;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.W))
                            continue;
                        camera.Position.X += 1;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.S))
                            continue;
                        camera.Position.X -= 1;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.A))
                            continue;
                        camera.Position.Y -= 1;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.D))
                            continue;
                        camera.Position.Y += 1;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                            continue;
                        camera.Position.Z += 1;
                        updateImg();
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                    {
                        while (Keyboard.IsKeyPressed(Keyboard.Key.E))
                            continue;
                        camera.Position.Z -= 1;
                        updateImg();
                    }
                }
            }
        }
    }
}
