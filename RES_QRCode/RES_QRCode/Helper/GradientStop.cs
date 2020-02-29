﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RES_QRCode.Helper
{
    public class GradientStop
    {
        private float location;

        public float Location
        {
            get => location;
            set
            {
                // Value needs to be 0-1.
                if (value > 1)
                    value = 1;
                else if (value < 0)
                    value = 0;

                location = value;
            }
        }

        public Color Color { get; set; }
    }
}
