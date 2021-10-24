using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Helper.Bootstrap.Grid.Column
{
    public interface IGridColumn<T>
    {

        /// <summary>
        /// Kolon Başlığı. Gönderilen veri tipinde display annotation ve name özelliği girilmiş ise tanımlamaya gerek yoktur.
        /// Başlık girilirse geçerli değer kabul edilir.
        /// </summary>
        /// <param name="title">Başlık</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Title(string title);

        /// <summary>
        /// Kolon Başlığı İpucu. Gönderilen veri tipinde display annotation ve description özelliği girilmiş ise tanımlamaya gerek yoktur.
        /// İpucu girilirse geçerli değer kabul edilir.
        /// </summary>
        /// <param name="titleTooltip">İpucu</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> TitleTooltip(string titleTooltip);

        /// <summary>
        /// Kolon başlığı HTML şablon. Başlık yazılırken uygulanmak üzere html şablon girilebilir.
        /// Başlığa ait değer gösterilmesi için şablon içine {0} veya {ColumndAdı} şeklinde eklenmelidir.
        /// </summary>
        /// <param name="titleTemplate">Şablon</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> TitleTemplate(string titleTemplate);

        /// <summary>
        /// Kolona ait verinin hangi formatta gösterileceğini belirler.
        /// Örnek : {0:d} 01.04.2016 10:34:00 gibi sonuç verir.
        /// </summary>
        /// <param name="format">Şablon</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Format(string format);

        /// <summary>
        /// Her veri satırı için kolon değeri HTML şablonu. 
        /// Kolona ait değer gösterilmesi için şablon içine {0} veya {ColumndAdı} şeklinde eklenmelidir.
        /// </summary>
        /// <param name="template">Şablon</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Template(string template);

        /// <summary>
        /// Kolonun piksel olarak genişliği.
        /// </summary>
        /// <param name="width">Genişlik</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Width(int width);

        /// <summary>
        /// Görünmeyen kolon.
        /// </summary>
        /// <param name="isHidden">Görünmez mi?</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Hidden(bool isHidden);

        /// <summary>
        /// Bu özellik gelecekte kullanılmak üzere eklenmiştir.
        /// </summary>
        /// <param name="isReadonly">Sadece Oku mu?</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Readonly(bool isReadonly);

        /// <summary>
        /// Bu özellik gelecekte kullanılmak üzere eklenmiştir.
        /// </summary>
        /// <param name="isSortable">Sıralama Yapılabilsin mi?</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Sortable(bool isSortable);

        /// <summary>
        /// Bu özellik gelecekte kullanılmak üzere eklenmiştir.
        /// </summary>
        /// <param name="isGroupable">Gruplanabilsin mi?</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Groupable(bool isGroupable);

        /// <summary>
        /// Bu özellik gelecekte kullanılmak üzere eklenmiştir.
        /// </summary>
        /// <param name="isFilterable">Filtrelenebilsin mi?</param>
        /// <returns>IGridColumn</returns>
        IGridColumn<T> Filterable(bool isFilterable);

    }
}
