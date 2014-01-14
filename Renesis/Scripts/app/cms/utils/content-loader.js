define(function (require, exports, module) {

	'use strict';

	var load = function (settings, onSuccess) {
		var f = settings.filters.serialize();
		var p = 'page=' + settings.page + '&' + f;

		if (settings.query.length && settings.query.val().trim() !== '') {
			p += '&q=' + settings.query.val().trim();
		}

		$.ajax({
			url: settings.contentServer,
			data: p,
			contentType: false,
			processData: false,
			type: 'GET'
		}).complete(function (response) {

			//console.log('cms/utils/content-loader', response);
			return onSuccess(response.responseJSON);
			
		});
	};

	module.exports = {
		load : load
	};
});