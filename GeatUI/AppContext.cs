using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeatUI
{
    /// <summary>
    /// アプリケーションのモデルを統括するクラス
    /// </summary>
    class AppContext
    {
        /// <summary>
        /// 編集中のUIデータ
        /// </summary>
        public EditableUIData UIData { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AppContext()
        {
            this.UIData = new EditableUIData();
        }
    }
}
