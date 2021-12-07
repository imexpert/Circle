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

                PostForm("/Admin/Products/AddProductDetail", "modalProductDetail_form").done(function (response) {
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

        return {
            // Public functions
            init: function () {
                form = document.querySelector('#modalProductDetail_form');
                submitButton = document.getElementById('modalProductDetail_submit');
                cancelButton = document.getElementById('modalProductDetail_cancel');

                handleForm();
            }
        };
    }();

    // On document ready
    KTUtil.onDOMContentLoaded(function () {
        console.log("geldi");
        KTModalNewCard.init();
    });
    /******/
})();
//# sourceMappingURL=new-card.js.map