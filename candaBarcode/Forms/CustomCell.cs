using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace candaBarcode.Forms
{
    class CustomCell : ViewCell
    {
        Label nameLabel, ageLabel, locationLabel;

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create("Name", typeof(string), typeof(CustomCell), "Name");
        public static readonly BindableProperty AgeProperty =
            BindableProperty.Create("Age", typeof(int), typeof(CustomCell), 0);
        public static readonly BindableProperty LocationProperty =
            BindableProperty.Create("Location", typeof(string), typeof(CustomCell), "Location");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public int Age
        {
            get { return (int)GetValue(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        public string Location
        {
            get { return (string)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }
        public CustomCell()
        {
            //instantiate each of our views
            nameLabel = new Label();
            StackLayout cellWrapper = new StackLayout();
            StackLayout horizontalLayout = new StackLayout();
            ageLabel = new Label();
            locationLabel = new Label();

            //set bindings
            nameLabel.SetBinding(Label.TextProperty, "Name");
            ageLabel.SetBinding(Label.TextProperty, "Age");
            locationLabel.SetBinding(Label.TextProperty, "Location");

            //Set properties for desired design
            cellWrapper.BackgroundColor = Color.FromHex("#eee");
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            ageLabel.HorizontalOptions = LayoutOptions.EndAndExpand;
            nameLabel.TextColor = Color.FromHex("#f35e20");
            ageLabel.TextColor = Color.FromHex("503026");

            //add views to the view hierarchy
            horizontalLayout.Children.Add(locationLabel);
            horizontalLayout.Children.Add(nameLabel);
            horizontalLayout.Children.Add(ageLabel);
            cellWrapper.Children.Add(horizontalLayout);
            View = cellWrapper;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                nameLabel.Text = Name;
                ageLabel.Text = Age.ToString();
                locationLabel.Text = Location;
            }
        }
    }
}
