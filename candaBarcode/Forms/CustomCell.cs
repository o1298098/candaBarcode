using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace candaBarcode.Forms
{
    class CustomCell : ViewCell
    {
        Label indexLabel, numLabel, stateLabel;

        public static readonly BindableProperty IndexProperty =
            BindableProperty.Create("index", typeof(string), typeof(CustomCell), "index");
        public static readonly BindableProperty NumProperty =
            BindableProperty.Create("num", typeof(string), typeof(CustomCell), "num");
        public static readonly BindableProperty StateProperty =
            BindableProperty.Create("state", typeof(string), typeof(CustomCell), "state");

        public string Index
        {
            get { return (string)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public string Num
        {
            get { return (string)GetValue(NumProperty); }
            set { SetValue(NumProperty, value); }
        }

        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public CustomCell()
        {
            //instantiate each of our views
            indexLabel = new Label();
            numLabel = new Label();
            stateLabel = new Label();
            StackLayout cellWrapper = new StackLayout();
            StackLayout horizontalLayout = new StackLayout();
            

            //set bindings
            indexLabel.SetBinding(Label.TextProperty, "index");
            numLabel.SetBinding(Label.TextProperty, "num");
            stateLabel.SetBinding(Label.TextProperty, "state");

            //Set properties for desired design
            //cellWrapper.BackgroundColor = Color.FromHex("#eee");
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            //numLabel.HorizontalOptions = LayoutOptions.EndAndExpand;
            //indexLabel.TextColor = Color.FromHex("#f35e20");
            indexLabel.WidthRequest = 120;            
            numLabel.WidthRequest = 120;
            stateLabel.WidthRequest = 120;
            indexLabel.HeightRequest = 50;
            numLabel.HeightRequest = 50;
            stateLabel.HeightRequest = 50;
            indexLabel.HorizontalTextAlignment = TextAlignment.Center;
            numLabel.HorizontalTextAlignment = TextAlignment.Center;
            stateLabel.HorizontalTextAlignment = TextAlignment.Center;
            indexLabel.VerticalTextAlignment = TextAlignment.Center;
            numLabel.VerticalTextAlignment = TextAlignment.Center;
            stateLabel.VerticalTextAlignment = TextAlignment.Center;
            //numLabel.TextColor = Color.FromHex("503026");

            //add views to the view hierarchy
            
            horizontalLayout.Children.Add(indexLabel);
            horizontalLayout.Children.Add(numLabel);
            horizontalLayout.Children.Add(stateLabel);
            cellWrapper.Children.Add(horizontalLayout);
            View = cellWrapper;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                indexLabel.Text =Index;
                numLabel.Text = Num;
                stateLabel.Text =State;
            }
        }
    }
}
