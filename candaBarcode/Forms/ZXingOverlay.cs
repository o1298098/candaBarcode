﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace candaBarcode.Forms
{
	public class ZXingOverlay :Grid
	{
        //Label topText;
        Label botText;
        Button flash;
        public delegate void FlashButtonClickedDelegate(Button sender, EventArgs e);
        public event FlashButtonClickedDelegate FlashButtonClicked;
        public ZXingOverlay ()
		{
            BindingContext = this;
            //ColumnSpacing = 0;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;

            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            var boxview = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.6,

            };
            var boxview2 = new BoxView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                Opacity = 0.6,

            };
            Children.Add(boxview, 0, 0);
            Children.Add(boxview2, 0, 2);

            //SetColumnSpan(boxview, 5);
            //SetColumnSpan(boxview2, 5);
            var AbsoluteLayouts = new AbsoluteLayout();
            SetColumnSpan(AbsoluteLayouts, 1);
            //topText = new Label
            //{
            //    VerticalOptions = LayoutOptions.Center,
            //    HorizontalOptions = LayoutOptions.Center,
            //    TextColor = Color.White,
            //    AutomationId = "zxingDefaultOverlay_TopTextLabel",
            //};
            //topText.SetBinding(Label.TextProperty, new Binding(nameof(TopText)));
            //Children.Add(topText, 0, 0);
            //SetColumnSpan(topText, 5);         
            botText = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.White,
                AutomationId = "zxingDefaultOverlay_BottomTextLabel",
            };
            botText.SetBinding(Label.TextProperty, new Binding(nameof(BottomText)));
            //Children.Add(botText, 0, 2);
            //SetColumnSpan(botText, 5);
            var MyStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            
            flash = new Button
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                //HeightRequest = 3,
                Image= "flashlightoff.png",
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                Opacity = 0.2,
                AutomationId = "zxingDefaultOverlay_FlashButton",
            };
            flash.SetBinding(Button.IsVisibleProperty, new Binding(nameof(ShowFlashButton)));
            flash.SetBinding(Button.TextProperty, new Binding(nameof(ButtonText)));
            flash.Clicked += (sender, e) =>
            {
                FlashButtonClicked?.Invoke(flash, e);
            };
            //MyStackLayout.Children.Add(botText);
            MyStackLayout.Children.Add(flash);
            Children.Add(MyStackLayout, 0, 2);
            //SetColumnSpan(MyStackLayout, 5);
            //this.ColumnSpacing = 0;
            this.RowSpacing = 0;
            //Device.StartTimer(TimeSpan.FromSeconds(0.2), () =>
            //{
            //    weizhi += 7;
            //    AbsoluteLayout.SetLayoutBounds(redline, new Rectangle(1, weizhi, 1, 1));
            //    if (weizhi > 150)
            //    {
            //        weizhi = -100;
            //    }
            //    return true;
            //});
        }

        public static readonly BindableProperty TopTextProperty =
            BindableProperty.Create(nameof(TopText), typeof(string), typeof(ZXingOverlay), string.Empty);
        public string TopText
        {
            get { return (string)GetValue(TopTextProperty); }
            set { SetValue(TopTextProperty, value); }
        }

        public static readonly BindableProperty BottomTextProperty =
            BindableProperty.Create(nameof(BottomText), typeof(string), typeof(ZXingOverlay), string.Empty);
        public string BottomText
        {
            get { return (string)GetValue(BottomTextProperty); }
            set { SetValue(BottomTextProperty, value); }
        }

        public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(ZXingOverlay), string.Empty);
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
        public static readonly BindableProperty ShowFlashButtonProperty =
            BindableProperty.Create(nameof(ShowFlashButton), typeof(bool), typeof(ZXingOverlay), false);
        public bool ShowFlashButton
        {
            get { return (bool)GetValue(ShowFlashButtonProperty); }
            set { SetValue(ShowFlashButtonProperty, value); }
        }

        public static BindableProperty FlashCommandProperty =
            BindableProperty.Create(nameof(FlashCommand), typeof(ICommand), typeof(ZXingOverlay),
                defaultValue: default(ICommand),
                propertyChanged: OnFlashCommandChanged);
        public ICommand FlashCommand
        {
            get { return (ICommand)GetValue(FlashCommandProperty); }
            set { SetValue(FlashCommandProperty, value); }
        }

        private static void OnFlashCommandChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var overlay = bindable as ZXingOverlay;
            if (overlay?.flash == null) return;
            overlay.flash.Command = newValue as Command;
        }
    }
}