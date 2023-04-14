using System;
using System.Windows;
using System.Windows.Controls;

namespace SecurityProject
{
    public class NavButton : ListBoxItem
    {
        static NavButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
        }

        //public bool RequiresDefaultFolders { get; set; }

        public Uri Navlink
        {
            get { return (Uri)GetValue(NavlinkProperty); }
            set { SetValue(NavlinkProperty, value); }
        }

        public static readonly DependencyProperty NavlinkProperty = DependencyProperty.Register("Navlink", typeof(Uri), typeof(NavButton), new PropertyMetadata(null));

        public string Navtext
        {
            get { return (string)GetValue(NavtextProperty); }
            set { SetValue(NavtextProperty, value); }
        }

        public static readonly DependencyProperty NavtextProperty = DependencyProperty.Register("Navtext", typeof(string), typeof(NavButton), new PropertyMetadata(null));
    }
}