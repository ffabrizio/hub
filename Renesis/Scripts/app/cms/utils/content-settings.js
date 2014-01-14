define(function(require, exports, module) {

	'use strict';

	var init = function(server) {
		var settings = { page: 0 };

		settings.filters = $('input[type="checkbox"]', '#filters');
		settings.query = $('div[role="search"] input[type="text"]', '#filters');
		settings.searchbutton = $('div[role="search"] .btn', '#filters');
		settings.loadmorebutton = $('[data-action="pager"]').hide();
		settings.showmastersbutton = $('label#showMaster input:checkbox');

		settings.results = $('table#results tbody');
		settings.noresults = $('div#no-results').hide();

		settings.detailsUrl = server.data('details-url');
		settings.cloneUrl = server.data('clone-url');
		settings.isLocalInstance = server.data('local-instance');
		settings.currentCulture = server.data('current-culture');
		settings.masterCulture = server.data('master-culture');
		settings.contentServer = server.data('content-url');

		settings.isComposerMode = false;
		settings.composerArea = $('[data-role="composition"]');
		if (settings.composerArea.length) {
			settings.isComposerMode = true;
		}


		//console.log('cms/utils/content-settings', settings);
		return settings;
	};

	module.exports = {
		init : init
	};
});