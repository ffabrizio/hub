define(function (require, exports, module) {

	'use strict';

	var Backbone = require('backbone');

	require('bootstrap');

	module.exports = Backbone.View.extend({
		el: '#delete-confirm',
		render: function () {
			$(this.el).modal('show');
		}
	});

});