using System;

namespace Sample.Views
{

    public class MainPageMenuItem
    {
        public MainPageMenuItem()
        {
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}