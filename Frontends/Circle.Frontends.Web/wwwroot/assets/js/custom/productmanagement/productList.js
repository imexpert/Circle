(() => {
    "use strict";

    var ckeditor;

    var KTProductPage = function () {

        var btnDeleteProduct;

        var handleProductPageEvent = function () {

            btnDeleteProduct.addEventListener('click', function (e) {
                var d = $(this).attr("data-id");
                alert("���������")
                //Swal.fire({
                //    title: '�r�n ve alt�nda bulunan �r�nler kal�c� olarak silinecek, devam etmek istiyor musunuz?',
                //    showDenyButton: true,
                //    showCancelButton: true,
                //    confirmButtonText: 'Sil',
                //    denyButtonText: 'Vazge�',
                //}).then((result) => {
                //    /* Read more about isConfirmed, isDenied below */
                //    if (result.isConfirmed) {
                //        Swal.fire('Saved!', '', 'success')
                //    } else if (result.isDenied) {
                //        Swal.fire('Changes are not saved', '', 'info')
                //    }
                //})
            });
        }

        return {
            init: function () {
                btnDeleteProduct = document.getElementById('btnDeleteProduct');

                handleProductPageEvent();
            }
        };
    }();

    KTUtil.onDOMContentLoaded(function () {
        KTProductPage.init();
    });
})();