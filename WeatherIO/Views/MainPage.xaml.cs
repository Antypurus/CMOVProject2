using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Business.WeatherAPI;
using Domain.Models;

namespace WeatherIO
{
    public partial class MainPage : ContentPage
    {
        string data = OpenWeather.GetCityWeather("London", "uk").Temperature.ToString();
        List<WeatherForecast> forecast = OpenWeather.GetCityWeatherForecast("London","uk");

        public MainPage()
        {
            InitializeComponent();

            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                FakeBoldText = true,
                Color = SKColors.Blue
            };

            float y = info.Height / 2;
            float x = info.Width / 2;
            canvas.DrawText(data, 0, y, paint);
        }
    }
}
