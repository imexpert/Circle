var ckeditor;

function productUpdateModal() {

    if (ckeditor == null) {
        ClassicEditor
            .create(document.querySelector('#ProductDescription'))
            .then(editor => {
                ckeditor = editor;
            })
            .catch(error => {
                console.error(error);
            });
    }

    $("#UpdateProductName").val($("#ProductName").text());

    $("#modalProduct").modal("show");

    var currentImage = $("#divProductImage").css("background-image");

    if (currentImage != 'none') {
        $("#divUpdateProductImage").css("background-image", currentImage);
    }
}

function productDetailModal() {
    $("#h1ProductTitle").text($("#ProductName").text());
    $("#modalProductDetail").modal("show");
}

(() => {
    "use strict";

    var KTModalNewCard = function () {
        var submitButton;
        var cancelButton;
        var form;
        var modal;

        // Handle form validation and submittion
        var handleForm = function () {
            // Action buttons
            submitButton.addEventListener('click', function (e) {
                // Prevent default button action
                e.preventDefault();

                // Show loading indication
                submitButton.setAttribute('data-kt-indicator', 'on');

                // Disable button to avoid multiple click 
                submitButton.disabled = true;

                var formData = new FormData(form);

                formData.append("ProductDescription", ckeditor.getData());

                var totalFiles = document.getElementById('imgAvatar').files.length;
                for (var i = 0; i < totalFiles; i++) {
                    var file = document.getElementById('imgAvatar').files[i];
                    formData.append("Image", file);
                }

                PostFormWithFile("/Admin/Products/UpdateProduct", formData).done(function (response) {
                    if (response.IsSuccess) {
                        ShowSuccessMessage(response.Message, "/Admin/Products/Product?productId=" + response.Data.Id);
                    }
                    else {
                        // Show loading indication
                        submitButton.setAttribute('data-kt-indicator', 'off');

                        // Disable button to avoid multiple click 
                        submitButton.disabled = false;

                        ShowErrorMessage(response.Message);
                    }
                });
            });

            cancelButton.addEventListener('click', function (e) {
                e.preventDefault();
                modal.hide(); // Hide modal	
            });
        }

        var getProductDetail = function () {
            console.log("id => " + getParameterByName("productId"));
            var data = {
                productId: getParameterByName("productId")
            };

            Get("/Admin/Products/GetProductWithId", data).done(function (response) {
                if (response.IsSuccess) {
                    $("#ProductName").text(response.Data.Product.Name);
                    $("#spanProductName").text(response.Data.Product.ProductCode);
                    $('#divProductImage').css('background-image', response.Data.Product.Image);
                    console.log("GetProductWithId");
                }
            });
        }

        return {
            // Public functions
            init: function () {
                form = document.querySelector('#modalProduct_form');
                submitButton = document.getElementById('modalProduct_submit');
                cancelButton = document.getElementById('modalProduct_cancel');

                handleForm();

                getProductDetail();
            }
        };
    }();

    // On document ready
    KTUtil.onDOMContentLoaded(function () {

        KTModalNewCard.init();
    });
    /******/
})();
//# sourceMappingURL=new-card.js.map