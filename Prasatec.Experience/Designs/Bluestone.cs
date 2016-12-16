using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience.Designs
{
    public sealed class Bluestone : IDesign
    {
        public Font SmallFont { get; private set; }
        public Font GeneralFont { get; private set; }
        public Font HeaderFont { get; private set; }
        public Color BackgroundColor { get; private set; }
        public Color ErrorColor { get; private set; }
        public Color ForegroundColor { get; private set; }
        public Color ButtonColor { get; private set; }
        public Color HoverColor { get; private set; }
        public Color ActiveColor { get; private set; }
        public Color ToggleColor { get; private set; }
        public Color IndeterminateColor { get; private set; }
        public int ActionButtonHeight { get; private set; }
        public int ActionButtonWidth { get; private set; }
        public int ButtonHeight { get; private set; }
        public int ButtonWidth { get; private set; }
        public int ContentHeight { get; private set; }
        public int DropdownHeight { get; private set; }
        public int NumberHeight { get; private set; }
        public int PaddingBase { get; private set; }
        public int PaginationButtonHeight { get; private set; }
        public int PaginationButtonWidth { get; private set; }
        public int PasswordHeight { get; private set; }
        public int TextboxHeight { get; private set; }
        public int TimestampHeight { get; private set; }

        public Bluestone()
        {
            SmallFont = new Font("Segoe UI", 8);
            GeneralFont = new Font("Segoe UI", 10);
            HeaderFont = new Font(this.GeneralFont, FontStyle.Bold);
            BackgroundColor = Color.FromArgb(255, 255, 255);
            ForegroundColor = Color.FromArgb(32, 32, 32);
            ErrorColor = Color.FromArgb(128, 0, 0);
            ButtonColor = Color.FromArgb(100, 150, 220);
            HoverColor = Color.FromArgb(69, 123, 197);
            ActiveColor = Color.FromArgb(55, 77, 104);
            ToggleColor = this.HoverColor;
            IndeterminateColor = Color.FromArgb(180, 180, 180);
            ActionButtonHeight = 24;
            ActionButtonWidth = 60;
            ButtonHeight = 30;
            ButtonWidth = 80;
            PaginationButtonHeight = 24;
            PaginationButtonWidth = 24;
            TextboxHeight = 32;
            PasswordHeight = 32;
            NumberHeight = 32;
            DropdownHeight = 32;
            TimestampHeight = 32;
            ContentHeight = 32;
            this.PaddingBase = 2;
        }
    }
}
