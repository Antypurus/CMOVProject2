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
        List<WeatherForecast> forecast = OpenWeather.GetCityWeatherForecast("London", "uk");

        public MainPage()
        {
            InitializeComponent();

            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
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

            double step = (double)Width/(double)ForecastCount;

            // Calculate x,y positions
            for(int i=0;i<ForecastCount;++i)
            {
                double averageTemperature = (Forecasts[i].MinTemp + Forecasts[i].MaxTemp)/2.0;

                Point point = new Point();
                point.x = Convert.ToInt32(Math.Round(i*step));
                point.y = Convert.ToInt32(Math.Round((averageTemperature/TemperatureCeiling)*Height));

                ret.Add(point);
            }

            return ret;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            const int offset = 50;
            List<Point> points = CalculateForecastPointPositions(this.forecast,info.Height-offset,info.Width-offset);

            SKPath path = new SKPath();
            for(int i=0;i<points.Count-1;++i)
            {
                path.MoveTo(points[i].x+offset,info.Height-points[i].y-offset);
                path.LineTo(points[i+1].x+offset,info.Height-points[i+1].y-offset);
            }
            path.Close();

            SKPaint linePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                FakeBoldText = true,
                Color = SKColors.Blue
            };

            SKPaint pointPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                StrokeWidth = 1,
                FakeBoldText = true,
                Color = SKColors.Red
            };
            
            SKPoint origin = new SKPoint(offset,info.Height-1-offset);
            SKPoint widthEnd = new SKPoint(info.Width-1,info.Height-1-offset);
            SKPoint heightEnd = new SKPoint(offset,0);

            canvas.Clear();
            canvas.DrawPath(path,linePaint);
            //draw circle in point positions
            for(int i=0;i<points.Count;++i)
            {
                double averageTemperature = (forecast[i].MinTemp + forecast[i].MaxTemp)/2.0;
                const int drawOffset = 10;
                const int drawOffsetCorrection = 5;

                SKPoint skpoint = new SKPoint(points[i].x+offset,info.Height-points[i].y-offset);
                SKPoint skpointbase = new SKPoint(points[i].x+offset,info.Height-1-offset);
                canvas.DrawCircle(skpoint,5.0f,pointPaint);
                canvas.DrawCircle(skpointbase,5.0f,pointPaint);
                canvas.DrawText(""+averageTemperature+"ºC",points[i].x+offset,info.Height-points[i].y-offset-drawOffset,pointPaint);

                string time = forecast[i].Date.Split(null)[1];
                canvas.DrawText(time,points[i].x+offset/2,info.Height-1-offset+drawOffset+drawOffsetCorrection,pointPaint);
            }
            //draw graph grid
            canvas.DrawLine(origin,widthEnd,pointPaint);
            canvas.DrawLine(origin,heightEnd,pointPaint);
        }
    }
}
