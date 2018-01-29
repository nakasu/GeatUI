using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GeatUI
{
    class PlaceableElement : BindableBase
    {
        private int x;
        /// <summary>
        /// X座標
        /// </summary>
        public int X
        {
            get { return this.x; }
            set { this.SetProperty(ref this.x, value); }
        }

        private int y;
        /// <summary>
        /// Y座標
        /// </summary>
        public int Y
        {
            get { return this.y; }
            set { this.SetProperty(ref this.y, value); }
        }

        private int width;
        /// <summary>
        /// 幅
        /// </summary>
        public int Width
        {
            get { return this.width; }
            set { this.SetProperty(ref this.width, value); }
        }

        private int height;
        /// <summary>
        /// 高さ
        /// </summary>
        public int Height
        {
            get { return this.height; }
            set { this.SetProperty(ref this.height, value); }
        }

        private SolidColorBrush fill;
        /// <summary>
        /// 塗りつぶす色
        /// </summary>
        public SolidColorBrush Fill
        {
            get { return this.fill; }
            set { this.SetProperty(ref this.fill, value); }
        }

        private SolidColorBrush stroke;
        /// <summary>
        /// 輪郭の色
        /// </summary>
        public SolidColorBrush Stroke
        {
            get { return this.stroke; }
            set { this.SetProperty(ref this.stroke, value); }
        }

        private bool isSelected;
        /// <summary>
        /// 選択中であるかのフラグ
        /// </summary>
        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetProperty(ref this.isSelected, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlaceableElement()
        {
            this.X = 0;
            this.Y = 0;
            this.Width = 0;
            this.Height = 0;
            this.Fill = null;
            this.Stroke = null;
            this.IsSelected = false;
        }
    }
}
