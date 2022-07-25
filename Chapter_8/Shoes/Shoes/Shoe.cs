using System;
using System.Collections.Generic;
using System.Text;

namespace Shoes {

    enum Style {
        Sneaker,
        Loafer,
        Sandal,
        Flipflop,
        Wingtip,
        Clog,
    }

    class Shoe {
        public Style Style {
            get; private set;
        }
        public string Color {
            get; private set;
        }
        public Shoe(Style style, string color) {
            Style = style;
            Color = color;
        }
        public string Description {
            get { return $"A {Color} {Style}"; }
        }
    }
}
