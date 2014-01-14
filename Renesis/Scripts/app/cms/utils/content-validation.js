define(function (require, exports, module) {

	'use strict';

	var $ = require('jquery');

	module.exports = {
		init: function () {
			require(['jq-validate', 'jq-validate-unobtrusive'], function() {
				$.validator.setDefaults({
					highlight: function (element, errorClass, validClass) {
						$(element).addClass(errorClass).removeClass(validClass);
						$(element).closest('.form-group').removeClass('has-success').addClass('has-error');
					},
					unhighlight: function (element, errorClass, validClass) {
						$(element).removeClass(errorClass).addClass(validClass);
						$(element).closest('.form-group').removeClass('has-error').addClass('has-success');
					}
				});
			});
			
		}
	};

});