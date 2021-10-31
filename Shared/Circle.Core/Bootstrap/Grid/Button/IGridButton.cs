using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Core.Bootstrap.Grid.Button
{
    public interface IGridButton
    {

        /// <summary>
        /// Buton HTML  ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>IGridButton</returns>
        IGridButton ID(string id);

        /// <summary>
        /// Buton başlığı
        /// </summary>
        /// <param name="title">Başlık</param>
        /// <returns>IGridButton</returns>
        IGridButton Title(string title);

        /// <summary>
        /// Buton İpucu
        /// </summary>
        /// <param name="tooltip">İpucu</param>
        /// <returns></returns>
        IGridButton Tooltip(string tooltip);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <returns></returns>
        IGridButton ActionUrl(string actionUrl);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        IGridButton CssClass(string cssClass);

        /// <summary>
        /// Buton simgesi. Butonun içinde görüntülenecek simge belirlemek için kullanılır.
        /// Fontawesome daki ikin classlarında biri kulşlanılmalıdır.
        /// </summary>
        /// <param name="icon">Icon</param>
        /// <returns>IGridButton</returns>
        IGridButton Icon(string icon);

        /// <summary>
        /// Buton tipi. Ekle, sil ve diğer buton tiplerini belirlemek yada özel buton tipi uygulamak için buton tipini değiştirebilirsiniz.
        /// </summary>
        /// <param name="buttonType">Buton Tipi</param>
        /// <returns>IGridButton</returns>
        IGridButton ButtonType(GridButtonType buttonType);

    }
}
