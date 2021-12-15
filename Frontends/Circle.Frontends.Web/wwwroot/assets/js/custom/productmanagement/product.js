var ckeditor;

function productUpdateModal() {

    var data = {
        productId: getParameterByName("productId")
    };

    Get("/Admin/Products/GetProductWithId", data).done(function (response) {
        if (response.IsSuccess) {
            $("#UpdateProductName").val(response.Data.Product.Name);
            $("#UpdateProductId").val(response.Data.Product.Id);
            $("#UpdateProductCategoryId").val(response.Data.Product.CategoryId);
            ckeditor.setData(response.Data.Product.Description);
            $("#modalProduct").modal("show");

            var currentImage = $("#divProductImage").css("background-image");

            if (currentImage != 'none') {
                $("#divUpdateProductImage").css("background-image", currentImage);
                $("#divUpdateProductImage").css("background-position-x", "center");
                $("#divUpdateProductImage").css("background-position-y", "center");
            }
        }
    });


}

function productDetailModal() {
    $("#h1ProductTitle").text($("#ProductName").text());
    $("#UpdateProductId").val(getParameterByName("productId"));
    $("#modalProductDetail").modal("show");
}

(() => {
    "use strict";

    var KTModalNewCard = function () {
        var submitButton;
        var cancelButton;
        var submitDetailButton;
        var cancelDetailButton;
        var form;
        var formDetail;
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
                formData.append("UpdateProductDescription", ckeditor.getData());

                var currentImage = $("#divUpdateProductImage").css("background-image");

                formData.append("IsImageExist", currentImage != 'none');


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

        var handleDetailForm = function () {
            // Action buttons
            submitDetailButton.addEventListener('click', function (e) {
                // Prevent default button action
                e.preventDefault();

                // Show loading indication
                submitDetailButton.setAttribute('data-kt-indicator', 'on');

                // Disable button to avoid multiple click 
                submitDetailButton.disabled = true;

                PostForm("/Admin/Products/AddProductDetail", "modalProductDetail_form").done(function (response) {
                    if (response.IsSuccess) {
                        ShowSuccessMessage(response.Message, "/Admin/Products/Product?productId=" + response.Data.ProductId);
                    }
                    else {
                        // Show loading indication
                        submitDetailButton.setAttribute('data-kt-indicator', 'off');

                        // Disable button to avoid multiple click 
                        submitDetailButton.disabled = false;

                        ShowErrorMessage(response.Message);
                    }
                });
            });

            cancelDetailButton.addEventListener('click', function (e) {
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
                    debugger;
                    $("#ProductName").text(response.Data.Product.Name);
                    $("#UpdateProductCategoryId").text(response.Data.Product.CategoryId);
                    $("#UpdateProductId").text(response.Data.Product.Id);

                    $("#spanProductDescription").html(response.Data.Product.Description);

                    $("#UpdateProductDescription").text(response.Data.Product.Description);
                    $("#spanProductName").text(response.Data.Product.ProductCode);

                    if (response.Data.Product.ImageString != null && response.Data.Product.ImageString != "") {
                        $('#divProductImage').css('background', "url(" + response.Data.Product.ImageString + ")");
                        $("#divProductImage").css("background-position-x", "center");
                        $("#divProductImage").css("background-position-y", "center");
                        $("#divProductImage").css("background-size", "100%");
                    }

                    if (response.Data.ProductDetailList.length > 0) {
                        for (var i = 0; i < response.Data.ProductDetailList.length; i++) {
                            var materialName = "<td>" + response.Data.ProductDetailList[i].MaterialName + "</td>";
                            var materialDetailName = "<td>" + response.Data.ProductDetailList[i].MaterialDetailName + "</td>";
                            var diameter = "<td>" + response.Data.ProductDetailList[i].Diameter + "</td>";
                            var length = "<td>" + response.Data.ProductDetailList[i].Length + "</td>";
                            var productCode = "<td>" + $("#spanProductName").text() + response.Data.ProductDetailList[i].ProductCode + "</td>";
                            var buttons = "<td><div class='btn-group btn-group-sm' role='group' aria-label='Small button group'><button type='button' class='btn btn-success btn-sm btn-outline-dark'><i class='fa fa-trash'></i></button><button type='button' class='btn btn-success btn-sm  btn-outline-dark'><i class='fa fa-edit'></i></button></div></td>";

                            var tr = "<tr>" + materialName + materialDetailName + diameter + length + productCode + buttons + "</tr>";
                            $("#tableProductDetail > tbody").append(tr);
                        }

                    }
                    console.log("GetProductWithId");
                }
            });
        }

        return {
            // Public functions
            init: function () {
                form = document.querySelector('#modalProduct_form');
                formDetail = document.querySelector('#modalProductDetail_form');
                submitButton = document.getElementById('modalProduct_submit');
                cancelButton = document.getElementById('modalProduct_cancel');

                submitDetailButton = document.getElementById('modalProductDetail_submit');
                cancelDetailButton = document.getElementById('modalProductDetail_cancel');

                handleForm();

                handleDetailForm();

                getProductDetail();
            }
        };
    }();

    // On document ready
    KTUtil.onDOMContentLoaded(function () {
        if (ckeditor == undefined) {
            ClassicEditor
                .create(document.querySelector('#UpdateProductDescription '))
                .then(editor => {
                    ckeditor = editor;
                })
                .catch(error => {
                    console.error(error);
                });
        }
        KTModalNewCard.init();
    });
    /******/
})();
//# sourceMappingURL=new-card.js.map