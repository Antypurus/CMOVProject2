using Domain.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using WeatherIO.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherIO.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastInfoView : ContentPage
    {
        private readonly ForecastInfoViewModel _vm;

        public ForecastInfoView(string city, string country)
        {
            InitializeComponent();
            _vm = new ForecastInfoViewModel(city, country);
            BindingContext = _vm;

            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            CanvasView = canvasView;
        }

        private struct Point
        {
            public int x;
            public int y;
        }

        private List<Point> CalculateForecastPointPositions(List<WeatherForecast> Forecasts, int Height, int Width)
        {
            List<Point> ret = new List<Point>();
            const int ForecastCount = 8;
            const double TemperatureCeiling = 50.0f; // could do a dynamic ceiling but this should create a better effect

            double step = (double)Width / (double)ForecastCount;

            // Calculate x,y positions
            for (int i = 0; i < ForecastCount; ++i)
            {
                double averageTemperature = (Forecasts[i].MinTemp + Forecasts[i].MaxTemp) / 2.0;

                Point point = new Point();
                point.x = Convert.ToInt32(Math.Round(i * step));
                point.y = Convert.ToInt32(Math.Round((averageTemperature / TemperatureCeiling) * Height));

                ret.Add(point);
            }

            return ret;
        }

        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            const int offset = 50;
            List<Point> points = CalculateForecastPointPositions(_vm.ForecastsIntervals, info.Height - offset, info.Width - offset);

            SKPath path = new SKPath();
            for (int i = 0; i < points.Count - 1; ++i)
            {
                path.MoveTo(points[i].x + offset, info.Height - points[i].y - offset);
                path.LineTo(points[i + 1].x + offset, info.Height - points[i + 1].y - offset);
            }
            path.Close();

            SKPaint linePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                IsAntialias = true,
                FakeBoldText = true,
                Color = SKColors.Blue
            };

            SKPaint pointPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                StrokeWidth = 1,
                IsAntialias = true,
                FakeBoldText = true,
                Color = SKColors.Red
            };

            SKPaint text = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                StrokeWidth = 1,
                IsAntialias = true,
                FakeBoldText = true,
                Color = SKColors.Black
            };

            SKPoint origin = new SKPoint(offset, info.Height - 1 - offset);
            SKPoint widthEnd = new SKPoint(info.Width - 1, info.Height - 1 - offset);
            SKPoint heightEnd = new SKPoint(offset, 0);

            canvas.Clear(SKColors.WhiteSmoke);
            canvas.DrawPath(path, linePaint);

            //draw graph grid
            canvas.DrawLine(origin, widthEnd, text);
            canvas.DrawLine(origin, heightEnd, text);

            //draw circle in point positions
            for (int i = 0; i < points.Count; ++i)
            {
                double averageTemperature = (_vm.ForecastsIntervals[i].MinTemp + _vm.ForecastsIntervals[i].MaxTemp) / 2.0;
                const int drawOffset = 10;
                const int drawOffsetCorrection = 5;
                const int iconSize = 50;

                SKPoint skpoint = new SKPoint(points[i].x + offset, info.Height - points[i].y - offset);
                SKPoint skpointbase = new SKPoint(points[i].x + offset, info.Height - 1 - offset);
                canvas.DrawCircle(skpoint, 5.0f, pointPaint);
                canvas.DrawCircle(skpointbase, 5.0f, pointPaint);

                canvas.DrawText("" + averageTemperature + "ºC", points[i].x + offset, info.Height - points[i].y - offset - drawOffset, text);

                string time = _vm.ForecastsIntervals[i].Date;
                canvas.DrawText(time, points[i].x + offset / 2, info.Height - 1 - offset + drawOffset + drawOffsetCorrection, text);

                SKBitmap bitmap = new SKBitmap(iconSize, iconSize);
                _vm.ForecastsIntervals[i].WeatherIcon.Resize(bitmap, SKBitmapResizeMethod.Box);
                SKPoint iconCoord = new SKPoint(points[i].x + offset / 2, info.Height - points[i].y - offset + drawOffset / 2);
                canvas.DrawBitmap(bitmap, iconCoord);
            }
        }

    }
}
