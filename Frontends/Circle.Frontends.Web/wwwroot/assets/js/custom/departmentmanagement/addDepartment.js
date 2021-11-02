/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!****************************************************************************!*\
  !*** ../../../themes/metronic/html/demo1/src/js/custom/modals/new-card.js ***!
  \****************************************************************************/


// Class definition
var KTModalNewCard = function () {
	var submitButton;
	var cancelButton;
	var validator;
	var form;
	var modal;
	var modalEl;
	
	// Init form inputs
	var initForm = function() {
		// Expiry month. For more info, plase visit the official plugin site: https://select2.org/
        $(form.querySelector('[name="card_expiry_month"]')).on('change', function() {
            // Revalidate the field when an option is chosen
            validator.revalidateField('card_expiry_month');
        });

		// Expiry year. For more info, plase visit the official plugin site: https://select2.org/
        $(form.querySelector('[name="card_expiry_year"]')).on('change', function() {
            // Revalidate the field when an option is chosen
            validator.revalidateField('card_expiry_year');
        });
	}

	// Handle form validation and submittion
	var handleForm = function() {
		// Stepper custom navigation

		var cook = readCookie('.AspNetCore.Culture');

		var titleMessage = "Bos olamaz.";
		
		if (cook != null && cook.endsWith('en-US')) {
			titleMessage = "Cannot be empty.";
		}
		
		// Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
		validator = FormValidation.formValidation(
			form,
			{
				fields: {
					'TitleTr': {
						validators: {
							notEmpty: {
								message: titleMessage
							}
						}
					},
					'TitleUs': {
						validators: {
							notEmpty: {
								message: titleMessage
							}
						}
					}
				},
				
				plugins: {
					trigger: new FormValidation.plugins.Trigger(),
					bootstrap: new FormValidation.plugins.Bootstrap5({
						rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
					})
				}
			}
		);

		// Action buttons
		submitButton.addEventListener('click', function (e) {
			// Prevent default button action
			e.preventDefault();

			// Validate form before submit
			if (validator) {
				validator.validate().then(function (status) {
					console.log('validated!');

					if (status == 'Valid') {
						// Show loading indication
						submitButton.setAttribute('data-kt-indicator', 'on');

						// Disable button to avoid multiple click 
						submitButton.disabled = true;

						PostForm("/Admin/Departments/AddDepartment", "modalDepartment_form").done(function (response) {
							if (response.IsSuccess) {
								ShowSuccessMessage(response.Message,"/Admin/Departments/List");
							}
							else {
								// Show loading indication
								submitButton.setAttribute('data-kt-indicator', 'off');

								// Disable button to avoid multiple click 
								submitButton.disabled = false;

								ShowErrorMessage(response.Message);
							}
						});					
					}
 				});
			}
		});

		cancelButton.addEventListener('click', function (e) {
			e.preventDefault();
			modal.hide(); // Hide modal	
		});
	}

	return {
		// Public functions
		init: function () {
			// Elements
			modalEl = document.querySelector('#modalDepartment');

			if (!modalEl) {
				return;
			}

			modal = new bootstrap.Modal(modalEl);

			form = document.querySelector('#modalDepartment_form');
			submitButton = document.getElementById('modalDepartment_submit');
			cancelButton = document.getElementById('modalDepartment_cancel');

			initForm();
			handleForm();
		}
	};
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
	KTModalNewCard.init();
});
/******/ })()
;
//# sourceMappingURL=new-card.js.map