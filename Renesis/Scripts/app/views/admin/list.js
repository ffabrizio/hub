define(function (require, exports, module) {

	'use strict';

	var Backbone = require('backbone'),
		moduleMgr = require('cms/content-manager');

	module.exports = Backbone.View.extend({
		el: '#server-data',
		render: function () {
			moduleMgr.init($(this.el));
		}
	});

});