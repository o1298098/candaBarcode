using candaBarcode.action;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.BaiduMaps;
using Xamarin.Forms.Xaml;

namespace candaBarcode.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private SignalrClient client { get; set; }
        private Coordinate userpoint { get; set; }
        private string Idiom { get; set; }
        public MapPage()
        {
            InitializeComponent();
            Idiom = Plugin.DeviceInfo.CrossDeviceInfo.Current.Model;
            IMapManager mapManager = DependencyService.Get<IMapManager>();
            Debug.WriteLine(mapManager.CoordinateType);
            mapManager.CoordinateType = CoordType.BD09LL;
            Debug.WriteLine(mapManager.CoordinateType);
            map.Loaded += MapLoaded;
            IOfflineMap offlineMap = DependencyService.Get<IOfflineMap>();
            offlineMap.HasUpdate += (_, e) => {
                Debug.WriteLine("OfflineMap has update: " + e.CityID);
            };
            offlineMap.Downloading += (_, e) => {
                Debug.WriteLine("OfflineMap downloading: " + e.CityID);
            };
            var dfd = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            //var list = offlineMap.HotList;
            //list = offlineMap.AllList;
            //var curr = offlineMap.Current;
            //offlineMap.Start(27);
            //offlineMap.Start(75);
            //curr = offlineMap.Current;

            ICalculateUtils calc = DependencyService.Get<ICalculateUtils>();
            Debug.WriteLine(calc.CalculateDistance(
                new Coordinate(40, 116),
                new Coordinate(41, 117)
            ));//139599.429229778 in iOS, 139689.085961837 in Android
            client = new SignalrClient();
            client.Connect();
            btnchat.Clicked += async delegate {
                await client.Send(Idiom, "nihao", map.Center.Latitude.ToString(), map.Center.Longitude.ToString());
            };
            client.OnReceiveEvent += (sender, message) =>
            {
                var coord = new Coordinate(Convert.ToDouble(message[2]), Convert.ToDouble(message[3]));
                var terpins = map.Pins.Where(s => s.Title == message[0]);
                if (terpins.Count() > 0)
                {
                    int index=map.Pins.IndexOf(terpins.ElementAt(0));
                    map.Pins[index].Coordinate = coord;
                }
                else
                {
                    AddPin(coord, message[0]);
                }
               
            };

        }

        public void MapLoaded(object sender, EventArgs x)
        {
            map.ShowScaleBar = true;
            InitLocationService();
            InitEvents();

            //Coordinate[] coords = {
            //    new Coordinate(40.044, 116.391),
            //    new Coordinate(39.861, 116.284),
            //    new Coordinate(39.861, 116.468)
            //};

            //map.Polygons.Add(new Polygon
            //{
            //    Points = new ObservableCollection<Coordinate>(coords),
            //    Color = Color.Blue,
            //    FillColor = Color.Red.MultiplyAlpha(0.7),
            //    Width = 2
            //});

            //map.Circles.Add(new Circle
            //{
            //    Coordinate = map.Center,
            //    Color = Color.Green,
            //    FillColor = Color.Yellow.MultiplyAlpha(0.2),
            //    Radius = 200,
            //    Width = 2
            //});

            //Task.Run(() => {
            //    for (; ; )
            //    {
            //        Task.Delay(1000).Wait();

            //        var p = map.Polygons[0].Points[0];
            //        p = new Coordinate(p.Latitude + 0.002, p.Longitude);
            //        map.Polygons[0].Points[0] = p;

            //        map.Circles[0].Radius += 100;
            //    }
            //});

            // 坐标转换
            //IProjection proj = map.Projection;
            //var coord = proj.ToCoordinate(new Point(100, 100));
            //Debug.WriteLine(proj.ToScreen(coord));
        }

        private static bool moved = false;
        public void InitLocationService()
        {
            map.LocationService.LocationUpdated +=async (_, e) => {                
                //Debug.WriteLine("LocationUpdated: " + ex.Coordinate);
                if (!moved)
                {
                    map.Center = e.Coordinate;                    
                    moved = true;
                    await client.Send(Idiom, "nihao", e.Coordinate.Latitude.ToString(), e.Coordinate.Longitude.ToString());
                }
            };
            map.LocationService.Failed += (_, e) => {
                Debug.WriteLine("Location failed: " + e.Message);
            };

            map.LocationService.Start();
        }

        public void InitEvents()
        {
            btnTrack.Clicked += (_, e) =>
            {
                //if (map.ShowUserLocation)
                //{
                //    map.UserTrackingMode = UserTrackingMode.None;
                //    map.ShowUserLocation = false;
                //}
                //else
                //{
                //    map.UserTrackingMode = UserTrackingMode.Follow;
                //    map.ShowUserLocation = true;
                //}
                ICalculateUtils calc = DependencyService.Get<ICalculateUtils>();
                double distance = calc.CalculateDistance(map.Center, userpoint);
                var km = distance / 1000;
                var min = km / 40*60;
                label.Text = string.Format("距离{0}公里，大约需要{1}分钟",km.ToString("f2"), min.ToString("f2"));
            };
         
            map.LongClicked += async (_, e) => {
                var terpins = map.Pins.Where(s => s.Title == Idiom);
                if (terpins.Count() > 0)
                {
                    int index = map.Pins.IndexOf(terpins.ElementAt(0));
                    map.Pins[index].Coordinate = e.Coordinate;
                }
                else
                {
                    AddPin(e.Coordinate, Idiom);
                }                
                await client.Send(Idiom, "nihao",e.Coordinate.Latitude.ToString(),e.Coordinate.Longitude.ToString());
            };

            map.StatusChanged += (_, e) => {
                //Debug.WriteLine(map.Center + " @" + map.ZoomLevel);
            };
        }

        void AddPin(Coordinate coord,string name)
        {
            Pin annotation = new Pin
            {
                Title = name,
                Coordinate = coord,
                Animate = true,
                Draggable = true,
                Enabled3D = true,
                Image = XImage.FromStream(
                    typeof(MapPage).GetTypeInfo().Assembly.GetManifestResourceStream("candaBarcode.Droid.Images.location.png")
                )
            };
            map.Pins.Add(annotation);

            annotation.Drag +=async (o, e) => {
                Pin self = o as Pin;
                self.Title = Idiom;
                int i = map.Pins.IndexOf(self);
                await client.Send(Idiom, "nihao", self.Coordinate.Latitude.ToString(), self.Coordinate.Longitude.ToString());
                //if (map.Polylines.Count > 0 && i > -1)
                //{
                //    map.Polylines[0].Points[i] = self.Coordinate;
                   
                //}
            };
            annotation.Clicked += (_, e) => {
                Debug.WriteLine("clicked");
                ((Pin)_).Image = XImage.FromStream(
                    typeof(MapPage).GetTypeInfo().Assembly.GetManifestResourceStream("candaBarcode.Droid.Images.location.png")
                );
                userpoint = ((Pin)_).Coordinate;
            };

            //if (0 == map.Polylines.Count && map.Pins.Count > 1)
            //{
            //    Polyline polyline = new Polyline
            //    {
            //        Points = new ObservableCollection<Coordinate> {
            //            map.Pins[0].Coordinate, map.Pins[1].Coordinate
            //        },
            //        Width = 4,
            //        Color = Color.Purple
            //    };

            //    map.Polylines.Add(polyline);
            //}
            //else if (map.Polylines.Count > 0)
            //{
            //    map.Polylines[0].Points.Add(annotation.Coordinate);
            //}
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}