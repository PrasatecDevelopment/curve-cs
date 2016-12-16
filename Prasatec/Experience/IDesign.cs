using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience
{
    public interface IDesign
    {
        Font GeneralFont { get; }
        Font HeaderFont { get; }
        Font SmallFont { get; }

        Color BackgroundColor { get; }
        Color ForegroundColor { get; }
        Color ErrorColor { get; }
        Color ButtonColor { get; }
        Color HoverColor { get; }
        Color ActiveColor { get; }
        Color ToggleColor { get; }
        Color IndeterminateColor { get; }

        Int32 PaddingBase { get; }

        Int32 ButtonHeight { get; }
        Int32 ButtonWidth { get; }
        Int32 ActionButtonHeight { get; }
        Int32 ActionButtonWidth { get; }
        Int32 PaginationButtonHeight { get; }
        Int32 PaginationButtonWidth { get; }
        Int32 TextboxHeight { get; }
        Int32 PasswordHeight { get; }
        Int32 NumberHeight { get; }
        Int32 DropdownHeight { get; }
        Int32 TimestampHeight { get; }
        Int32 ContentHeight { get; }

    }
}
