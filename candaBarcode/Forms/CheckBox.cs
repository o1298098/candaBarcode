using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;

namespace candaBarcode.Forms
{
    public class Checkbox : StackLayout
    {
        public event EventHandler CheckedChanged;

        private readonly Image _image;
        private readonly Label _label;

        //CHANGE THESE 2 STRINGS ACCORDING TO YOUR NAMESPACE AND IMAGE
        static string imgUnchecked = "uncheck.png";
        static string imgChecked = "checked.png";

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(Checkbox));

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(Boolean?), typeof(Checkbox), null, BindingMode.TwoWay, propertyChanged: CheckedValueChanged);

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(String), typeof(Checkbox), null, BindingMode.TwoWay, propertyChanged: TextValueChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public Boolean? Checked
        {
            get
            {
                if (GetValue(CheckedProperty) == null)
                {
                    return null;
                }

                return (Boolean)GetValue(CheckedProperty);
            }
            set
            {
                SetValue(CheckedProperty, value);
                OnPropertyChanged();
                RaiseCheckedChanged();
            }
        }

        public String Text
        {
            get
            {
                if (GetValue(TextProperty) == null)
                {
                    return null;
                }

                return (String)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged();
                RaiseCheckedChanged();
            }
        }

        public Checkbox()
        {
            Orientation = StackOrientation.Horizontal;
            BackgroundColor = Color.Transparent;

            _image = new Image()
            {
                Source = imgUnchecked,
                HeightRequest = 35,
                WidthRequest = 35,
                VerticalOptions = LayoutOptions.Center
            };
            var tg = new TapGestureRecognizer();
            tg.Tapped += OnClicked;
            _image.GestureRecognizers.Add(tg);
            Children.Add(_image);

            _label = new Label()
            {
                VerticalOptions = LayoutOptions.Center
            };
            _label.GestureRecognizers.Add(tg);
            Children.Add(_label);
        }

        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && (Boolean)newValue)
                ((Checkbox)bindable)._image.Source =imgChecked;
            else
                ((Checkbox)bindable)._image.Source =imgUnchecked;
        }

        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((Checkbox)bindable)._label.Text = newValue.ToString();
        }

        private void RaiseCheckedChanged()
        {
            CheckedChanged?.Invoke(this, EventArgs.Empty);
            Command?.Execute(null);
        }

        public void OnClicked(object sender, EventArgs e)
        {
            Checked = !Checked;
        }
    }
}
