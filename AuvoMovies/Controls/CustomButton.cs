using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoMovies.Controls
{
    public class CustomButton : Button
    {
        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(CustomButton), 10);

        public int BorderRadius
        {
            get => (int)GetValue(BorderRadiusProperty);
            set => SetValue(BorderRadiusProperty, value);
        }
    }
}
