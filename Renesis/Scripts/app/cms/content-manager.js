define(function (require, exports, module) {

	'use strict';

	var settingsUtils = require('cms/utils/content-settings');
	var loaderUtils = require('cms/utils/content-loader');
	var templateUtils = require('cms/utils/content-templates');
	
	var settings = {},
	    composer;

	var init = function (el) {
		var server = $(el);
		if (!server.length) return;

		settings = settingsUtils.init(server);
		if (settings.isComposerMode) {
			composer = require('cms/content-composer');
			composer.init(settings);
		}
		
		settings.filters.click(function () {
			settings.results.empty();
			settings.page = 0;


			loaderUtils.load(settings, function(data) {
				bind(data);
			});
		});

		settings.searchbutton.click(function () {
			settings.results.empty();
			settings.page = 0;

			loaderUtils.load(settings, function (data) {
				bind(data);
			});
		});

		settings.query.bind('keypress', function (e) {
			var code = (e.keyCode ? e.keyCode : e.which);
			if (code == 13) {
				settings.searchbutton.click();
				return false;
			}
			return true;
		});

		settings.loadmorebutton.click(function () {
			settings.page++;
			$(this).hide();
			loaderUtils.load(settings, function (data) {
				bind(data);
			});
		});

		settings.showmastersbutton.change(function () {
			$('tr.danger', settings.results).toggle();
		});
		
		templateUtils.init(settings);
		loaderUtils.load(settings, function (data) {
			bind(data);
		});

	};

	

	var bind = function (data) {
		if (data.Items.length < 1) {
			settings.noresults.show();
		} else {
			settings.noresults.hide();
		}

		if (data.ItemsLeft > 0) settings.loadmorebutton.show().removeClass('hide');
		for (var i in data.Items) {
			settings.results.append(templateUtils.getRowHtml(data.Items[i]));
			templateUtils.getContentPreview($('tr:last', settings.result));
		}

		if (settings.isComposerMode) {
			composer.manageItems(settings.composerArea);
		}
	};
	
	module.exports = {
		init : init
	};
});