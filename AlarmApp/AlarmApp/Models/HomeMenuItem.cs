using System;
using System.Collections.Generic;
using System.Text;

namespace AlarmApp.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Alarm
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
