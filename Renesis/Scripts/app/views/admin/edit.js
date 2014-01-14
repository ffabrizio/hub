define(function (require, exports, module) {

	'use strict';

	var Backbone = require('backbone'),
	    tinymce = require('tinymce'),
	    moduleMgr = require('cms/content-manager'),
	    validation = require('cms/utils/content-validation');

	module.exports = Backbone.View.extend({
		el: 'form',
		render: function () {
			validation.init();

			if ($('[data-role="composition"]').length) {
				moduleMgr.init('#server-data');
			}

			tinymce.init({
				menubar: false,
				statusbar: false,
				selector: 'textarea[data-editor="true"]',
				plugins: [
					  "lists link fullscreen",
					  "paste",
					  "image",
					  "media"
				],
				toolbar: "undo redo | bold italic | bullist numlist | link unlink | image fullscreen"
			});
		}
	});

});