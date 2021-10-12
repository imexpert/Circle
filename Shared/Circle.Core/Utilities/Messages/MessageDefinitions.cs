using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Core.Utilities.Messages
{
    public enum MessageDefinitions : int
    {
        KAYIT_ISLEMI_BASARILI = 1,
        KAYIT_ISLEMI_HATALI = 2,
        GUNCELLEME_ISLEMI_BASARILI = 3,
        GUNCELLEME_ISLEMI_HATALI = 4,
        SILME_ISLEMI_BASARILI = 5,
        SILME_ISLEMI_HATALI = 6,
        KAYIT_BULUNAMADI = 7,
        BIR_HATA_OLUSTU = 8,
        KAYIT_ZATEN_MEVCUT = 9,
        KULLANICI_ADI_YADA_SIFRE_HATALI = 10,
    }

    public static class DefaultMessageDefinitions
    {
        public static Dictionary<int, string> DefaultMessages = new Dictionary<int, string>()
        {
            { 7, "Kayıt bulunamadı."},
            { 1, "Kayıt işlemi başarılı."},
            { 2, "Kayıt işlemi sırasında hata oluştu."},
            { 8, "Bir hata oluştu. Lütfen tekrar deneyiniz."},
            { 9, "Kayıt zaten eklenmiş."},
            { 10, "Kullanıcı adı veya şifre hatalı."}
        };
    }
}
