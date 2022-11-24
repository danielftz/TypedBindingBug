using System;
using CommunityToolkit.Maui.Markup;

namespace TypedBindingBug
{
    public class MainPage : ContentPage
    {
        #region Color BindableProperty
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            propertyName: nameof(Color),
            returnType: typeof(Color),
            declaringType: typeof(MainPage),
            defaultValue: Colors.Blue
        );
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        #endregion
        Random _random = new Random();
        public MainPage()
        {
            Content = new VerticalStackLayout
            {
                Spacing = 10,
                Children =
                {
                    new BoxView
                    {
						//never changes
						BindingContext = this,
                    }.Size(200).Center()
                    .Bind(BoxView.ColorProperty, (MainPage m)=>m.Color, mode: BindingMode.OneWay),


                    new BoxView
                    {
                        BindingContext = this,
                    }.Size(200).Center()
                    .Bind(BoxView.ColorProperty, nameof(Color)),


                    new Button
                    {
                        Text = "Change Color",
                        Command = new Command(() =>
                        {
                            int num = _random.Next(0,256);
                            Color = new Color(num, num, num);
                        })
                    }
                }
            };
        }
    }
}

