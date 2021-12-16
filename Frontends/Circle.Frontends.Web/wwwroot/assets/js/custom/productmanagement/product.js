(() => {
    "use strict";

    var ckeditor;

    var KTProductPage = function () {

        var formUpdateProduct;
        var btnShowProductModal;
        var btnUpdateProduct;
        var btnCancelUpdateProduct;

        var handleProductPageEvent = function () {

            btnShowProductModal.addEventListener('click', function (e) {
                $("#ProductName").val(null);
                ckeditor.setData("");
                $('#divUpdateProductImage').css('background', "");

                var data = {
                    productId: getParameterByName("productId")
                };

                Get("/Admin/Products/GetProductWithId", data).done(function (response) {
                    if (response.IsSuccess) {
                        $("#ProductName").val(response.Data.Product.Name);

                        ckeditor.setData(response.Data.Product.Description);

                        if (response.Data.Product.ImageString != null && response.Data.Product.ImageString != "") {
                            $('#divUpdateProductImage').css('background', "url(" + response.Data.Product.ImageString + ")");
                            $("#divUpdateProductImage").css("background-position-x", "center");
                            $("#divUpdateProductImage").css("background-position-y", "center");
                            $("#divUpdateProductImage").css("background-size", "100%");
                        }
                    }
                });

                showModal("modalProduct");
            });

            btnUpdateProduct.addEventListener('click', function (e) {
                e.preventDefault();

                btnUpdateProduct.setAttribute('data-kt-indicator', 'on');
                btnUpdateProduct.disabled = true;

                var formData = new FormData(formUpdateProduct);
                formData.append("ProductId", $("#ProductId").val());
                formData.append("ProductCategoryId", $("#ProductCategoryId").val());
                formData.append("ProductDescription", ckeditor.getData());

                PostFormWithFile("/Admin/Products/UpdateProduct", formData).done(function (response) {
                    if (response.IsSuccess) {
                        ShowSuccessMessage(response.Message, "/Admin/Products/Product?productId=" + response.Data.Id);
                    }
                    else {
                        // Show loading indication
                        btnUpdateProduct.setAttribute('data-kt-indicator', 'off');

                        // Disable button to avoid multiple click 
                        btnUpdateProduct.disabled = false;

                        ShowErrorMessage(response.Message);
                    }
                });
            });

            btnCancelUpdateProduct.addEventListener('click', function (e) {
                hideModal("modalProduct");
            });
        }

        var handleProductPageLoad = function () {

            var data = {
                productId: getParameterByName("productId")
            };

            Get("/Admin/Products/GetProductWithId", data).done(function (response) {
                if (response.IsSuccess) {
                    $("#ProductId").val(response.Data.Product.Id);
                    $("#ProductCategoryId").val(response.Data.Product.CategoryId);

                    $("#h3ProductName").text(response.Data.Product.Name);
                    $("#spanProductCode").text(response.Data.Product.ProductCode);
                    $("#spanProductDescription").html(response.Data.Product.Description);

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
                }
            });
        }

        return {
            init: function () {
                formUpdateProduct = document.getElementById('updateProduct_form');

                btnShowProductModal = document.getElementById('btnShowProductModal');
                btnUpdateProduct = document.getElementById('btnUpdateProduct');
                btnCancelUpdateProduct = document.getElementById('btnCancelUpdateProduct');

                handleProductPageLoad();

                handleProductPageEvent();
            }
        };
    }();

    KTUtil.onDOMContentLoaded(function () {
        if (ckeditor == undefined) {
            ClassicEditor
                .create(document.querySelector('#ProductDescription '))
                .then(editor => {
                    ckeditor = editor;
                })
                .catch(error => {
                    console.error(error);
                });
        }

        KTProductPage.init();
    });
})();