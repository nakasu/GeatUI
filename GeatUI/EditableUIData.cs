using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GeatUI
{
    /// <summary>
    /// 作成したUIデータを表すモデル
    /// </summary>
    class EditableUIData : BindableBase
    {
        /// <summary>
        /// 配置している要素のコレクション
        /// </summary>
        public ObservableCollection<PlaceableElement> ElementCollection { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EditableUIData()
        {
            this.ElementCollection = new ObservableCollection<PlaceableElement>();
        }

        /// <summary>
        /// 長方形を追加する
        /// </summary>
        /// <param name="area">長方形を追加する領域を指定</param>
        public void AddRect(Rect area)
        {
            var element = new PlaceableElement
            {
                X = (int)area.X,
                Y = (int)area.Y,
                Width = (int)area.Width,
                Height = (int)area.Height,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                IsSelected = false
            };

            ElementCollection.Add(element);
        }

        /// <summary>
        /// 選択状態の長方形を削除する
        /// </summary>
        public void RemoveRect()
        {
            foreach(var e in ElementCollection.Reverse())
            {
                if (e.IsSelected)
                    ElementCollection.Remove(e);
            }
        }

        /// <summary>
        /// 指定した長方形を選択状態ににする
        /// </summary>
        /// <param name="rect">選択する長方形</param>
        public void SelectRect(PlaceableElement rect)
        {
            rect.IsSelected = true;
        }

        /// <summary>
        /// すべての長方形の選択を解除する
        /// </summary>
        public void DeselectAllRect()
        {
            foreach (var e in ElementCollection)
                e.IsSelected = false;
        }
    }
}
